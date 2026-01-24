using System.Security.Claims;
using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Optional.Unsafe;

namespace Infrastructure.Middleware;

public class ClerkUserSyncMiddleware(RequestDelegate next, ILogger<ClerkUserSyncMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var clerkId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            // Also try "sub" claim
            if (string.IsNullOrEmpty(clerkId))
            {
                clerkId = context.User.FindFirstValue("sub");
            }
            
            if (!string.IsNullOrEmpty(clerkId))
            {
                using var scope = serviceProvider.CreateScope();
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                var roleQueries = scope.ServiceProvider.GetRequiredService<IRoleQueries>();
                
                var existingUser = await userRepository.SearchByClerkId(clerkId, context.RequestAborted);
                
                if (!existingUser.HasValue)
                {
                    // User exists in Clerk but not in our DB - create them (JIT provisioning)
                    var email = context.User.FindFirstValue(ClaimTypes.Email) ?? 
                                context.User.FindFirstValue("email");
                                
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
                            logger.LogInformation("JIT provisioned user with ID {UserId} for ClerkId {ClerkId}", createdUser.Id.Value, clerkId);
                            if (context.User.Identity is ClaimsIdentity identity)
                            {
                                identity.AddClaim(new Claim("user_id", createdUser.Id.Value.ToString()));
                            }
                        }
                    }
                }
                else
                {
                    // User exists, add internal ID to claims for IdentityService to use
                    var user = existingUser.ValueOrFailure();
                    
                    if (context.User.Identity is ClaimsIdentity identity && !identity.HasClaim(c => c.Type == "user_id"))
                    {
                        identity.AddClaim(new Claim("user_id", user.Id.Value.ToString()));
                    }
                }
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
