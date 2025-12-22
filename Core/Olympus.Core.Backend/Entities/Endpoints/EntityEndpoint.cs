namespace Olympus.Core.Backend.Entities;

public abstract class EntityEndpoint<TEntity> : BaseEndpoint where TEntity : class, IEntity {

	public IEntityDatabase Database { get; set; } = default!;

	protected void CheckPermissions(int? one = null, int[]? any = null, int[]? all = null) => this.AddPermissionsRequirement(one, any, all);

}

public abstract class EntityEndpoint<TEntity, TRequest> : Endpoint<TRequest> where TEntity : class, IEntity where TRequest : class, IEntityRequest {

	public IEntityDatabase Database { get; set; } = default!;

	protected void CheckPermissions(int? one = null, int[]? any = null, int[]? all = null) => this.AddPermissionsRequirement(one, any, all);

}

public abstract class EntityEndpoint<TEntity, TRequest, TResponse> : Endpoint<TRequest, TResponse> where TEntity : class, IEntity where TRequest : class, IEntityRequest where TResponse : class, IEntityResponse {

	public IEntityDatabase Database { get; set; } = default!;

	protected void CheckPermissions(int? one = null, int[]? any = null, int[]? all = null) => this.AddPermissionsRequirement(one, any, all);

}

public abstract class EntityEndpoint<TEntity, TRequest, TResponse, TMapper> : Endpoint<TRequest, TResponse, TMapper> where TEntity : class, IEntity where TRequest : class, IEntityRequest where TResponse : class, IEntityResponse where TMapper : class, IMapper {

	public IEntityDatabase Database { get; set; } = default!;

	protected void CheckPermissions(int? one = null, int[]? any = null, int[]? all = null) => this.AddPermissionsRequirement(one, any, all);

}

public abstract class EntityEndpointWithoutRequest<TEntity> : EndpointWithoutRequest where TEntity : class, IEntity {

	public IEntityDatabase Database { get; set; } = default!;

	protected void CheckPermissions(int? one = null, int[]? any = null, int[]? all = null) => this.AddPermissionsRequirement(one, any, all);

}

public abstract class EntityEndpointWithoutRequest<TEntity, TResponse> : EndpointWithoutRequest<TResponse> where TEntity : class, IEntity where TResponse : class, IEntityResponse {

	public IEntityDatabase Database { get; set; } = default!;

	protected void CheckPermissions(int? one = null, int[]? any = null, int[]? all = null) => this.AddPermissionsRequirement(one, any, all);

}

public abstract class EntityEndpointWithoutRequest<TEntity, TResponse, TMapper> : EndpointWithoutRequest<TResponse, TMapper> where TEntity : class, IEntity where TResponse : class, IEntityResponse where TMapper : class, IResponseMapper {

	public IEntityDatabase Database { get; set; } = default!;

	protected void CheckPermissions(int? one = null, int[]? any = null, int[]? all = null) => this.AddPermissionsRequirement(one, any, all);

}
