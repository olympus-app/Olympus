namespace Olympus.Core.Backend;

public abstract class EntityModel<TEntity, TModel, TController> : IEntityModel<TEntity, TModel> where TEntity : class, IEntity where TModel : class, IModel where TController : class {

	protected abstract string ModulePrefix { get; }

	protected abstract void Configure(ODataModelBuilder builder);

	protected EntityTypeConfiguration<TModel> BaseConfiguration(ODataModelBuilder builder, string? name = null) {

		if (string.IsNullOrWhiteSpace(name)) name = typeof(TController).Name.Crop("Controller");

		var Entity = builder.EntitySet<TModel>(name).EntityType;

		Entity.HasKey(x => x.Id);
		Entity.HasRequired(x => x.CreatedBy).AutomaticallyExpand(true).Select(SelectExpandType.Automatic, nameof(UserAudit.Id), nameof(UserAudit.Name));
		Entity.HasRequired(x => x.UpdatedBy).AutomaticallyExpand(true).Select(SelectExpandType.Automatic, nameof(UserAudit.Id), nameof(UserAudit.Name));
		Entity.Property(x => x.Version).IsConcurrencyToken();
		Entity.Ignore(x => x.Version);
		Entity.Page(100, 100);

		return Entity;

	}

	public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix) {

		if (routePrefix != ModulePrefix) return;

		switch (apiVersion.MajorVersion) {

			default: Configure(builder); break;

		}

	}

}
