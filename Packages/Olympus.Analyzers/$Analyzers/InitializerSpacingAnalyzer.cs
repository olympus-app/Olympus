namespace Olympus.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class InitializerSpacingAnalyzer : DiagnosticAnalyzer {

	public const string Identifier = "OL0003";

	public const string Category = "Formatting";

	private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Strings.InitializerSpacingTitle), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString MessageRemoveAfterOpen = new LocalizableResourceString(nameof(Strings.InitializerSpacingMessageRemoveAfterOpen), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString MessageRemoveBeforeClose = new LocalizableResourceString(nameof(Strings.InitializerSpacingMessageRemoveBeforeClose), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString MessageRemoveBetweenItems = new LocalizableResourceString(nameof(Strings.InitializerSpacingMessageRemoveBetweenItems), Strings.ResourceManager, typeof(Strings)); // NOVO

	private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Strings.InitializerSpacingDescription), Strings.ResourceManager, typeof(Strings));

	private static readonly DiagnosticDescriptor RuleAfterOpen = new(Identifier, Title, MessageRemoveAfterOpen, Category, DiagnosticSeverity.Info, true, Description);

	private static readonly DiagnosticDescriptor RuleBeforeClose = new(Identifier, Title, MessageRemoveBeforeClose, Category, DiagnosticSeverity.Info, true, Description);

	private static readonly DiagnosticDescriptor RuleBetweenItems = new(Identifier, Title, MessageRemoveBetweenItems, Category, DiagnosticSeverity.Info, true, Description); // NOVO

	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [RuleAfterOpen, RuleBeforeClose, RuleBetweenItems];

	public override void Initialize(AnalysisContext context) {

		context.EnableConcurrentExecution();
		context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
		context.RegisterSyntaxNodeAction(AnalyzeInitializer, SyntaxKind.ObjectInitializerExpression, SyntaxKind.CollectionInitializerExpression, SyntaxKind.ArrayInitializerExpression, SyntaxKind.ComplexElementInitializerExpression, SyntaxKind.WithInitializerExpression);
		context.RegisterSyntaxNodeAction(AnalyzeAnonymousObject, SyntaxKind.AnonymousObjectCreationExpression);
		context.RegisterSyntaxNodeAction(AnalyzeSwitchExpression, SyntaxKind.SwitchExpression);
		context.RegisterSyntaxNodeAction(AnalyzePropertyPattern, SyntaxKind.PropertyPatternClause);
		context.RegisterSyntaxNodeAction(AnalyzeAccessorList, SyntaxKind.AccessorList);
		context.RegisterSyntaxNodeAction(AnalyzeArgumentList, SyntaxKind.ArgumentList);
		context.RegisterSyntaxNodeAction(AnalyzeParameterList, SyntaxKind.ParameterList);
		context.RegisterSyntaxNodeAction(AnalyzeCollectionExpression, SyntaxKind.CollectionExpression);
		context.RegisterSyntaxNodeAction(AnalyzeBracketedArgumentList, SyntaxKind.BracketedArgumentList);
		context.RegisterSyntaxNodeAction(AnalyzeBracketedParameterList, SyntaxKind.BracketedParameterList);
		context.RegisterSyntaxNodeAction(AnalyzeListPattern, SyntaxKind.ListPattern);
		context.RegisterSyntaxNodeAction(AnalyzeAttributeList, SyntaxKind.AttributeList);

	}

	private void AnalyzeInitializer(SyntaxNodeAnalysisContext context) {

		var node = (InitializerExpressionSyntax)context.Node;
		AnalyzeBraces(context, node.OpenBraceToken, node.CloseBraceToken, node.Expressions);

	}

	private void AnalyzeAnonymousObject(SyntaxNodeAnalysisContext context) {

		var node = (AnonymousObjectCreationExpressionSyntax)context.Node;
		AnalyzeBraces(context, node.OpenBraceToken, node.CloseBraceToken, node.Initializers);

	}

	private void AnalyzeSwitchExpression(SyntaxNodeAnalysisContext context) {

		var node = (SwitchExpressionSyntax)context.Node;
		AnalyzeBraces(context, node.OpenBraceToken, node.CloseBraceToken, node.Arms);

	}

	private void AnalyzePropertyPattern(SyntaxNodeAnalysisContext context) {

		var node = (PropertyPatternClauseSyntax)context.Node;
		AnalyzeBraces(context, node.OpenBraceToken, node.CloseBraceToken, node.Subpatterns);

	}

	private void AnalyzeAccessorList(SyntaxNodeAnalysisContext context) {

		var node = (AccessorListSyntax)context.Node;
		AnalyzeBraces(context, node.OpenBraceToken, node.CloseBraceToken, node.Accessors);

	}

	private void AnalyzeArgumentList(SyntaxNodeAnalysisContext context) {

		var node = (ArgumentListSyntax)context.Node;
		AnalyzeBraces(context, node.OpenParenToken, node.CloseParenToken, node.Arguments);

	}

	private void AnalyzeParameterList(SyntaxNodeAnalysisContext context) {

		var node = (ParameterListSyntax)context.Node;
		AnalyzeBraces(context, node.OpenParenToken, node.CloseParenToken, node.Parameters);

	}

	private void AnalyzeCollectionExpression(SyntaxNodeAnalysisContext context) {

		var node = (CollectionExpressionSyntax)context.Node;
		AnalyzeBraces(context, node.OpenBracketToken, node.CloseBracketToken, node.Elements);

	}

	private void AnalyzeBracketedArgumentList(SyntaxNodeAnalysisContext context) {

		var node = (BracketedArgumentListSyntax)context.Node;
		AnalyzeBraces(context, node.OpenBracketToken, node.CloseBracketToken, node.Arguments);

	}

	private void AnalyzeBracketedParameterList(SyntaxNodeAnalysisContext context) {

		var node = (BracketedParameterListSyntax)context.Node;
		AnalyzeBraces(context, node.OpenBracketToken, node.CloseBracketToken, node.Parameters);

	}

	private void AnalyzeListPattern(SyntaxNodeAnalysisContext context) {

		var node = (ListPatternSyntax)context.Node;
		AnalyzeBraces(context, node.OpenBracketToken, node.CloseBracketToken, node.Patterns);

	}

	private void AnalyzeAttributeList(SyntaxNodeAnalysisContext context) {

		var node = (AttributeListSyntax)context.Node;
		AnalyzeBraces(context, node.OpenBracketToken, node.CloseBracketToken, node.Attributes);

	}

	private static void AnalyzeBraces<TNode>(SyntaxNodeAnalysisContext context, SyntaxToken openToken, SyntaxToken closeToken, SeparatedSyntaxList<TNode> items) where TNode : SyntaxNode {

		AnalyzeBraces(context, openToken, closeToken, items.Any() ? items.First().GetFirstToken() : default, items.Any() ? items.Last().GetLastToken() : default);
		AnalyzeBetweenItems(context, items);

	}

	private static void AnalyzeBraces<TNode>(SyntaxNodeAnalysisContext context, SyntaxToken openToken, SyntaxToken closeToken, SyntaxList<TNode> items) where TNode : SyntaxNode {

		AnalyzeBraces(context, openToken, closeToken, items.Any() ? items.First().GetFirstToken() : default, items.Any() ? items.Last().GetLastToken() : default);
		AnalyzeBetweenItems(context, items);

	}

	private static void AnalyzeBetweenItems<TNode>(SyntaxNodeAnalysisContext context, IEnumerable<TNode> items) where TNode : SyntaxNode {

		SyntaxNode? previousItem = null;

		foreach (var item in items) {

			if (previousItem is not null) {

				var prevEndLine = GetVisualEndLine(previousItem.GetLastToken());
				var currStartLine = GetVisualStartLine(item.GetFirstToken());

				if (currStartLine - prevEndLine > 1) {

					var diagnostic = Diagnostic.Create(RuleBetweenItems, item.GetFirstToken().GetLocation());
					context.ReportDiagnostic(diagnostic);

				}

			}

			previousItem = item;

		}

	}

	private static void AnalyzeBraces(SyntaxNodeAnalysisContext context, SyntaxToken openToken, SyntaxToken closeToken, SyntaxToken firstContentToken, SyntaxToken lastContentToken) {

		if (openToken.IsMissing || closeToken.IsMissing) return;

		var openLine = openToken.GetLocation().GetLineSpan().EndLinePosition.Line;
		var closeLine = closeToken.GetLocation().GetLineSpan().StartLinePosition.Line;

		if (openLine == closeLine) return;

		var firstToken = firstContentToken != default ? firstContentToken : closeToken;
		var firstLine = GetVisualStartLine(firstToken);

		if (firstLine - openLine > 1) {

			var diagnostic = Diagnostic.Create(RuleAfterOpen, openToken.GetLocation());
			context.ReportDiagnostic(diagnostic);

		}

		int visualContentEndLine;

		var firstCommentOnClose = closeToken.LeadingTrivia.FirstOrDefault(trivia => trivia.IsKind(SyntaxKind.SingleLineCommentTrivia) || trivia.IsKind(SyntaxKind.MultiLineCommentTrivia));

		visualContentEndLine = firstCommentOnClose != default ? firstCommentOnClose.GetLocation().GetLineSpan().EndLinePosition.Line : lastContentToken != default ? GetVisualEndLine(lastContentToken) : openLine;

		if (closeLine - visualContentEndLine > 1) {

			var diagnostic = Diagnostic.Create(RuleBeforeClose, closeToken.GetLocation());
			context.ReportDiagnostic(diagnostic);

		}

	}

	private static int GetVisualStartLine(SyntaxToken token) {

		var firstTrivia = token.LeadingTrivia.FirstOrDefault(trivia => !trivia.IsKind(SyntaxKind.WhitespaceTrivia) && !trivia.IsKind(SyntaxKind.EndOfLineTrivia));

		if (firstTrivia != default) return firstTrivia.GetLocation().GetLineSpan().StartLinePosition.Line;

		return token.GetLocation().GetLineSpan().StartLinePosition.Line;

	}

	private static int GetVisualEndLine(SyntaxToken token) {

		var lastTrivia = token.TrailingTrivia.LastOrDefault(trivia => !trivia.IsKind(SyntaxKind.WhitespaceTrivia) && !trivia.IsKind(SyntaxKind.EndOfLineTrivia));

		if (lastTrivia != default) return lastTrivia.GetLocation().GetLineSpan().EndLinePosition.Line;

		return token.GetLocation().GetLineSpan().EndLinePosition.Line;

	}

}
