using System.Security.Claims;
using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Optional.Unsafe;

namespace Infrastructure.Middleware;

public class ClerkUserSyncMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        Console.WriteLine($"ClerkUserSyncMiddleware: IsAuthenticated = {context.User.Identity?.IsAuthenticated}");
        
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var clerkId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine($"ClerkUserSyncMiddleware: ClerkId from NameIdentifier = {clerkId}");
            
            // Also try "sub" claim
            if (string.IsNullOrEmpty(clerkId))
            {
                clerkId = context.User.FindFirstValue("sub");
                Console.WriteLine($"ClerkUserSyncMiddleware: ClerkId from sub = {clerkId}");
            }
            
            if (!string.IsNullOrEmpty(clerkId))
            {
                using var scope = serviceProvider.CreateScope();
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                var roleQueries = scope.ServiceProvider.GetRequiredService<IRoleQueries>();
                
                var existingUser = await userRepository.SearchByClerkId(clerkId, context.RequestAborted);
                Console.WriteLine($"ClerkUserSyncMiddleware: User exists in DB = {existingUser.HasValue}");
                
                if (!existingUser.HasValue)
                {
                    // User exists in Clerk but not in our DB - create them (JIT provisioning)
                    var email = context.User.FindFirstValue(ClaimTypes.Email) ?? 
                                context.User.FindFirstValue("email");
                    Console.WriteLine($"ClerkUserSyncMiddleware: Email = {email}");
                                
                    if (!string.IsNullOrEmpty(email))
                    {
                        var createdUser = await CreateUserJit(
                            clerkId, 
                            email, 
                            context.User, 
                            userRepository, 
                            roleQueries, 
                            context.RequestAborted);
                            
                        if (createdUser != null)
                        {
                            Console.WriteLine($"ClerkUserSyncMiddleware: Created user with ID = {createdUser.Id.Value}");
                            if (context.User.Identity is ClaimsIdentity identity)
                            {
                                identity.AddClaim(new Claim("user_id", createdUser.Id.Value.ToString()));
                            }
                        }
                        else
                        {
                            Console.WriteLine("ClerkUserSyncMiddleware: Failed to create user");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ClerkUserSyncMiddleware: No email found, cannot create user");
                    }
                }
                else
                {
                    // User exists, add internal ID to claims for IdentityService to use
                    var user = existingUser.ValueOrFailure();
                    Console.WriteLine($"ClerkUserSyncMiddleware: Found existing user with ID = {user.Id.Value}");
                    
                    if (context.User.Identity is ClaimsIdentity identity && !identity.HasClaim(c => c.Type == "user_id"))
                    {
                        identity.AddClaim(new Claim("user_id", user.Id.Value.ToString()));
                        Console.WriteLine($"ClerkUserSyncMiddleware: Added user_id claim");
                    }
                }
            }
            else
            {
                Console.WriteLine("ClerkUserSyncMiddleware: No ClerkId found");
            }
        }

        await next(context);
    }

    private async Task<User?> CreateUserJit(
        string clerkId, 
        string email, 
        ClaimsPrincipal principal, 
        IUserRepository userRepository, 
        IRoleQueries roleQueries, 
        CancellationToken cancellationToken)
    {
        try
        {
            // Determine role from claims or metadata
            string roleName = RoleNames.User;
            var roleClaim = principal.FindFirst(ClaimTypes.Role)?.Value ?? 
                           principal.FindFirst("role")?.Value;
                           
            if (!string.IsNullOrEmpty(roleClaim))
            {
                roleName = roleClaim;
            }
            // Also check for metadata in case it's passed differently
            else 
            {
                var metadata = principal.FindFirst("public_metadata")?.Value;
                if (!string.IsNullOrEmpty(metadata) && metadata.Contains("\"role\":\"Administrator\""))
                {
                    roleName = RoleNames.Admin;
                }
                else if (!string.IsNullOrEmpty(metadata) && metadata.Contains("\"role\":\"Coordinator\""))
                {
                    roleName = RoleNames.Coordinator;
                }
            }

            var roleResult = await roleQueries.GetByName(roleName, cancellationToken);
            if (!roleResult.HasValue)
            {
                roleResult = await roleQueries.GetByName(RoleNames.User, cancellationToken);
                if (!roleResult.HasValue) return null; // Should not happen if seeded correctly
            }

            var role = roleResult.ValueOrFailure();
            
            // Extract name info
            var name = principal.FindFirst(ClaimTypes.Name)?.Value ?? principal.FindFirst("name")?.Value;
            string? firstName = name; 
            string? lastName = null;
            
            if (!string.IsNullOrEmpty(name) && name.Contains(' '))
            {
                var parts = name.Split(' ', 2);
                firstName = parts[0];
                lastName = parts.Length > 1 ? parts[1] : null;
            }

            var userId = UserId.New();
            var user = User.New(
                userId, 
                email, 
                firstName, 
                lastName, 
                null, 
                string.Empty,
                role.Id,
                clerkId);

            await userRepository.Create(user, cancellationToken);
            return user;
        }
        catch
        {
            // Log error but don't crash the request
            // User will try to be created again on next request
            return null;
        }
    }
}
