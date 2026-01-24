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
            // Update email first if provided
            if (!string.IsNullOrEmpty(email))
            {
                var emailUpdateSuccess = await UpdateUserEmailAsync(clerkId, email);
                if (!emailUpdateSuccess)
                {
                    _logger.LogWarning("Failed to update email for Clerk user {ClerkId}, continuing with other updates", clerkId);
                }
            }
            
            // Update other fields
            var updateData = new Dictionary<string, object?>();
            
            if (firstName != null) updateData["first_name"] = firstName;
            if (lastName != null) updateData["last_name"] = lastName;
            
            if (updateData.Count == 0)
            {
                _logger.LogDebug("No additional fields to update for Clerk user {ClerkId}", clerkId);
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

    private async Task<bool> UpdateUserEmailAsync(string clerkId, string newEmail)
    {
        try
        {
            // First, get the user to find their current primary email address
            var getUserResponse = await _httpClient.GetAsync($"users/{clerkId}");
            if (!getUserResponse.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to get Clerk user {ClerkId} for email update", clerkId);
                return false;
            }

            var userContent = await getUserResponse.Content.ReadAsStringAsync();
            using var userDoc = JsonDocument.Parse(userContent);
            
            // Get current primary email
            var primaryEmailAddress = userDoc.RootElement.GetProperty("primary_email_address_id").GetString();
            var emailAddresses = userDoc.RootElement.GetProperty("email_addresses");
            
            string? currentEmail = null;
            bool isLinkedToOAuth = false;
            
            foreach (var emailAddr in emailAddresses.EnumerateArray())
            {
                var emailId = emailAddr.GetProperty("id").GetString();
                if (emailId == primaryEmailAddress)
                {
                    currentEmail = emailAddr.GetProperty("email_address").GetString();
                    
                    // Check if email is linked to OAuth provider
                    if (emailAddr.TryGetProperty("linked_to", out var linkedTo) && linkedTo.GetArrayLength() > 0)
                    {
                        isLinkedToOAuth = true;
                        _logger.LogInformation("Primary email {Email} is linked to OAuth provider for user {ClerkId}", currentEmail, clerkId);
                    }
                    break;
                }
            }

            // If email is the same, no need to update
            if (currentEmail == newEmail)
            {
                _logger.LogDebug("Email is the same, no update needed for Clerk user {ClerkId}", clerkId);
                return true;
            }

            _logger.LogInformation("Updating email for Clerk user {ClerkId} from {OldEmail} to {NewEmail}", clerkId, currentEmail, newEmail);

            // For OAuth-linked emails, we cannot change the primary email directly
            if (isLinkedToOAuth)
            {
                _logger.LogWarning("Cannot update OAuth-linked email for user {ClerkId}. OAuth email must be updated through the OAuth provider.", clerkId);
                // Return a special error code that we can catch
                throw new InvalidOperationException("OAUTH_EMAIL_CANNOT_BE_CHANGED");
            }

            // For non-OAuth users, we need to create a new email address and set it as primary
            // Step 1: Create new email address
            var createEmailData = new Dictionary<string, object>
            {
                ["user_id"] = clerkId,
                ["email_address"] = newEmail,
                ["verified"] = true
            };
            
            var createEmailContent = new StringContent(
                JsonSerializer.Serialize(createEmailData),
                Encoding.UTF8,
                "application/json"
            );

            var createEmailResponse = await _httpClient.PostAsync(
                $"email_addresses", 
                createEmailContent
            );

            if (!createEmailResponse.IsSuccessStatusCode)
            {
                var errorContent = await createEmailResponse.Content.ReadAsStringAsync();
                _logger.LogError("Failed to create new email address. Status: {Status}, Error: {Error}", 
                    createEmailResponse.StatusCode, errorContent);
                return false;
            }

            // Get the new email ID from response
            var createResponseContent = await createEmailResponse.Content.ReadAsStringAsync();
            using var emailDoc = JsonDocument.Parse(createResponseContent);
            var newEmailId = emailDoc.RootElement.GetProperty("id").GetString();

            if (string.IsNullOrEmpty(newEmailId))
            {
                _logger.LogError("Failed to get new email ID from response");
                return false;
            }

            _logger.LogInformation("Created new email address {EmailId} with value {Email}", newEmailId, newEmail);

            // Step 2: Update user to set the new email as primary
            var updateUserData = new Dictionary<string, object>
            {
                ["primary_email_address_id"] = newEmailId
            };
            
            var updateUserContent = new StringContent(
                JsonSerializer.Serialize(updateUserData),
                Encoding.UTF8,
                "application/json"
            );

            var updateUserResponse = await _httpClient.PatchAsync($"users/{clerkId}", updateUserContent);

            if (!updateUserResponse.IsSuccessStatusCode)
            {
                var errorContent = await updateUserResponse.Content.ReadAsStringAsync();
                _logger.LogError("Failed to set new email as primary for Clerk user {ClerkId}. Status: {Status}, Error: {Error}", 
                    clerkId, updateUserResponse.StatusCode, errorContent);
                
                // Try to clean up the created email
                await _httpClient.DeleteAsync($"email_addresses/{newEmailId}");
                return false;
            }

            _logger.LogInformation("Successfully set email {EmailId} as primary for user {ClerkId}", newEmailId, clerkId);

            // Step 3: Delete the old email address
            if (!string.IsNullOrEmpty(primaryEmailAddress))
            {
                var deleteResponse = await _httpClient.DeleteAsync($"email_addresses/{primaryEmailAddress}");
                if (!deleteResponse.IsSuccessStatusCode)
                {
                    // Not critical, just log warning
                    _logger.LogWarning("Failed to delete old email {OldEmailId} for user {ClerkId}", primaryEmailAddress, clerkId);
                }
                else
                {
                    _logger.LogInformation("Successfully deleted old email {OldEmailId}", primaryEmailAddress);
                }
            }

            _logger.LogInformation("Successfully updated email for Clerk user {ClerkId} to {NewEmail}", clerkId, newEmail);
            return true;
        }
        catch (InvalidOperationException ex) when (ex.Message == "OAUTH_EMAIL_CANNOT_BE_CHANGED")
        {
            // Re-throw this specific exception so it can be caught by the caller
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating email for Clerk user {ClerkId}", clerkId);
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

    public async Task<bool> UpdateUserPasswordAsync(string clerkId, string newPassword)
    {
        if (string.IsNullOrEmpty(_secretKey))
        {
            _logger.LogWarning("Clerk API is not configured. Skipping password update.");
            return false;
        }

        try
        {
            var updateData = new { password = newPassword };
            
            var content = new StringContent(
                JsonSerializer.Serialize(updateData),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PatchAsync($"users/{clerkId}", content);
            
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Successfully updated password for Clerk user {ClerkId}", clerkId);
                return true;
            }
            
            var errorContent = await response.Content.ReadAsStringAsync();
            _logger.LogError("Failed to update password for Clerk user {ClerkId}. Status: {Status}, Error: {Error}", 
                clerkId, response.StatusCode, errorContent);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating password for Clerk user {ClerkId}", clerkId);
            return false;
        }
    }
}
