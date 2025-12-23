namespace Olympus.Core.Settings;

[Flags]
public enum AppHostType {

	None = 0,

	Api = 1,

	Web = 2,

	All = Api | Web,

}
