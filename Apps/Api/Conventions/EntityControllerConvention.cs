using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Olympus.Api;

public class EntityControllerConvention : IApplicationModelConvention {

	public void Apply(ApplicationModel application) {

		foreach (var controller in application.Controllers) {

			var attribute = controller.ControllerType.GetCustomAttribute<EntityControllerAttribute>();

			if (attribute is not null) {

				var enabledActions = attribute.EnabledActions;
				var actionsToRemove = new List<ActionModel>();

				foreach (var action in controller.Actions) {

					var isEnabled = action.ActionName switch {
						"Get" => enabledActions.HasFlag(EntityControllerActions.Read),
						"Post" => enabledActions.HasFlag(EntityControllerActions.Create),
						"Put" => enabledActions.HasFlag(EntityControllerActions.Update),
						"Patch" => enabledActions.HasFlag(EntityControllerActions.Update),
						"Delete" => enabledActions.HasFlag(EntityControllerActions.Delete),
						_ => true
					};

					if (!isEnabled) actionsToRemove.Add(action);

				}

				foreach (var action in actionsToRemove) {

					controller.Actions.Remove(action);

				}

			}

		}

	}

}
