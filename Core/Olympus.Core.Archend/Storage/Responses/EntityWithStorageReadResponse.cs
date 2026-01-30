namespace Olympus.Core.Archend.Storage;

public abstract record EntityWithStorageReadResponse<TStorageEntityReadResponse> : EntityReadResponse, IEntityWithStorageReadResponse<TStorageEntityReadResponse> where TStorageEntityReadResponse : class, IStorageEntityReadResponse {

	public Guid FileId { get; set; }

	public TStorageEntityReadResponse File { get; set; } = null!;

}
