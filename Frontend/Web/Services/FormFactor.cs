using Olympus.Frontend.Interface.Services;

namespace Olympus.Frontend.Web.Services;

public class FormFactor : IFormFactor {

	public string GetFormFactor() {
		return "WebAssembly";
	}

	public string GetPlatform() {
		return Environment.OSVersion.ToString();
	}

}
