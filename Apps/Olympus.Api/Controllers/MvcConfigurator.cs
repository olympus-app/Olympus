using Microsoft.AspNetCore.Mvc;

namespace Olympus.Api.Controllers;

public class MvcConfigurator(IEnumerable<IAppModuleOptions> modules) : IConfigureOptions<MvcOptions> {

	public void Configure(MvcOptions options) {

		options.Filters.Add<ControllersResultFilter>();
		options.Conventions.Add(new EntityControllerConvention());
		options.Conventions.Add(new ModulesGroupsConvention(modules));

	}

}
