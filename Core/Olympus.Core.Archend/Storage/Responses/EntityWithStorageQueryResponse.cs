namespace Olympus.Core.Archend.Storage;

public abstract record EntityWithStorageQueryResponse<TStorageEntityQueryResponse> : EntityQueryResponse, IEntityWithStorageQueryResponse<TStorageEntityQueryResponse> where TStorageEntityQueryResponse : class, IStorageEntityQueryResponse {

	public Guid FileId { get; set; }

	public TStorageEntityQueryResponse File { get; set; } = null!;

}
