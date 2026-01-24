namespace Application.Common.Interfaces;

public interface IClerkApiService
{
    /// <summary>
    /// Updates user profile information in Clerk
    /// </summary>
    Task<bool> UpdateUserAsync(string clerkId, string? firstName, string? lastName, string? email);
    
    /// <summary>
    /// Updates user's public metadata in Clerk (e.g., role)
    /// </summary>
    Task<bool> UpdateUserMetadataAsync(string clerkId, Dictionary<string, object> metadata);
    
    /// <summary>
    /// Deletes a user from Clerk
    /// </summary>
    Task<bool> DeleteUserAsync(string clerkId);
    
    /// <summary>
    /// Creates a user in Clerk
    /// </summary>
    Task<string?> CreateUserAsync(string email, string password, string? firstName, string? lastName, string role);
    
    /// <summary>
    /// Updates user's password in Clerk
    /// </summary>
    Task<bool> UpdateUserPasswordAsync(string clerkId, string newPassword);
}
