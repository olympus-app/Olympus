namespace Olympus.Api.Endpoints;

public static class EndpointsRegistrator {

	public static void AddEndpointsServices(this WebApplicationBuilder builder) {

		builder.Services.AddFastEndpoints();

	}

	public static void MapEndpoints(this WebApplication app) {

		app.UseFastEndpoints(options => {

			options.Endpoints.ShortNames = true;
			options.Endpoints.RoutePrefix = Routes.Api;
			options.Endpoints.NameGenerator = static context => context.EndpointType.Name.Crop("Endpoint");

			options.Versioning.DefaultVersion = 1;
			options.Versioning.PrependToRoute = true;

			options.Security.NameClaimType = AppClaimsTypes.Name;
			options.Security.RoleClaimType = AppClaimsTypes.Role;
			options.Security.ScopeClaimType = AppClaimsTypes.Scope;
			options.Security.PermissionsClaimType = AppClaimsTypes.Permission;

			options.Errors.StatusCode = 400;
			options.Errors.ContentType = ContentTypes.Json;
			options.Errors.ProducesMetadataType = typeof(ProblemResult);
			options.Errors.ResponseBuilder = static (failures, context, statusCode) => {
				return new ProblemResult() {
					Status = statusCode,
					Message = ((HttpStatusCode)statusCode).Humanized,
					Details = failures.Count == 1 ? failures[0].ErrorMessage : null,
					Errors = failures.Count > 1 ? failures.Select(static failure => new Core.Archend.Endpoints.ErrorResult() {
						Origin = failure.PropertyName,
						Message = failure.ErrorMessage,
					}) : null,
				};
			};

		});

	}

}
