using Microsoft.AspNetCore.Http;

namespace Application.Services.Interfaces;

public interface IFileStorageService
{
    Task<string> UploadImageAsync(IFormFile file, string folderName);
    Task<bool> DeleteImageAsync(string publicId);
}