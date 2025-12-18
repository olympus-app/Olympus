namespace Olympus.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class ReturnStatementSpacingAnalyzer : DiagnosticAnalyzer {

	public const string Identifier = "OL0004";

	public const string Category = "Formatting";

	private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Strings.ReturnSpacingTitle), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString Message = new LocalizableResourceString(nameof(Strings.ReturnSpacingMessage), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Strings.ReturnSpacingDescription), Strings.ResourceManager, typeof(Strings));

	private static readonly DiagnosticDescriptor Rule = new(Identifier, Title, Message, Category, DiagnosticSeverity.Info, true, Description);

	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

	public override void Initialize(AnalysisContext context) {

		context.EnableConcurrentExecution();
		context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
		context.RegisterSyntaxNodeAction(AnalyzeReturn, SyntaxKind.ReturnStatement);

	}

	private void AnalyzeReturn(SyntaxNodeAnalysisContext context) {

		var returnStatement = (ReturnStatementSyntax)context.Node;

		if (returnStatement.Parent is BlockSyntax block && block.Parent is LambdaExpressionSyntax) return;

		var returnToken = returnStatement.ReturnKeyword;
		var prevToken = returnToken.GetPreviousToken();

		if (prevToken.IsKind(SyntaxKind.None) || prevToken.IsKind(SyntaxKind.OpenBraceToken)) return;

		var prevLine = prevToken.GetLocation().GetLineSpan().EndLinePosition.Line;
		var visualReturnStartLine = GetVisualStartLine(returnToken);

		if (prevLine == visualReturnStartLine) return;
		if (visualReturnStartLine - prevLine >= 2) return;

		var diagnostic = Diagnostic.Create(Rule, returnToken.GetLocation());
		context.ReportDiagnostic(diagnostic);

	}

	private static int GetVisualStartLine(SyntaxToken token) {

		var firstTrivia = token.LeadingTrivia.FirstOrDefault(trivia => !trivia.IsKind(SyntaxKind.WhitespaceTrivia) && !trivia.IsKind(SyntaxKind.EndOfLineTrivia));

		if (firstTrivia != default) return firstTrivia.GetLocation().GetLineSpan().StartLinePosition.Line;

		return token.GetLocation().GetLineSpan().StartLinePosition.Line;

	}

}
