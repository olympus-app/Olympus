using System.Reflection;
using System.Runtime.CompilerServices;

namespace Olympus.Core.Modules;

public static class AppModuleExtensions {

	private const string AppBaseName = AppSettings.AppBaseName;

	private static readonly int AppBaseNameLength = AppBaseName.Length;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool IsStandardLayer(string? layerName) {

		return layerName is nameof(AppModuleLayerType.Archend) or nameof(AppModuleLayerType.Backend) or nameof(AppModuleLayerType.Frontend);

	}

	private static string? GetSegment(string? namespaceString, int segmentIndex) {

		if (string.IsNullOrEmpty(namespaceString)) return null;

		var span = namespaceString.AsSpan();
		if (!span.StartsWith(AppBaseName, StringComparison.Ordinal)) return null;
		if (span.Length <= AppBaseNameLength) return null;

		var currentSlice = span.Slice(AppBaseNameLength + 1);

		var targetRelativeIndex = segmentIndex - 1;

		for (var i = 0; i < targetRelativeIndex; i++) {

			var dotIndex = currentSlice.IndexOf('.');
			if (dotIndex == -1) return null;

			currentSlice = currentSlice.Slice(dotIndex + 1);

		}

		var endDot = currentSlice.IndexOf('.');

		if (endDot == -1) return currentSlice.ToString();

		return currentSlice.Slice(0, endDot).ToString();

	}

	private static bool TryExtractModuleAndLayer(string? @namespace, out string? module, out string? layer) {

		module = null;
		layer = null;

		if (string.IsNullOrEmpty(@namespace)) return false;
		var span = @namespace.AsSpan();

		if (!span.StartsWith(AppBaseName, StringComparison.Ordinal) || span.Length <= AppBaseNameLength) return false;

		var remainder = span.Slice(AppBaseNameLength + 1);
		var firstDot = remainder.IndexOf('.');
		if (firstDot == -1) return false;

		module = remainder.Slice(0, firstDot).ToString();
		remainder = remainder.Slice(firstDot + 1);

		var secondDot = remainder.IndexOf('.');

		layer = secondDot == -1 ? remainder.ToString() : remainder.Slice(0, secondDot).ToString();

		return true;

	}

	public static IEnumerable<Assembly> GetModulesAssemblies(this AppDomain domain, AppModuleLayerType? layer = null) {

		var layerSuffix = layer.HasValue ? "." + layer.Value.Name : null;

		foreach (var assembly in domain.GetAssemblies()) {

			if (assembly.IsDynamic) continue;

			var fullName = assembly.FullName;
			if (fullName is null) continue;

			var span = fullName.AsSpan();

			var commaIndex = span.IndexOf(',');
			if (commaIndex > 0) span = span.Slice(0, commaIndex);

			if (!span.StartsWith(AppBaseName, StringComparison.Ordinal)) continue;

			if (layerSuffix is not null && !span.EndsWith(layerSuffix, StringComparison.Ordinal)) continue;

			yield return assembly;

		}

	}

	public static string? GetModuleName(this Type type) => GetSegment(type.Namespace, segmentIndex: 1);

	public static string? GetModuleName(this Assembly assembly) => GetSegment(assembly.RootNamespace, segmentIndex: 1);

	public static string? GetLayerName(this Type type) => GetSegment(type.Namespace, segmentIndex: 2);

	public static string? GetLayerName(this Assembly assembly) => GetSegment(assembly.RootNamespace, segmentIndex: 2);

	public static HashSet<string> GetModulesNames(this AppDomain domain, AppModuleLayerType? layer = null) {

		return domain.GetModulesAssemblies(layer).GetModulesNames(layer);

	}

	public static HashSet<string> GetModulesNames(this IEnumerable<Type> types, AppModuleLayerType? layer = null) {

		var modules = new HashSet<string>();

		var targetLayer = layer?.Name;
		var filterByLayer = layer.HasValue;

		foreach (var type in types) {

			if (TryExtractModuleAndLayer(type.Namespace, out var modName, out var layName)) {

				if (filterByLayer) {

					if (layName != targetLayer) continue;

				} else if (!IsStandardLayer(layName)) {

					continue;

				}

				modules.Add(modName!);

			}

		}

		return modules;

	}

	public static HashSet<string> GetModulesNames(this IEnumerable<Assembly> assemblies, AppModuleLayerType? layer = null) {

		var modules = new HashSet<string>();

		var targetLayer = layer?.Name;
		var filterByLayer = layer.HasValue;

		foreach (var assembly in assemblies) {

			if (TryExtractModuleAndLayer(assembly.RootNamespace, out var modName, out var layName)) {

				if (filterByLayer) {

					if (layName != targetLayer) continue;

				} else if (!IsStandardLayer(layName)) {

					continue;

				}

				modules.Add(modName!);

			}

		}

		return modules;

	}

}
