using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Olympus.Api.Host.Services;

public static partial class DatabaseRegistrator {

	[GenerateServiceRegistrations(AssignableTo = typeof(IModel), CustomHandler = nameof(RegisterCompiledModel), AssemblyNameFilter = $"{AppSettings.AppBaseName}.*")]
	public static partial void AddDatabase(this IServiceCollection services);

	public static void RegisterCompiledModel<TModel>(this IServiceCollection services) where TModel : class, IModel {

		var instanceProperty = typeof(TModel).GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);

		if (instanceProperty is null) return;

		var modelInstance = instanceProperty.GetValue(null) as IModel;

		if (modelInstance is not null) services.AddSingleton(modelInstance);

	}

	public static void AddDatabase(this WebApplicationBuilder builder) {

		builder.Services.AddDatabase();

	}

}
