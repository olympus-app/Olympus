#pragma warning disable OA0007

using Microsoft.AspNetCore.Builder;

namespace Olympus.Core.Backend.Endpoints;

public abstract class Endpoint : EndpointWithoutRequest {

	protected void Prefix(string prefix) => RoutePrefixOverride(prefix.Trim('/'));

	protected void CheckPermissions(int? one = null, int[]? any = null, int[]? all = null) => this.AddPermissionsRequirement(one, any, all);

	protected void DisableAntiforgery() => Options(static builder => builder.WithMetadata(new IgnoreAntiforgeryTokenAttribute()));

	public abstract class WithRequest<TRequest> : Endpoint<TRequest> where TRequest : IRequest {

		protected void Prefix(string prefix) => RoutePrefixOverride(prefix.Trim('/'));

		protected void CheckPermissions(int? one = null, int[]? any = null, int[]? all = null) => this.AddPermissionsRequirement(one, any, all);

		protected void DisableAntiforgery() => Options(static builder => builder.WithMetadata(new IgnoreAntiforgeryTokenAttribute()));

		public abstract class WithResponse<TResponse> : Endpoint<TRequest, TResponse> where TResponse : IResponse {

			protected void Prefix(string prefix) => RoutePrefixOverride(prefix.Trim('/'));

			protected void CheckPermissions(int? one = null, int[]? any = null, int[]? all = null) => this.AddPermissionsRequirement(one, any, all);

			protected void DisableAntiforgery() => Options(static builder => builder.WithMetadata(new IgnoreAntiforgeryTokenAttribute()));

		}

	}

	public abstract class WithResponse<TResponse> : EndpointWithoutRequest<TResponse> where TResponse : IResponse {

		protected void Prefix(string prefix) => RoutePrefixOverride(prefix.Trim('/'));

		protected void CheckPermissions(int? one = null, int[]? any = null, int[]? all = null) => this.AddPermissionsRequirement(one, any, all);

		protected void DisableAntiforgery() => Options(static builder => builder.WithMetadata(new IgnoreAntiforgeryTokenAttribute()));

	}

}

#pragma warning restore OA0007
