namespace Olympus.Core.Backend.Storage;

public record FileUploadConfiguration : IFileUploadConfiguration {

	public string? FileName { get; set; }

	public string? FolderName { get; set; }

	public string? ContentType { get; set; }

	public StorageLocation Bucket { get; set; } = StorageLocation.Private;

}
