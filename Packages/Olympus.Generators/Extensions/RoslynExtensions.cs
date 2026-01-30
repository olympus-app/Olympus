namespace Olympus.Generators;

public static class RoslynExtensions {

	public static IEnumerable<IAssemblySymbol> GetSolutionAssemblies(this Compilation compilation, string solutionName) {

		var referenced = compilation.SourceModule.ReferencedAssemblySymbols;

		foreach (var assembly in referenced) {

			if (assembly.Name.StartsWith(solutionName, StringComparison.OrdinalIgnoreCase)) {

				yield return assembly;

			}

		}

		yield return compilation.Assembly;

	}

	public static IEnumerable<INamedTypeSymbol> GetAllTypes(this IAssemblySymbol assembly) {

		var stack = new Stack<INamespaceSymbol>();

		stack.Push(assembly.GlobalNamespace);

		while (stack.Count > 0) {

			var currentNamespace = stack.Pop();

			foreach (var member in currentNamespace.GetMembers()) {

				if (member is INamespaceSymbol ns) {

					stack.Push(ns);

				} else if (member is INamedTypeSymbol type) {

					yield return type;

				}

			}

		}

	}

	public static bool Implements(this ITypeSymbol type, INamedTypeSymbol? interfaceSymbol) {

		if (interfaceSymbol is null) return false;

		foreach (var iface in type.AllInterfaces) {

			if (SymbolEqualityComparer.Default.Equals(iface, interfaceSymbol)) return true;

			if (interfaceSymbol.IsGenericType && SymbolEqualityComparer.Default.Equals(iface.ConstructedFrom, interfaceSymbol)) return true;

		}

		return false;

	}

	public static bool IsConcreteClass(this INamedTypeSymbol type) {

		return type.TypeKind == TypeKind.Class && !type.IsAbstract;

	}

}
