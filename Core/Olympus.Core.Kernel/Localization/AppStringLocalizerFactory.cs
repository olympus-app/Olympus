using System.Globalization;
using System.Reflection;
using System.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Olympus.Core.Kernel.Localization;

public class AppStringLocalizerFactory(ILoggerFactory loggerFactory) : IStringLocalizerFactory {

	private readonly IResourceNamesCache cache = new ResourceNamesCache();

	public IStringLocalizer Create(Type resourceSource) {

		var sourceAssembly = resourceSource.Assembly;
		var hostAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

		var sourceResourceName = string.Join('.', sourceAssembly.GetName().Name, resourceSource.Name);
		var hostResourceName = string.Join('.', hostAssembly.GetName().Name, sourceResourceName);

		var sourceResourceManager = new ResourceManager(sourceResourceName, sourceAssembly);
		var hostResourceManager = (ResourceManager?)null;

		if (hostAssembly.GetManifestResourceNames().Contains(hostResourceName + ".resources")) {

			hostResourceManager = new ResourceManager(hostResourceName, hostAssembly);
			if (hostResourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, true) is null) hostResourceManager = null;

		}

		hostResourceManager ??= sourceResourceManager;

		return new AppStringLocalizer(sourceResourceManager, hostResourceManager);

	}

	public IStringLocalizer Create(string baseName, string location) {

		var assembly = Assembly.Load(new AssemblyName(location));
		var resourceManager = new ResourceManager(baseName, assembly);
		var logger = loggerFactory.CreateLogger(baseName);

		return new ResourceManagerStringLocalizer(resourceManager, assembly, baseName, cache, logger);

	}

}
