using System.Reflection;

namespace Olympus.Core.Modules;

public static class AppModuleOptionsExtensions {

	extension(IEnumerable<IAppModuleOptions> options) {

		public IAppModuleOptions? GetByName(string name) {

			return options.FirstOrDefault(options => string.Equals(name, options.ModuleName, StringComparison.OrdinalIgnoreCase));

		}

		public IAppModuleOptions? GetByAssembly(Assembly assembly) {

			return options.FirstOrDefault(options => options.GetType().Assembly.RootNamespace?.Slices(-1, '.') == assembly.RootNamespace?.Slices(-1, '.'));

		}

	}

}
