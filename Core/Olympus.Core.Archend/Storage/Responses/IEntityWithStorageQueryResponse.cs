namespace Olympus.Core.Archend.Storage;

public interface IEntityWithStorageQueryResponse<TStorageEntityQueryResponse> : IEntityQueryResponse where TStorageEntityQueryResponse : class, IStorageEntityQueryResponse {

	public Guid FileId { get; set; }

	public TStorageEntityQueryResponse File { get; set; }

}
