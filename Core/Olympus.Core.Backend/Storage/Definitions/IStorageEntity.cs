namespace Olympus.Core.Backend.Storage;

public interface IStorageEntity : IEntity {

	public string Name { get; set; }

	public string BaseName { get; }

	public string Extension { get; }

	public string ContentType { get; set; }

	public StorageLocation StorageBucket { get; set; }

	public string StoragePath { get; set; }

	public long Size { get; set; }

}
