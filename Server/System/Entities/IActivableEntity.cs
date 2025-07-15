namespace Olympus.Server.System;

public interface IActivableEntity {

	public bool Active { get; }

	public void Activate();
	public void Deactivate();
	public void ToggleActive();

}
