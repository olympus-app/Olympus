namespace Olympus.Core.Backend.Storage;

public abstract class EntityWithStorage<TStorageEntity> : Entity, IEntityWithStorage<TStorageEntity> where TStorageEntity : class, IStorageEntity {

	public Guid FileId { get; set; }

	public virtual TStorageEntity File { get; set; } = null!;

}
