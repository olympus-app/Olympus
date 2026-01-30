namespace Olympus.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class BlockSpacingAnalyzer : DiagnosticAnalyzer {

	public const string Identifier = "OA0002";

	public const string Category = "Formatting";

	private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Strings.BlockSpacingTitle), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString MessageAfterOpen = new LocalizableResourceString(nameof(Strings.BlockSpacingMessageAfterOpen), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString MessageBeforeClose = new LocalizableResourceString(nameof(Strings.BlockSpacingMessageBeforeClose), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Strings.BlockSpacingDescription), Strings.ResourceManager, typeof(Strings));

	private static readonly DiagnosticDescriptor RuleAfterOpen = new(Identifier, Title, MessageAfterOpen, Category, DiagnosticSeverity.Info, true, Description);

	private static readonly DiagnosticDescriptor RuleBeforeClose = new(Identifier, Title, MessageBeforeClose, Category, DiagnosticSeverity.Info, true, Description);

	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [RuleAfterOpen, RuleBeforeClose];

	public override void Initialize(AnalysisContext context) {

		context.EnableConcurrentExecution();
		context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
		context.RegisterSyntaxNodeAction(AnalyzeNamespace, SyntaxKind.NamespaceDeclaration);
		context.RegisterSyntaxNodeAction(AnalyzeTypeDeclaration, SyntaxKind.StructDeclaration, SyntaxKind.InterfaceDeclaration, SyntaxKind.ClassDeclaration, SyntaxKind.RecordDeclaration, SyntaxKind.RecordStructDeclaration);
		context.RegisterSyntaxNodeAction(AnalyzeExtensionBlock, SyntaxKind.ExtensionBlockDeclaration);
		context.RegisterSyntaxNodeAction(AnalyzeEnum, SyntaxKind.EnumDeclaration);
		context.RegisterSyntaxNodeAction(AnalyzeSwitch, SyntaxKind.SwitchStatement);
		context.RegisterSyntaxNodeAction(AnalyzeBlock, SyntaxKind.Block);

	}

	private void AnalyzeNamespace(SyntaxNodeAnalysisContext context) {

		var namespaceDecl = (NamespaceDeclarationSyntax)context.Node;
		AnalyzeBraces(context, namespaceDecl.OpenBraceToken, namespaceDecl.CloseBraceToken, namespaceDecl.Members);

	}

	private void AnalyzeTypeDeclaration(SyntaxNodeAnalysisContext context) {

		var typeDecl = (TypeDeclarationSyntax)context.Node;
		AnalyzeBraces(context, typeDecl.OpenBraceToken, typeDecl.CloseBraceToken, typeDecl.Members);

	}

	private void AnalyzeExtensionBlock(SyntaxNodeAnalysisContext context) {

		var extensionDecl = (ExtensionBlockDeclarationSyntax)context.Node;
		AnalyzeBraces(context, extensionDecl.OpenBraceToken, extensionDecl.CloseBraceToken, extensionDecl.Members);

	}

	private void AnalyzeEnum(SyntaxNodeAnalysisContext context) {

		var enumDecl = (EnumDeclarationSyntax)context.Node;
		AnalyzeBraces(context, enumDecl.OpenBraceToken, enumDecl.CloseBraceToken, enumDecl.Members);

	}

	private void AnalyzeSwitch(SyntaxNodeAnalysisContext context) {

		var switchStatement = (SwitchStatementSyntax)context.Node;
		AnalyzeBraces(context, switchStatement.OpenBraceToken, switchStatement.CloseBraceToken, switchStatement.Sections);

	}

	private void AnalyzeBlock(SyntaxNodeAnalysisContext context) {

		var block = (BlockSyntax)context.Node;
		if (block.Parent is LambdaExpressionSyntax) return;
		AnalyzeBraces(context, block.OpenBraceToken, block.CloseBraceToken, block.Statements);

	}

	private static void AnalyzeBraces<TNode>(SyntaxNodeAnalysisContext context, SyntaxToken openBrace, SyntaxToken closeBrace, SyntaxList<TNode> items) where TNode : SyntaxNode {

		AnalyzeBraces(context, openBrace, closeBrace, items.Any() ? items.First().GetFirstToken() : default, items.Any() ? items.Last().GetLastToken() : default);

	}

	private static void AnalyzeBraces<TNode>(SyntaxNodeAnalysisContext context, SyntaxToken openBrace, SyntaxToken closeBrace, SeparatedSyntaxList<TNode> items) where TNode : SyntaxNode {

		AnalyzeBraces(context, openBrace, closeBrace, items.Any() ? items.First().GetFirstToken() : default, items.Any() ? items.Last().GetLastToken() : default);

	}

	private static void AnalyzeBraces(SyntaxNodeAnalysisContext context, SyntaxToken openBrace, SyntaxToken closeBrace, SyntaxToken firstToken, SyntaxToken lastToken) {

		if (openBrace.IsMissing || closeBrace.IsMissing) return;

		var openBraceLine = openBrace.GetLocation().GetLineSpan().EndLinePosition.Line;
		var closeBraceLine = closeBrace.GetLocation().GetLineSpan().StartLinePosition.Line;

		if (openBraceLine == closeBraceLine) return;

		var firstContentToken = firstToken != default ? firstToken : closeBrace;
		var firstContentLine = GetVisualStartLine(firstContentToken);

		if (firstContentLine - openBraceLine < 2) {
			var diagnostic = Diagnostic.Create(RuleAfterOpen, openBrace.GetLocation());
			context.ReportDiagnostic(diagnostic);
		}

		int lastContentLineEnd;

		var lastCommentTrivia = closeBrace.LeadingTrivia.LastOrDefault(trivia => trivia.IsKind(SyntaxKind.SingleLineCommentTrivia) || trivia.IsKind(SyntaxKind.MultiLineCommentTrivia));

		lastContentLineEnd = lastCommentTrivia != default ? lastCommentTrivia.GetLocation().GetLineSpan().EndLinePosition.Line : lastToken != default ? lastToken.GetLocation().GetLineSpan().EndLinePosition.Line : openBraceLine;

		if (closeBraceLine - lastContentLineEnd < 2) {

			var diagnostic = Diagnostic.Create(RuleBeforeClose, closeBrace.GetLocation());
			context.ReportDiagnostic(diagnostic);

		}

	}

	private static int GetVisualStartLine(SyntaxToken token) {

		var firstTrivia = token.LeadingTrivia.FirstOrDefault(trivia => !trivia.IsKind(SyntaxKind.WhitespaceTrivia) && !trivia.IsKind(SyntaxKind.EndOfLineTrivia));

		if (firstTrivia != default) return firstTrivia.GetLocation().GetLineSpan().StartLinePosition.Line;

		return token.GetLocation().GetLineSpan().StartLinePosition.Line;

	}

}
