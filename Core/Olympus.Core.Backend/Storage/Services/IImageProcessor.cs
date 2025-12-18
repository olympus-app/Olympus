namespace Olympus.Core.Backend.Storage;

public interface IImageProcessor {

	public Task<Stream> ConvertAsync(Stream stream, int quality = 80, CancellationToken cancellationToken = default);

	public Task<Stream> ResizeAsync(Stream stream, int width, int height, ResizeMode mode = ResizeMode.Max, int quality = 80, CancellationToken cancellationToken = default);

	public Task<Dictionary<ThumbnailSize, Stream>> GenerateThumbnailsAsync(Stream stream, ResizeMode mode = ResizeMode.Max, int quality = 80, CancellationToken cancellationToken = default);

}
