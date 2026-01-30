namespace Olympus.Core.Archend.Storage;

public interface IEntityWithStorageReadResponse<TStorageEntityReadResponse> : IEntityReadResponse where TStorageEntityReadResponse : class, IStorageEntityReadResponse {

	public Guid FileId { get; set; }

	public TStorageEntityReadResponse File { get; set; }

}
