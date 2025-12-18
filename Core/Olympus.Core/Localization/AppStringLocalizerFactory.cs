using System.Reflection;
using System.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Olympus.Core.Localization;

public class AppStringLocalizerFactory(ILoggerFactory loggerFactory) : IStringLocalizerFactory {

	private readonly IResourceNamesCache Cache = new ResourceNamesCache();

	public IStringLocalizer Create(Type resourceSource) {

		var sourceAssembly = resourceSource.Assembly;
		var hostAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

		var sourceResourceName = $"{sourceAssembly.GetName().Name}.{resourceSource.Name}";
		var hostResourceName = $"{hostAssembly.GetName().Name}.{resourceSource.Name}";

		var sourceResourceManager = new ResourceManager(sourceResourceName, sourceAssembly);
		var hostResourceManager = (ResourceManager?)null;

		if (hostAssembly.GetManifestResourceInfo(hostResourceName + ".resources") is not null) {

			hostResourceManager = new ResourceManager(hostResourceName, hostAssembly);

			try {

				if (hostResourceManager.GetResourceSet(AppCultureInfo.Invariant, true, true) is null) hostResourceManager = null;

			} catch {

				hostResourceManager = null;

			}

		}

		hostResourceManager ??= sourceResourceManager;

		return new AppStringLocalizer(sourceResourceManager, hostResourceManager);

	}

	public IStringLocalizer Create(string baseName, string location) {

		var assembly = Assembly.Load(new AssemblyName(location));
		var resourceManager = new ResourceManager(baseName, assembly);
		var logger = loggerFactory.CreateLogger(baseName);

		return new ResourceManagerStringLocalizer(resourceManager, assembly, baseName, Cache, logger);

	}

}
