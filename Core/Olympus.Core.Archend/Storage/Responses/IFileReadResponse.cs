namespace Olympus.Core.Archend.Storage;

public interface IFileReadResponse : IEntityReadResponse {

	public string Extension { get; init; }

	public long Size { get; init; }

}
