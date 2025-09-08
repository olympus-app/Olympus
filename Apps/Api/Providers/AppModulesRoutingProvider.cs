using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace Olympus.Api;

public class AppModulesRoutingProvider(IEnumerable<IAppModuleOptions> modules) : IApplicationModelProvider {

	public int Order => 99;

	public void OnProvidersExecuted(ApplicationModelProviderContext context) { }

	public void OnProvidersExecuting(ApplicationModelProviderContext context) {

		foreach (var controller in context.Result.Controllers) {

			var assemblyName = controller.ControllerType.Assembly.GetName().Name;
			if (string.IsNullOrEmpty(assemblyName)) continue;

			var nameParts = assemblyName.Split('.');
			if (nameParts.Length < 3 || nameParts[0] != AppConsts.AppName || nameParts[2] != AppLayer.Backend.ToString()) continue;

			var moduleName = nameParts[1];
			var moduleOptions = modules.FirstOrDefault(module => module.Enum.ToString().Equals(moduleName, StringComparison.OrdinalIgnoreCase));
			if (moduleOptions is null) continue;

			var prefix = moduleOptions.ApiPrefix;
			var attribute = new ODataRouteComponentAttribute(prefix);

			((List<object>)controller.Attributes).Add(attribute);

		}

	}

}
