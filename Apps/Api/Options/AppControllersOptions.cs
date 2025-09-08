using Microsoft.AspNetCore.Mvc;

namespace Olympus.Api;

public class AppControllersOptions : IConfigureOptions<MvcOptions> {

	public void Configure(MvcOptions options) {

		options.Conventions.Add(new EntityControllerConvention());

	}

}
