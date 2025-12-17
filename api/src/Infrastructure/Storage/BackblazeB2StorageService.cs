using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Storage;

public class BackblazeB2StorageService : IStorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly BackblazeB2Settings _settings;

    public BackblazeB2StorageService(IAmazonS3 s3Client, IOptions<BackblazeB2Settings> settings)
    {
        _s3Client = s3Client;
        _settings = settings.Value;
    }

    public async Task<string?> UploadFileAsync(string prefix, IFormFile file, CancellationToken cancellationToken = default)
    {
        try
        {
            var contentType = file.ContentType.Split('/');
            if (contentType[0] != "image")
            {
                return null;
            }

            var fileExtension = contentType[1];
            var fileName = $"{Guid.NewGuid()}.{fileExtension}";
            var key = $"{prefix}/{fileName}";

            using var stream = file.OpenReadStream();
            
            var putRequest = new PutObjectRequest
            {
                BucketName = _settings.BucketName,
                Key = key,
                InputStream = stream,
                ContentType = file.ContentType
            };

            await _s3Client.PutObjectAsync(putRequest, cancellationToken);
            
            return key;
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<string>?> UploadFilesAsync(string prefix, IFormFileCollection files, CancellationToken cancellationToken = default)
    {
        try
        {
            var uploadedKeys = new List<string>();

            foreach (var file in files)
            {
                var contentType = file.ContentType.Split('/');
                if (contentType[0] != "image")
                {
                    return null;
                }

                var fileExtension = contentType[1];
                var fileName = $"{Guid.NewGuid()}.{fileExtension}";
                var key = $"{prefix}/{fileName}";

                using var stream = file.OpenReadStream();
                
                var putRequest = new PutObjectRequest
                {
                    BucketName = _settings.BucketName,
                    Key = key,
                    InputStream = stream,
                    ContentType = file.ContentType
                };

                await _s3Client.PutObjectAsync(putRequest, cancellationToken);
                uploadedKeys.Add(key);
            }

            return uploadedKeys;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> DeleteFileAsync(string fileKey, CancellationToken cancellationToken = default)
    {
        try
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _settings.BucketName,
                Key = fileKey
            };

            await _s3Client.DeleteObjectAsync(deleteRequest, cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public string GetPublicUrl(string fileKey)
        => _settings.GetPublicUrl(fileKey);
}
