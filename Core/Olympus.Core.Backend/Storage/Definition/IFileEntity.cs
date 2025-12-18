namespace Olympus.Core.Backend.Storage;

public interface IFileEntity : IEntity {

	public string Name { get; set; }

	public string BaseName { get; set; }

	public string Extension { get; set; }

	public string ContentType { get; set; }

	public string ContentHash { get; set; }

	public string StorageBucket { get; set; }

	public string StoragePath { get; set; }

	public long Size { get; set; }

}
