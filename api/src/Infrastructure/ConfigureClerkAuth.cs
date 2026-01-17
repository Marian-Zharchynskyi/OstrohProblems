using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class ConfigureClerkAuth
{
    public static void AddClerkAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var clerkDomain = configuration["Clerk:Domain"];
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = $"https://{clerkDomain}";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = $"https://{clerkDomain}",
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
                
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                        {
                            context.Token = accessToken;
                        }
                        
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        if (context.Exception.InnerException != null)
                        {
                            Console.WriteLine($"Inner exception: {context.Exception.InnerException.Message}");
                        }
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token validated successfully");
                        
                        if (context.Principal?.Identity is System.Security.Claims.ClaimsIdentity identity)
                        {
                            // Log all claims for debugging
                            foreach (var claim in identity.Claims)
                            {
                                Console.WriteLine($"Claim: {claim.Type} = {claim.Value}");
                            }
                            
                            // First check if role claim already exists (from Clerk session token template)
                            // Your Clerk template uses: "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": "{{user.public_metadata.role}}"
                            var existingRoleClaim = context.Principal.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
                            
                            // Check if role is empty, null, or whitespace
                            if (string.IsNullOrWhiteSpace(existingRoleClaim))
                            {
                                Console.WriteLine("Role claim is empty or null, checking public_metadata...");
                                
                                // Try to extract from public_metadata claim
                                var publicMetadataClaim = context.Principal.FindFirst("public_metadata")?.Value;
                                if (!string.IsNullOrEmpty(publicMetadataClaim) && publicMetadataClaim != "{}")
                                {
                                    try
                                    {
                                        var metadata = System.Text.Json.JsonSerializer.Deserialize<System.Collections.Generic.Dictionary<string, object>>(publicMetadataClaim);
                                        if (metadata != null && metadata.ContainsKey("role"))
                                        {
                                            var role = metadata["role"]?.ToString();
                                            if (!string.IsNullOrEmpty(role))
                                            {
                                                identity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, role));
                                                Console.WriteLine($"Added role from public_metadata: {role}");
                                            }
                                            else
                                            {
                                                identity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "User"));
                                                Console.WriteLine("Role in metadata is empty, defaulting to User");
                                            }
                                        }
                                        else
                                        {
                                            identity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "User"));
                                            Console.WriteLine("No role key in metadata, defaulting to User");
                                        }
                                    }
                                    catch
                                    {
                                        identity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "User"));
                                        Console.WriteLine("Failed to parse metadata, defaulting to User");
                                    }
                                }
                                else
                                {
                                    // Default role if no metadata at all or metadata is empty object
                                    identity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "User"));
                                    Console.WriteLine("No public_metadata or empty object, defaulting to User");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Role from Clerk template: {existingRoleClaim}");
                            }
                        }
                        
                        return Task.CompletedTask;
                    }
                };
            });
    }
}
