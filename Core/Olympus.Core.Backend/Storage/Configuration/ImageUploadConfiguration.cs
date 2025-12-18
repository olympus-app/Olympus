namespace Olympus.Core.Backend.Storage;

public record ImageUploadConfiguration : FileUploadConfiguration {

	public ImageSize ImageSize { get; set; } = ImageSize.Large;

	public ResizeMode ResizeMode { get; set; } = ResizeMode.Max;

	public bool GenerateThumbnails { get; set; } = true;

	public int CompressionQuality { get; set; } = 80;

}
