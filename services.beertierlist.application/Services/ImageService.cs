using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

namespace services.beertierlist.application.Services;

public class ImageService(BlobServiceClient blobServiceClient) : IImageService
{
    private static readonly string ContainerName = "images";
    private static readonly TimeSpan ReadSasLifetime = TimeSpan.FromHours(2);

    public Task RemoveImage(string blobName, CancellationToken cancellationToken)
    {
        var container = blobServiceClient.GetBlobContainerClient(ContainerName);

        var blob = container.GetBlobClient(blobName);

        return blob.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }

    public string GenerateReadSasUri(string blobName)
    {
        var blob = blobServiceClient.GetBlobContainerClient(ContainerName).GetBlobClient(blobName);
        if (!blob.CanGenerateSasUri)
            throw new InvalidOperationException("Blob storage credentials must support shared key authorization to generate image SAS URLs.");

        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = ContainerName,
            BlobName = blobName,
            Resource = "b",
            Protocol = SasProtocol.Https,
            ExpiresOn = DateTimeOffset.UtcNow.Add(ReadSasLifetime)
        };
        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        return blob.GenerateSasUri(sasBuilder).ToString();
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
