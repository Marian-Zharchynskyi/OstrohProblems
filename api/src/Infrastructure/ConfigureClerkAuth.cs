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
                        if (context.Principal?.Identity is System.Security.Claims.ClaimsIdentity identity)
                        {
                            // Check if role claim already exists (from Clerk session token template)
                            var existingRoleClaim = context.Principal.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
                            
                            // Check if role is empty, null, or whitespace
                            if (string.IsNullOrWhiteSpace(existingRoleClaim))
                            {
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
                                            }
                                            else
                                            {
                                                identity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "User"));
                                            }
                                        }
                                        else
                                        {
                                            identity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "User"));
                                        }
                                    }
                                    catch
                                    {
                                        identity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "User"));
                                    }
                                }
                                else
                                {
                                    // Default role if no metadata at all or metadata is empty object
                                    identity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "User"));
                                }
                            }
                        }
                        
                        return Task.CompletedTask;
                    }
                };
            });
    }
}

