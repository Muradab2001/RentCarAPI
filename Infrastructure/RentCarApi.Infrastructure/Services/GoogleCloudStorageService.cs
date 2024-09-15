using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RentCarApi.Application.Helpers;
using RentCarApi.Application.Services;
using RentCarApi.Domain.Enum;

namespace RentCarApi.Infrastructure.Services;
public class GoogleCloudStorageService : IStorageService
{
    private readonly StorageClient _storageClient;
    private readonly string _bucketName;
   
    public GoogleCloudStorageService(IConfiguration configuration)
    {
        string credentialsPath = configuration["GoogleCloudStorage:CredentialsPath"];
        GoogleCredential credential;

        using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream);
        }

        _bucketName = configuration["GoogleCloudStorage:BucketName"];
        _storageClient = StorageClient.Create(credential);
    }

    public async Task<string> UploadPhotoAsync(IFormFile file, FolderNames folderName)
    {
        using var convertedStream = await FileHelpers.ConvertToWebPAsync(file);

        string fileName = Guid.NewGuid().ToString() + ".webp";
        string folderPath = folderName.ToString();
        string fullPath = $"{folderPath}/{fileName}";

        var imageObject = new Google.Apis.Storage.v1.Data.Object
        {
            Bucket = _bucketName,
            Name = fullPath,  
            ContentType = "image/webp"
        };

        convertedStream.Position = 0;
        await _storageClient.UploadObjectAsync(imageObject, convertedStream);

        return $"https://storage.googleapis.com/{_bucketName}/{fullPath}";
    }

    public async Task DeletePhotoAsync(string fileUrl)
    {
        var uri = new Uri(fileUrl);
        string fullPath = uri.AbsolutePath.TrimStart('/').Replace($"{_bucketName}/", "");
        await _storageClient.DeleteObjectAsync(_bucketName, fullPath);
    }
}
