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
		var resourceName = $"{sourceAssembly.GetName().Name}.{resourceSource.Name}";
		var sourceResourceManager = new ResourceManager(resourceName, sourceAssembly);
		var hostResourceManager = (ResourceManager?)null;

		if (hostAssembly.GetManifestResourceNames().Contains(resourceName + ".resources")) {

			var manager = new ResourceManager(resourceName, hostAssembly);

			if (manager.GetResourceSet(System.Globalization.CultureInfo.InvariantCulture, true, true) is not null) {

				hostResourceManager = manager;

			}

		}

		return new AppStringLocalizer(sourceResourceManager, hostResourceManager);

	}

	public IStringLocalizer Create(string baseName, string location) {

		var assembly = Assembly.Load(new AssemblyName(location));
		var resourceManager = new ResourceManager(baseName, assembly);
		var logger = loggerFactory.CreateLogger(baseName);

		return new ResourceManagerStringLocalizer(resourceManager, assembly, baseName, Cache, logger);

	}

}
