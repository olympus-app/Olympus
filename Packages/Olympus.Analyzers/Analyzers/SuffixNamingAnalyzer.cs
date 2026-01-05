using Olympus.Analyzers.Localization;

namespace Olympus.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class SuffixNamingAnalyzer : DiagnosticAnalyzer {

	public const string Identifier = "OL0007";

	public const string Category = "Naming";

	public const string SuffixProperty = "ExpectedSuffix";

	private const string ConfigKey = "olympus_suffix_naming_targets";

	private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Strings.SuffixNamingTitle), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString Message = new LocalizableResourceString(nameof(Strings.SuffixNamingMessage), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Strings.SuffixNamingDescription), Strings.ResourceManager, typeof(Strings));

	private static readonly DiagnosticDescriptor Rule = new(Identifier, Title, Message, Category, DiagnosticSeverity.Info, true, Description);

	private static readonly string[] DefaultSuffixes = ["Settings", "Options", "Model", "Request", "Response", "Result", "Mapper", "Endpoint"];

	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

	public override void Initialize(AnalysisContext context) {

		context.EnableConcurrentExecution();
		context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
		context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);

	}

	private void AnalyzeSymbol(SymbolAnalysisContext context) {

		var namedType = (INamedTypeSymbol)context.Symbol;
		if (namedType.IsImplicitlyDeclared) return;

		var suffixes = GetConfiguredSuffixes(context, namedType);

		string? requiredSuffix = null;

		if (namedType.BaseType is not null && TryGetTargetSuffix(namedType.BaseType.Name, suffixes, out var baseSuffix)) requiredSuffix = baseSuffix;

		if (requiredSuffix is null) {

			foreach (var iface in namedType.Interfaces) {

				if (TryGetTargetSuffix(iface.Name, suffixes, out var ifaceSuffix)) {

					requiredSuffix = ifaceSuffix;
					break;

				}

			}

		}

		if (requiredSuffix is null) return;

		if (namedType.Name.EndsWith(requiredSuffix, StringComparison.Ordinal)) return;

		var baseName = namedType.Name;

		foreach (var suffix in suffixes) {

			if (baseName.EndsWith(suffix, StringComparison.Ordinal)) {

				baseName = baseName.Substring(0, baseName.Length - suffix.Length);
				break;

			}

		}

		var suggestedName = baseName + requiredSuffix;

		var properties = ImmutableDictionary<string, string?>.Empty.Add(SuffixProperty, suggestedName);

		var diagnostic = Diagnostic.Create(Rule, namedType.Locations[0], properties, namedType.Name, requiredSuffix);

		context.ReportDiagnostic(diagnostic);

	}

	private static string[] GetConfiguredSuffixes(SymbolAnalysisContext context, INamedTypeSymbol symbol) {

		var syntaxTree = symbol.Locations.FirstOrDefault()?.SourceTree;
		if (syntaxTree is null) return DefaultSuffixes;

		var options = context.Options.AnalyzerConfigOptionsProvider.GetOptions(syntaxTree);

		if (options.TryGetValue(ConfigKey, out var configValue) && !string.IsNullOrWhiteSpace(configValue)) {

			return configValue.Split([','], StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();

		}

		return DefaultSuffixes;

	}

	private static bool TryGetTargetSuffix(string name, string[] targets, out string? suffix) {

		foreach (var target in targets) {

			if (name.EndsWith(target, StringComparison.Ordinal)) {

				suffix = target;
				return true;

			}

		}

		suffix = null;

		return false;

	}

}
