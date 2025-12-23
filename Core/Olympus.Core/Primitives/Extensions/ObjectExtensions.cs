using System.Reflection;

namespace Olympus.Core.Primitives;

public static class ObjectExtensions {

	extension(object obj) {

		public Assembly GetAssembly() {

			return obj.GetType().Assembly;

		}

	}

}
