namespace Olympus.Web;

public class FormFactor : IFormFactor
{

	public string GetFormFactor()
	{

		return "WebAssembly";

	}

	public string GetPlatform()
	{

		return Environment.OSVersion.ToString();

	}

}
