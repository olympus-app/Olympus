using Android.App;
using Android.Runtime;

namespace Olympus.Native.Host;

[Application]
public class MainApplication(IntPtr handle, JniHandleOwnership ownership) : MauiApplication(handle, ownership) {

	protected override MauiApp CreateMauiApp() => NativeHost.CreateMauiApp();

}
