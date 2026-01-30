namespace Olympus.Core.Settings;

public class PasskeysSettings : Settings {

	public override void Configure(IConfiguration configuration) {

		configuration.Bind("Passkeys", this);

	}

}
