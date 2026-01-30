namespace Olympus.Core.Backend.Endpoints;

public static class EndpointsExtensions {

	extension(BaseEndpoint endpoint) {

		public void AddPermissionsRequirement(int? one = null, int[]? any = null, int[]? all = null) {

			var requirements = new PermissionsRequirements(one, any, all);

			endpoint.Definition.Policy(builder => builder.AddRequirements(requirements));

		}

	}

}
