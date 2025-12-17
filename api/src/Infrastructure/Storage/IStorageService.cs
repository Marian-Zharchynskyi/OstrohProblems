using Microsoft.AspNetCore.Http;

namespace Infrastructure.Storage;

public interface IStorageService
{
    Task<string?> UploadFileAsync(string prefix, IFormFile file, CancellationToken cancellationToken = default);
    Task<List<string>?> UploadFilesAsync(string prefix, IFormFileCollection files, CancellationToken cancellationToken = default);
    Task<bool> DeleteFileAsync(string fileKey, CancellationToken cancellationToken = default);
    string GetPublicUrl(string fileKey);
}
