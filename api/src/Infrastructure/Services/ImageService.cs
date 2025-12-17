using Application.Common;
using Application.Services.ImageService;
using Infrastructure.Storage;
using Microsoft.AspNetCore.Http;
using Optional;

namespace Infrastructure.Services;

public class ImageService(IStorageService storageService) : IImageService
{
    public async Task<Option<string>> SaveImageFromFileAsync(string prefix, IFormFile image, string? oldImageKey)
    {
        if (!string.IsNullOrEmpty(oldImageKey))
        {
            await storageService.DeleteFileAsync(oldImageKey);
        }

        var result = await storageService.UploadFileAsync(prefix, image);
        
        return result != null 
            ? Option.Some(result) 
            : Option.None<string>();
    }

    public async Task<Option<List<string>>> SaveImagesFromFilesAsync(string prefix, IFormFileCollection images)
    {
        var result = await storageService.UploadFilesAsync(prefix, images);
        
        return result != null 
            ? Option.Some(result) 
            : Option.None<List<string>>();
    }

    public async Task<Result<bool, string>> DeleteImageAsync(string imageKey)
    {
        var result = await storageService.DeleteFileAsync(imageKey);
        
        if (result)
            return true;
        
        return "Failed to delete image";
    }

    public string GetImageUrl(string imageKey)
        => storageService.GetPublicUrl(imageKey);
}
