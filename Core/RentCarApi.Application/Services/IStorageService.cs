using Microsoft.AspNetCore.Http;
using RentCarApi.Domain.Enum;

namespace RentCarApi.Application.Services;
public interface IStorageService
{
     Task<string> UploadPhotoAsync(IFormFile file, FolderNames folderName);
     Task DeletePhotoAsync(string fileUrl);
}
