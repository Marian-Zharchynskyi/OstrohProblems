using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class ClerkApiService : IClerkApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ClerkApiService> _logger;
    private readonly string? _secretKey;

    public ClerkApiService(HttpClient httpClient, IConfiguration configuration, ILogger<ClerkApiService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _secretKey = configuration["Clerk:SecretKey"];
        
        if (!string.IsNullOrEmpty(_secretKey))
        {
            _httpClient.BaseAddress = new Uri("https://api.clerk.com/v1/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _secretKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        else
        {
            _logger.LogWarning("Clerk:SecretKey is not configured. Clerk API sync will be disabled.");
        }
    }

    public async Task<bool> UpdateUserAsync(string clerkId, string? firstName, string? lastName, string? email)
    {
        if (string.IsNullOrEmpty(_secretKey))
        {
            _logger.LogWarning("Clerk API is not configured. Skipping user update.");
            return false;
        }

        try
        {
            var updateData = new Dictionary<string, object?>();
            
            if (firstName != null) updateData["first_name"] = firstName;
            if (lastName != null) updateData["last_name"] = lastName;
            // Note: Clerk requires primary_email_address_id to update email, which is more complex
            // For email updates, consider using Clerk's email management endpoints
            
            if (updateData.Count == 0)
            {
                _logger.LogDebug("No fields to update for Clerk user {ClerkId}", clerkId);
                return true;
            }

            var content = new StringContent(
                JsonSerializer.Serialize(updateData),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PatchAsync($"users/{clerkId}", content);
            
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Successfully updated Clerk user {ClerkId}", clerkId);
                return true;
            }
            
            var errorContent = await response.Content.ReadAsStringAsync();
            _logger.LogError("Failed to update Clerk user {ClerkId}. Status: {Status}, Error: {Error}", 
                clerkId, response.StatusCode, errorContent);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating Clerk user {ClerkId}", clerkId);
            return false;
        }
    }

    public async Task<bool> UpdateUserMetadataAsync(string clerkId, Dictionary<string, object> metadata)
    {
        if (string.IsNullOrEmpty(_secretKey))
        {
            _logger.LogWarning("Clerk API is not configured. Skipping metadata update.");
            return false;
        }

        try
        {
            var updateData = new { public_metadata = metadata };
            
            var content = new StringContent(
                JsonSerializer.Serialize(updateData),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PatchAsync($"users/{clerkId}", content);
            
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Successfully updated Clerk user metadata {ClerkId}", clerkId);
                return true;
            }
            
            var errorContent = await response.Content.ReadAsStringAsync();
            _logger.LogError("Failed to update Clerk user metadata {ClerkId}. Status: {Status}, Error: {Error}", 
                clerkId, response.StatusCode, errorContent);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating Clerk user metadata {ClerkId}", clerkId);
            return false;
        }
    }

    public async Task<bool> DeleteUserAsync(string clerkId)
    {
        if (string.IsNullOrEmpty(_secretKey))
        {
            _logger.LogWarning("Clerk API is not configured. Skipping user deletion.");
            return false;
        }

        try
        {
            var response = await _httpClient.DeleteAsync($"users/{clerkId}");
            
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Successfully deleted Clerk user {ClerkId}", clerkId);
                return true;
            }
            
            // 404 means user already doesn't exist in Clerk, which is fine
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _logger.LogWarning("Clerk user {ClerkId} not found. May have been already deleted.", clerkId);
                return true;
            }
            
            var errorContent = await response.Content.ReadAsStringAsync();
            _logger.LogError("Failed to delete Clerk user {ClerkId}. Status: {Status}, Error: {Error}", 
                clerkId, response.StatusCode, errorContent);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting Clerk user {ClerkId}", clerkId);
            return false;
        }
    }

    public async Task<string?> CreateUserAsync(string email, string password, string? firstName, string? lastName, string role)
    {
        if (string.IsNullOrEmpty(_secretKey))
        {
            _logger.LogWarning("Clerk API is not configured. Skipping user creation.");
            return null;
        }

        try
        {
            var createData = new
            {
                email_address = new[] { email },
                password,
                first_name = firstName,
                last_name = lastName,
                public_metadata = new { role },
                skip_password_checks = true,
                skip_password_requirement = false
            };

            var content = new StringContent(
                JsonSerializer.Serialize(createData),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("users", content);
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(responseContent);
                var clerkId = doc.RootElement.GetProperty("id").GetString();
                
                _logger.LogInformation("Successfully created Clerk user with email {Email}, ClerkId: {ClerkId}", email, clerkId);
                return clerkId;
            }
            
            var errorContent = await response.Content.ReadAsStringAsync();
            _logger.LogError("Failed to create Clerk user. Status: {Status}, Error: {Error}", 
                response.StatusCode, errorContent);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating Clerk user with email {Email}", email);
            return null;
        }
    }
}
