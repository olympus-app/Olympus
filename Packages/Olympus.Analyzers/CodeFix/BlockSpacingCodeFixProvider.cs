namespace Olympus.Analyzers;

[Shared]
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(BlockSpacingCodeFixProvider))]
public class BlockSpacingCodeFixProvider : CodeFixProvider {

	private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Strings.BlockSpacingCodeFixTitle), Strings.ResourceManager, typeof(Strings));

	public override ImmutableArray<string> FixableDiagnosticIds => [BlockSpacingAnalyzer.Identifier];

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
				equivalenceKey: nameof(BlockSpacingCodeFixProvider)
			),
			diagnostic
		);

	}

	private static Task<Document> FixSpacingAsync(Document document, SyntaxNode root, SyntaxToken token) {

		SyntaxToken newToken;

		var endOfLine = SyntaxFactory.EndOfLine("\r\n");

		if (token.IsKind(SyntaxKind.OpenBraceToken)) {

			var newTrivia = token.TrailingTrivia.Insert(0, endOfLine);
			newToken = token.WithTrailingTrivia(newTrivia);

		} else {

			var currentTrivia = token.LeadingTrivia;
			var lastCommentIndex = -1;

			for (var i = currentTrivia.Count - 1; i >= 0; i--) {

				if (currentTrivia[i].IsKind(SyntaxKind.SingleLineCommentTrivia) || currentTrivia[i].IsKind(SyntaxKind.MultiLineCommentTrivia)) {

					lastCommentIndex = i;
					break;

				}

			}

			var newTrivia = lastCommentIndex != -1 ? currentTrivia.Insert(lastCommentIndex + 1, endOfLine) : currentTrivia.Insert(0, endOfLine);

			newToken = token.WithLeadingTrivia(newTrivia);

		}

		var newRoot = root.ReplaceToken(token, newToken);

		return Task.FromResult(document.WithSyntaxRoot(newRoot));

	}

}
