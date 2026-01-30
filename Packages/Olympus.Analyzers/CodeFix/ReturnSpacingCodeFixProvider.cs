namespace Olympus.Analyzers;

[Shared]
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ReturnStatementSpacingCodeFixProvider))]
public class ReturnStatementSpacingCodeFixProvider : CodeFixProvider {

	private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Strings.ReturnSpacingCodeFixTitle), Strings.ResourceManager, typeof(Strings));

	public override ImmutableArray<string> FixableDiagnosticIds => [ReturnStatementSpacingAnalyzer.Identifier];

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
				equivalenceKey: nameof(ReturnStatementSpacingCodeFixProvider)
			),
			diagnostic
		);

	}

	private static Task<Document> FixSpacingAsync(Document document, SyntaxNode root, SyntaxToken token) {

		var endOfLine = SyntaxFactory.EndOfLine("\r\n");
		var newTrivia = token.LeadingTrivia.Insert(0, endOfLine);
		var newToken = token.WithLeadingTrivia(newTrivia);
		var newRoot = root.ReplaceToken(token, newToken);

		return Task.FromResult(document.WithSyntaxRoot(newRoot));

	}

}
