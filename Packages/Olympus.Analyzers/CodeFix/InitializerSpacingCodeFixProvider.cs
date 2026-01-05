using Olympus.Analyzers.Localization;

namespace Olympus.Analyzers;

[Shared]
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(InitializerSpacingCodeFixProvider))]
public class InitializerSpacingCodeFixProvider : CodeFixProvider {

	private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Strings.InitializerSpacingCodeFixTitle), Strings.ResourceManager, typeof(Strings));

	public override ImmutableArray<string> FixableDiagnosticIds => [InitializerSpacingAnalyzer.Identifier];

	public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

	public override async Task RegisterCodeFixesAsync(CodeFixContext context) {

		var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
		if (root is null) return;

		var diagnostic = context.Diagnostics[0];
		var diagnosticSpan = diagnostic.Location.SourceSpan;

		var token = root.FindToken(diagnosticSpan.Start);

		context.RegisterCodeFix(
			CodeAction.Create(
				title: Title.ToString(),
				createChangedDocument: cToken => FixSpacingAsync(context.Document, root, token),
				equivalenceKey: nameof(InitializerSpacingCodeFixProvider)
			),
			diagnostic
		);

	}

	private static Task<Document> FixSpacingAsync(Document document, SyntaxNode root, SyntaxToken token) {

		var isOpeningToken = token.IsKind(SyntaxKind.OpenBraceToken) || token.IsKind(SyntaxKind.OpenParenToken) || token.IsKind(SyntaxKind.OpenBracketToken);

		var tokenToFix = isOpeningToken ? token.GetNextToken() : token;

		if (tokenToFix.IsKind(SyntaxKind.None)) return Task.FromResult(document);

		var currentTrivia = tokenToFix.LeadingTrivia;
		var contentStartIndex = -1;

		for (var i = 0; i < currentTrivia.Count; i++) {

			if (!currentTrivia[i].IsKind(SyntaxKind.EndOfLineTrivia) && !currentTrivia[i].IsKind(SyntaxKind.WhitespaceTrivia)) {

				contentStartIndex = i;
				break;

			}

		}

		var boundaryIndex = contentStartIndex == -1 ? currentTrivia.Count : contentStartIndex;
		var lastEolIndexInGap = -1;

		for (var i = boundaryIndex - 1; i >= 0; i--) {

			if (currentTrivia[i].IsKind(SyntaxKind.EndOfLineTrivia)) {

				lastEolIndexInGap = i;
				break;

			}

		}

		var newTriviaList = new List<SyntaxTrivia>();
		var prevToken = tokenToFix.GetPreviousToken();
		var prevEndsInLine = prevToken.TrailingTrivia.Any(trivia => trivia.IsKind(SyntaxKind.EndOfLineTrivia));

		if (!prevEndsInLine) newTriviaList.Add(SyntaxFactory.EndOfLine("\r\n"));

		if (lastEolIndexInGap != -1) {

			for (var i = lastEolIndexInGap + 1; i < currentTrivia.Count; i++) {

				newTriviaList.Add(currentTrivia[i]);

			}

		} else {

			newTriviaList.AddRange(currentTrivia);

		}

		var newToken = tokenToFix.WithLeadingTrivia(newTriviaList);
		var newRoot = root.ReplaceToken(tokenToFix, newToken);

		return Task.FromResult(document.WithSyntaxRoot(newRoot));

	}

}
