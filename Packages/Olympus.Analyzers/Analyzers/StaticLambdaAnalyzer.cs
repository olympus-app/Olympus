namespace Olympus.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class StaticLambdaAnalyzer : DiagnosticAnalyzer {

	public const string Identifier = "OA0006";

	public const string Category = "Performance";

	private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Strings.StaticLambdaTitle), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString Message = new LocalizableResourceString(nameof(Strings.StaticLambdaMessage), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Strings.StaticLambdaDescription), Strings.ResourceManager, typeof(Strings));

	private static readonly DiagnosticDescriptor Rule = new(Identifier, Title, Message, Category, DiagnosticSeverity.Info, true, Description);

	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

	public override void Initialize(AnalysisContext context) {

		context.EnableConcurrentExecution();
		context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
		context.RegisterSyntaxNodeAction(AnalyzeLambda, SyntaxKind.SimpleLambdaExpression, SyntaxKind.ParenthesizedLambdaExpression, SyntaxKind.AnonymousMethodExpression);

	}

	private void AnalyzeLambda(SyntaxNodeAnalysisContext context) {

		var lambda = (AnonymousFunctionExpressionSyntax)context.Node;

		if (lambda.Modifiers.Any(SyntaxKind.StaticKeyword)) return;

		if (IsExpressionTree(context.SemanticModel, lambda)) return;

		if (IsLinqMethod(context, lambda)) return;

		if (CapturesState(context.SemanticModel, lambda)) return;

		var location = lambda.GetLocation();
		var diagnostic = Diagnostic.Create(Rule, location);

		context.ReportDiagnostic(diagnostic);

	}

	private static bool CapturesState(SemanticModel semanticModel, AnonymousFunctionExpressionSyntax lambda) {

		foreach (var node in lambda.Body.DescendantNodes()) {

			if (node.IsKind(SyntaxKind.ThisExpression) || node.IsKind(SyntaxKind.BaseExpression)) return true;

			if (node is IdentifierNameSyntax identifier) {

				if (IsIgnorableIdentifier(identifier)) continue;

				var symbolInfo = semanticModel.GetSymbolInfo(identifier);
				var symbol = symbolInfo.Symbol;

				if (symbol is null) continue;

				if (symbol.IsStatic) continue;
				if (symbol is IFieldSymbol field && field.IsConst) continue;
				if (symbol is ILocalSymbol loc && loc.IsConst) continue;

				if (symbol is ILocalSymbol or IParameterSymbol) {

					if (!IsDeclaredInsideLambda(symbol, lambda)) return true;
					continue;

				}

				if (IsInstanceMember(symbol)) return true;

			}

		}

		return false;

	}

	private static bool IsIgnorableIdentifier(IdentifierNameSyntax identifier) {

		var parent = identifier.Parent;
		if (parent is null) return false;

		if (parent is VariableDeclaratorSyntax varDecl && varDecl.Identifier == identifier.Identifier) return true;

		if (parent is ParameterSyntax param && param.Identifier == identifier.Identifier) return true;

		if (parent is NameColonSyntax) return true;

		if (parent is MemberAccessExpressionSyntax memberAccess && memberAccess.Name == identifier) return true;

		if (parent is ObjectCreationExpressionSyntax) return true;

		return false;

	}

	private static bool IsInstanceMember(ISymbol symbol) {

		return symbol.Kind is SymbolKind.Field or SymbolKind.Property or SymbolKind.Event or SymbolKind.Method;

	}

	private static bool IsDeclaredInsideLambda(ISymbol symbol, AnonymousFunctionExpressionSyntax lambda) {

		var declaringReference = symbol.DeclaringSyntaxReferences.FirstOrDefault();
		if (declaringReference is null) return false;

		var declaringNode = declaringReference.GetSyntax();

		var root = lambda.Body ?? (SyntaxNode)lambda.ExpressionBody!;

		return root.Contains(declaringNode) || lambda.Contains(declaringNode);

	}

	private static bool IsExpressionTree(SemanticModel semanticModel, AnonymousFunctionExpressionSyntax lambda) {

		var typeInfo = semanticModel.GetTypeInfo(lambda);

		return typeInfo.ConvertedType?.ToDisplayString().StartsWith("System.Linq.Expressions.Expression") == true;

	}

	private static bool IsLinqMethod(SyntaxNodeAnalysisContext context, AnonymousFunctionExpressionSyntax lambda) {

		if (lambda.Parent is not ArgumentSyntax arg || arg.Parent is not ArgumentListSyntax argList || argList.Parent is not InvocationExpressionSyntax invocation) return false;

		var symbol = context.SemanticModel.GetSymbolInfo(invocation).Symbol as IMethodSymbol;

		return symbol?.ContainingType?.ContainingNamespace?.ToDisplayString() == "System.Linq";

	}

}
