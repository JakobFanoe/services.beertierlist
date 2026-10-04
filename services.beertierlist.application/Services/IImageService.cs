namespace services.beertierlist.application.Services;

public interface IImageService
{
    Task<UploadedImage> UploadImage(Stream imageStream, string fileName, string contentType, CancellationToken cancellationToken);
    Task RemoveImage(string blobName, CancellationToken cancellationToken);
}
