namespace Olympus.Core.Archend.Storage;

public interface IFileListResponse : IEntityListResponse {

	public string Extension { get; init; }

	public long Size { get; init; }

}
