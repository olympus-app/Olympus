namespace Olympus.Analyzers;

[Shared]
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(StaticLambdaCodeFixProvider))]
public class StaticLambdaCodeFixProvider : CodeFixProvider {

	private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Strings.StaticLambdaCodeFixTitle), Strings.ResourceManager, typeof(Strings));

	public override ImmutableArray<string> FixableDiagnosticIds => [StaticLambdaAnalyzer.Identifier];

	public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

	public override async Task RegisterCodeFixesAsync(CodeFixContext context) {

		var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
		if (root is null) return;

		var diagnostic = context.Diagnostics[0];
		var diagnosticSpan = diagnostic.Location.SourceSpan;

		var token = root.FindToken(diagnosticSpan.Start);
		var lambda = token.Parent?.FirstAncestorOrSelf<AnonymousFunctionExpressionSyntax>();

		if (lambda is null) return;

		context.RegisterCodeFix(
			CodeAction.Create(
				title: Title.ToString(),
				createChangedDocument: cToken => MakeStaticAsync(context.Document, root, lambda),
				equivalenceKey: nameof(StaticLambdaCodeFixProvider)
			),
			diagnostic
		);

	}

	private static Task<Document> MakeStaticAsync(Document document, SyntaxNode root, AnonymousFunctionExpressionSyntax lambda) {

		var staticToken = SyntaxFactory.Token(SyntaxKind.StaticKeyword).WithTrailingTrivia(SyntaxFactory.Space);

		var newModifiers = lambda.Modifiers.Insert(0, staticToken);
		var newLambda = lambda.WithModifiers(newModifiers);
		var newRoot = root.ReplaceNode(lambda, newLambda);

		return Task.FromResult(document.WithSyntaxRoot(newRoot));

	}

}
