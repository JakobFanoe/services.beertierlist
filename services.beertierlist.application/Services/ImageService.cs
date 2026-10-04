using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace services.beertierlist.application.Services;

public class ImageService(BlobServiceClient blobServiceClient) : IImageService
{
    private static readonly string ContainerName = "images";

    public Task RemoveImage(string blobName, CancellationToken cancellationToken)
    {
        var container = blobServiceClient.GetBlobContainerClient(ContainerName);

        var blob = container.GetBlobClient(blobName);

        return blob.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }

    public async Task<UploadedImage> UploadImage(Stream imageStream, string fileName, string contentType, CancellationToken cancellationToken)
    {
        var container = blobServiceClient.GetBlobContainerClient(ContainerName);

        await container.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

        var extension = Path.GetExtension(fileName);
        var blobName = $"{Guid.CreateVersion7():N}{extension}";

        var blob = container.GetBlobClient(blobName);

        await blob.UploadAsync(
            imageStream,
            new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = contentType
                }
            },
            cancellationToken);

        return new UploadedImage(blob.Uri.ToString(), blobName);
    }
}
