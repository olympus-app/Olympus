using System.Reflection;

namespace Olympus.Core.Kernel.Primitives;

public static class AssemblyExtensions {

	extension(Assembly assembly) {

		public string? RootNamespace => assembly.GetName().Name;

		public string? GetRootNamespace(int parts = 0) {

			var name = assembly.GetName().Name;

			if (string.IsNullOrEmpty(name)) return null;

			var nameParts = name.Split('.');

			return parts <= 0 || parts >= nameParts.Length ? name : string.Join('.', nameParts.Take(parts));

		}

	}

}
