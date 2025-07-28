using Olympus.Frontend.Interface.Services;

namespace Olympus.Frontend.Native.Services;

public class FormFactor : IFormFactor {

	public string GetFormFactor() {

		return DeviceInfo.Idiom.ToString();

	}

	public string GetPlatform() {

		return DeviceInfo.Platform.ToString() + " - " + DeviceInfo.VersionString;

	}

}
