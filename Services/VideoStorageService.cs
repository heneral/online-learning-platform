using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace OnlineLearningPlatform.Services;

public interface IVideoStorageService
{
    Task<string> UploadVideoAsync(Stream videoStream, string fileName);
    Task<bool> DeleteVideoAsync(string videoUrl);
    Task<string> GetVideoUrlAsync(string fileName);
}

public class VideoStorageService : IVideoStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName;

    public VideoStorageService(IConfiguration configuration)
    {
        var connectionString = configuration["AzureStorage:ConnectionString"];
        _containerName = configuration["AzureStorage:ContainerName"] ?? "course-videos";

        if (!string.IsNullOrEmpty(connectionString))
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
        }
        else
        {
            throw new InvalidOperationException("Azure Storage connection string is not configured");
        }
    }

    public async Task<string> UploadVideoAsync(Stream videoStream, string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        var blobName = $"{Guid.NewGuid()}_{fileName}";
        var blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.UploadAsync(videoStream, new BlobHttpHeaders { ContentType = "video/mp4" });

        return blobClient.Uri.ToString();
    }

    public async Task<bool> DeleteVideoAsync(string videoUrl)
    {
        try
        {
            var uri = new Uri(videoUrl);
            var blobName = uri.Segments.Last();

            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            return await blobClient.DeleteIfExistsAsync();
        }
        catch
        {
            return false;
        }
    }

    public async Task<string> GetVideoUrlAsync(string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        if (await blobClient.ExistsAsync())
        {
            return blobClient.Uri.ToString();
        }

        throw new FileNotFoundException("Video not found");
    }
}
