namespace Olympus.Core.Backend.Storage;

public abstract class EntityWithFile<TEntity, TFile> : Entity, IEntityWithFile<TEntity, TFile> where TEntity : class, IEntity where TFile : class, IFileEntity {

	public Guid EntityId { get; set; }

	public virtual TEntity Entity { get; set; } = null!;

	public Guid FileId { get; set; }

	public virtual TFile File { get; set; } = null!;

}
