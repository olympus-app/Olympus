namespace Olympus.Core.Archend.Storage;

public interface IFileQueryResponse : IEntityQueryResponse {

	public string Extension { get; init; }

	public long Size { get; init; }

}
