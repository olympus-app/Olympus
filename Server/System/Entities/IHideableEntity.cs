namespace Olympus.Server.System;

public interface IHideableEntity {

	public bool Hidden { get; }

	public void Hide();
	public void Unhide();
	public void ToggleHidden();

}
