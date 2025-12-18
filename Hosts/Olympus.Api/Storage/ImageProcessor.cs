using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Olympus.Api.Storage;

public class ImageProcessor : IImageProcessor {

	private static SixLabors.ImageSharp.Processing.ResizeMode GetResizeMode(Core.Backend.Storage.ResizeMode mode) {

		return mode switch {
			Core.Backend.Storage.ResizeMode.Stretch => SixLabors.ImageSharp.Processing.ResizeMode.Stretch,
			Core.Backend.Storage.ResizeMode.Crop => SixLabors.ImageSharp.Processing.ResizeMode.Crop,
			Core.Backend.Storage.ResizeMode.Pad => SixLabors.ImageSharp.Processing.ResizeMode.Pad,
			Core.Backend.Storage.ResizeMode.Min => SixLabors.ImageSharp.Processing.ResizeMode.Min,
			Core.Backend.Storage.ResizeMode.Max => SixLabors.ImageSharp.Processing.ResizeMode.Max,
			_ => SixLabors.ImageSharp.Processing.ResizeMode.Max,
		};

	}

	private static async Task<Stream> SaveToStreamAsync(Image image, int quality, CancellationToken cancellationToken) {

		var stream = new MemoryStream();
		var encoder = new JpegEncoder() { Quality = quality };

		await image.SaveAsync(stream, encoder, cancellationToken);

		stream.Position = 0;

		return stream;

	}

	public async Task<Stream> ConvertAsync(Stream stream, int quality = 80, CancellationToken cancellationToken = default) {

		stream.ResetPosition();

		using var image = await Image.LoadAsync(stream, cancellationToken);

		return await SaveToStreamAsync(image, quality, cancellationToken);

	}

	public async Task<Stream> ResizeAsync(Stream stream, int width, int height, Core.Backend.Storage.ResizeMode mode = Core.Backend.Storage.ResizeMode.Max, int quality = 80, CancellationToken cancellationToken = default) {

		stream.ResetPosition();

		using var image = await Image.LoadAsync(stream, cancellationToken);

		var scale = Math.Min(1.0, Math.Min((double)image.Width / width, (double)image.Height / height));

		var newWidth = (int)(width * scale);
		var newHeight = (int)(height * scale);

		image.Mutate(context => context.Resize(
			new ResizeOptions() {
				Size = new Size(newWidth, newHeight),
				Mode = GetResizeMode(mode),
				PadColor = Color.White,

			}
		));

		return await SaveToStreamAsync(image, quality, cancellationToken);

	}

	public async Task<Dictionary<ThumbnailSize, Stream>> GenerateThumbnailsAsync(Stream stream, Core.Backend.Storage.ResizeMode mode = Core.Backend.Storage.ResizeMode.Crop, int quality = 80, CancellationToken cancellationToken = default) {

		stream.ResetPosition();

		using var image = await Image.LoadAsync(stream, cancellationToken);

		var thumbnails = new Dictionary<ThumbnailSize, Stream>();

		foreach (var size in FastEnum.GetValues<ThumbnailSize>()) {

			var targetSize = size.Value;

			using var thumbImage = image.Clone(context => context.Resize(
				new ResizeOptions() {
					Size = new Size(targetSize, targetSize),
					Mode = GetResizeMode(mode),
				}
			));

			var thumbnail = await SaveToStreamAsync(thumbImage, quality, cancellationToken);
			thumbnails.Add(size, thumbnail);

		}

		return thumbnails;

	}

}
