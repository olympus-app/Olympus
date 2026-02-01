namespace Olympus.Core.Backend.Storage;

public interface IStorageEntity : IEntity {

	public string Name { get; set; }

	public string BaseName { get; }

	public string Extension { get; }

	public string ContentType { get; set; }

	public StorageLocation Bucket { get; set; }

	public string Path { get; set; }

	public long Size { get; set; }

}
