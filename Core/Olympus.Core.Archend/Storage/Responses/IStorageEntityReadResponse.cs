namespace Olympus.Core.Archend.Storage;

public interface IStorageEntityReadResponse : IEntityReadResponse {

	public string Name { get; init; }

	public string ContentType { get; init; }

	public string Extension { get; init; }

	public long Size { get; init; }

}
