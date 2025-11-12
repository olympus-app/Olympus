using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace Olympus.Api.Routing;

public class RoutingProvider(IEnumerable<IAppModuleOptions> modules) : IApplicationModelProvider {

	public int Order => 99;

	public void OnProvidersExecuted(ApplicationModelProviderContext context) { }

	public void OnProvidersExecuting(ApplicationModelProviderContext context) {

		foreach (var controller in context.Result.Controllers) {

			var module = modules.SelectFrom(controller.ControllerType.Assembly);
			if (module is null) continue;

			var attribute = new ODataRouteComponentAttribute(module.ApiPrefix);

			((List<object>)controller.Attributes).Add(attribute);

		}

	}

}
