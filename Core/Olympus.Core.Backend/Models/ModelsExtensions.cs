namespace Olympus.Core.Backend.Models;

public static class ModelsExtensions {

	extension(NavigationPropertyConfiguration configuration) {

		public NavigationPropertyConfiguration AutoExpand() {

			return configuration.AutomaticallyExpand(true); ;

		}

		public PropertyConfiguration AutoSelect(params string[] properties) {

			var camelizedProperties = properties.Select(p => p.Camelize()).ToArray();

			return configuration.Select(SelectExpandType.Automatic, camelizedProperties);

		}

	}

}
