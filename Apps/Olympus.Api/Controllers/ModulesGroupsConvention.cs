using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Olympus.Api.Controllers;

public class ModulesGroupsConvention(IEnumerable<IAppModuleOptions> modules) : IControllerModelConvention {

	public void Apply(ControllerModel controller) {

		var module = modules.SelectFrom(controller.ControllerType.Assembly);
		if (module is null) return;

		controller.ApiExplorer.GroupName = module.Name;

	}

}
