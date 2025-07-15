namespace Olympus.Server.System;

public interface IBaseEntity : IAuditableEntity, IActivableEntity, IHideableEntity {

	public Guid ID { get; }

}
