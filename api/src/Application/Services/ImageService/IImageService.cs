using Application.Common;
using Microsoft.AspNetCore.Http;
using Optional;

namespace Application.Services.ImageService
{
    public interface IImageService
    {
        Task<Option<string>> SaveImageFromFileAsync(string prefix, IFormFile image, string? oldImageKey);
        Task<Option<List<string>>> SaveImagesFromFilesAsync(string prefix, IFormFileCollection images);
        Task<Result<bool, string>> DeleteImageAsync(string imageKey);
        string GetImageUrl(string imageKey);
    }
}