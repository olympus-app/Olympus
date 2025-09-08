using System.Globalization;
using System.Reflection;
using System.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Olympus.Kernel;

public class AppStringLocalizerFactory(ILoggerFactory loggerFactory) : IStringLocalizerFactory {

	private readonly IResourceNamesCache cache = new ResourceNamesCache();

	public IStringLocalizer Create(Type resourceSource) {

		var sourceAssembly = resourceSource.Assembly;
		var hostAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

		var resourceBaseName = sourceAssembly.GetName().Name + ".Resources.Localization." + resourceSource.Name;

		var hostResourceManager = (ResourceManager?)null;

		if (hostAssembly.GetManifestResourceNames().Contains(resourceBaseName + ".resources")) {

			hostResourceManager = new ResourceManager(resourceBaseName, hostAssembly);
			if (hostResourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, true) == null) hostResourceManager = null;

		}

		var originalResourceManager = new ResourceManager(resourceBaseName, sourceAssembly);

		hostResourceManager ??= originalResourceManager;

		return new AppStringLocalizer(hostResourceManager, originalResourceManager);

	}

	public IStringLocalizer Create(string baseName, string location) {

		var assembly = Assembly.Load(new AssemblyName(location));
		var resourceManager = new ResourceManager(baseName, assembly);
		var logger = loggerFactory.CreateLogger(baseName);

		return new ResourceManagerStringLocalizer(resourceManager, assembly, baseName, cache, logger);

	}

}
