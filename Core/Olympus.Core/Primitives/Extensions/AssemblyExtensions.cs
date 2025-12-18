using System.Reflection;

namespace Olympus.Core.Primitives;

public static class AssemblyExtensions {

	extension(Assembly assembly) {

		public string? RootNamespace {

			get {

				var fullName = assembly.FullName;
				if (string.IsNullOrEmpty(fullName)) return null;

				var commaIndex = fullName.IndexOf(',');

				if (commaIndex > 0) return fullName.Substring(0, commaIndex);

				return fullName;

			}

		}

	}

}
