namespace Olympus.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class LambdaParameterNamingAnalyzer : DiagnosticAnalyzer {

	public const string Identifier = "OA0005";

	public const string Category = "Semantics";

	public const string PropertyName = "SuggestedName";

	private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Strings.LambdaParameterNamingTitle), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString Message = new LocalizableResourceString(nameof(Strings.LambdaParameterNamingMessage), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Strings.LambdaParameterNamingDescription), Strings.ResourceManager, typeof(Strings));

	private static readonly DiagnosticDescriptor Rule = new(Identifier, Title, Message, Category, DiagnosticSeverity.Info, true, Description);

	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

	public override void Initialize(AnalysisContext context) {

		context.EnableConcurrentExecution();
		context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
		context.RegisterSyntaxNodeAction(AnalyzeSimpleLambda, SyntaxKind.SimpleLambdaExpression);
		context.RegisterSyntaxNodeAction(AnalyzeParenthesizedLambda, SyntaxKind.ParenthesizedLambdaExpression);

	}

	private void AnalyzeSimpleLambda(SyntaxNodeAnalysisContext context) {

		var lambda = (SimpleLambdaExpressionSyntax)context.Node;
		AnalyzeParameter(context, lambda.Parameter);

	}

	private void AnalyzeParenthesizedLambda(SyntaxNodeAnalysisContext context) {

		var lambda = (ParenthesizedLambdaExpressionSyntax)context.Node;

		foreach (var parameter in lambda.ParameterList.Parameters) {

			AnalyzeParameter(context, parameter);

		}

	}

	private static void AnalyzeParameter(SyntaxNodeAnalysisContext context, ParameterSyntax parameter) {

		var name = parameter.Identifier.ValueText;

		if (name.Length > 1) return;

		if (name is "_" or "x") return;

		var semanticModel = context.SemanticModel;
		var paramSymbol = semanticModel.GetDeclaredSymbol(parameter);

		if (paramSymbol?.Type is null) return;

		var expectedInitial = GetSingleLetterSuggestion(paramSymbol.Type);

		if (string.IsNullOrEmpty(expectedInitial)) return;

		if (string.Equals(name, expectedInitial, StringComparison.Ordinal)) return;

		var properties = ImmutableDictionary<string, string?>.Empty.Add(PropertyName, expectedInitial);
		var diagnostic = Diagnostic.Create(Rule, parameter.Identifier.GetLocation(), properties, name, expectedInitial);

		context.ReportDiagnostic(diagnostic);

	}

	private static string GetSingleLetterSuggestion(ITypeSymbol typeSymbol) {

		var typeName = GetCleanTypeName(typeSymbol);
		if (string.IsNullOrEmpty(typeName)) return "x";

		return char.ToLowerInvariant(typeName[0]).ToString();

	}

	private static string GetCleanTypeName(ITypeSymbol typeSymbol) {

		if (typeSymbol.TypeKind == TypeKind.Array) return "Array";

		var typeName = typeSymbol.Name;

		if (typeName.Length > 1 && typeName.StartsWith("I") && char.IsUpper(typeName[1])) typeName = typeName.Substring(1);

		return typeName;

	}

}
