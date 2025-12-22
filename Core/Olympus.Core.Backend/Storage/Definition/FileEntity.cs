namespace Olympus.Core.Backend.Storage;

public abstract class FileEntity : Entity, IFileEntity {

	public string Name { get; set; } = string.Empty;

	public string BaseName { get; set; } = string.Empty;

	public string Extension { get; set; } = string.Empty;

	public string ContentType { get; set; } = string.Empty;

	public string StorageBucket { get; set; } = string.Empty;

	public string StoragePath { get; set; } = string.Empty;

	public long Size { get; set; }

}
