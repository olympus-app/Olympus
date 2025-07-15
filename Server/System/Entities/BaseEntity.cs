using Olympus.Server.Authentication;

namespace Olympus.Server.System;

public abstract class BaseEntity : IBaseEntity {

	public Guid ID { get; protected set; } = new Guid();

	public Guid? CreatedBy { get; protected set; }
	public DateTime? CreatedAt { get; protected set; }
	public virtual User? CreatedUser { get; protected set; }

	public Guid? UpdatedBy { get; protected set; }
	public DateTime? UpdatedAt { get; protected set; }
	public virtual User? UpdatedUser { get; protected set; }

	public bool Active { get; protected set; } = true;

	public bool Hidden { get; protected set; } = false;

	public void SetCreated(Guid? userId, DateTime? createdAt = null) {

		CreatedBy ??= userId;
		CreatedAt ??= createdAt ?? DateTime.UtcNow;
		UpdatedBy ??= CreatedBy;
		UpdatedAt ??= CreatedAt;

	}

	public void SetUpdated(Guid? userId, DateTime? updatedAt = null) {

		UpdatedBy = userId;
		UpdatedAt = updatedAt ?? DateTime.UtcNow;
		CreatedBy ??= UpdatedBy;
		CreatedAt ??= UpdatedAt;

	}

	public void Activate() => Active = true;
	public void Deactivate() => Active = false;
	public void ToggleActive() => Active = !Active;

	public void Hide() => Hidden = true;
	public void Unhide() => Hidden = false;
	public void ToggleHidden() => Hidden = !Hidden;

}
