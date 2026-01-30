namespace Olympus.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class ServiceLifecycleAnalyzer : DiagnosticAnalyzer {

	public const string Identifier = "OG0010";

	public const string Category = "Services";

	private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Strings.ServiceLifecycleTitle), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString Message = new LocalizableResourceString(nameof(Strings.ServiceLifecycleMessage), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Strings.ServiceLifecycleDescription), Strings.ResourceManager, typeof(Strings));

	private static readonly DiagnosticDescriptor Rule = new(Identifier, Title, Message, Category, DiagnosticSeverity.Warning, true, Description);

	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

	public override void Initialize(AnalysisContext context) {

		context.EnableConcurrentExecution();
		context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
		context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);

	}

	private void AnalyzeSymbol(SymbolAnalysisContext context) {

		var namedType = (INamedTypeSymbol)context.Symbol;

		if (namedType.TypeKind is not TypeKind.Class and not TypeKind.Struct and not TypeKind.Interface) return;

		var lifecyclesFound = new HashSet<string>();

		foreach (var @interface in namedType.AllInterfaces) {

			var originalDef = @interface.ConstructedFrom;
			var @namespace = originalDef.ContainingNamespace?.ToDisplayString();
			var name = originalDef.Name;

			if (@namespace != "Olympus.Core.Services") continue;

			if (name.StartsWith("ITransientService")) lifecyclesFound.Add("Transient");
			else if (name.StartsWith("IScopedService")) lifecyclesFound.Add("Scoped");
			else if (name.StartsWith("ISingletonService")) lifecyclesFound.Add("Singleton");
			else if (name.StartsWith("ISettingsService") || name.StartsWith("IOptionsService")) lifecyclesFound.Add("Options");
			else if (name == "IComplexService") lifecyclesFound.Add("Complex");

		}

		if (lifecyclesFound.Count > 1) {

			var conflictList = string.Join(", ", lifecyclesFound);
			var diagnostic = Diagnostic.Create(Rule, namedType.Locations[0], namedType.Name, conflictList);

			context.ReportDiagnostic(diagnostic);

		}

	}

}
