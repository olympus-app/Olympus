using System.Reflection;

namespace Olympus.Core.Kernel.Modularization;

public static class AppModuleOptionsExtensions {

	extension(IEnumerable<IAppModuleOptions> options) {

		public IAppModuleOptions? SelectFrom(Assembly assembly) {

			return options.FirstOrDefault(options => options.GetType().Assembly.GetRootNamespace(2) == assembly.GetRootNamespace(2));

		}

	}

}
