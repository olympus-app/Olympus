namespace Olympus.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class SuffixNamingAnalyzer : DiagnosticAnalyzer {

	public const string Identifier = "OA0007";

	public const string Category = "Style";

	public const string SuffixProperty = "ExpectedSuffix";

	private const string ConfigKey = "olympus_suffix_naming_targets";

	private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Strings.SuffixNamingTitle), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString Message = new LocalizableResourceString(nameof(Strings.SuffixNamingMessage), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Strings.SuffixNamingDescription), Strings.ResourceManager, typeof(Strings));

	private static readonly DiagnosticDescriptor Rule = new(Identifier, Title, Message, Category, DiagnosticSeverity.Info, true, Description);

	private static readonly string[] DefaultSuffixes = ["Request", "Response"];

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

		if (!TryGetTargetSuffix(namedType.Name, suffixes, out var currentSuffix)) return;

		string? parentSuffix = null;

		if (namedType.BaseType is not null && TryGetTargetSuffix(namedType.BaseType.Name, suffixes, out var baseSuffix)) {

			parentSuffix = baseSuffix;

		}

		if (parentSuffix is null) {

			foreach (var iface in namedType.Interfaces) {

				if (TryGetTargetSuffix(iface.Name, suffixes, out var ifaceSuffix)) {

					parentSuffix = ifaceSuffix;

					break;

				}

			}

		}

		if (parentSuffix is null) return;

		if (string.Equals(currentSuffix, parentSuffix, StringComparison.Ordinal)) return;

		var baseName = namedType.Name.Substring(0, namedType.Name.Length - currentSuffix!.Length);

		var suggestedName = baseName + parentSuffix;

		var properties = ImmutableDictionary<string, string?>.Empty.Add(SuffixProperty, suggestedName);

		var diagnostic = Diagnostic.Create(Rule, namedType.Locations[0], properties, namedType.Name, parentSuffix);

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
