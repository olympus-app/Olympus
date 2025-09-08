using Foundation;

namespace Olympus.Native.Host;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate {

	protected override MauiApp CreateMauiApp() => NativeHost.CreateMauiApp();

}
