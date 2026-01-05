using Olympus.Analyzers.Localization;

namespace Olympus.Analyzers;

[Shared]
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(OneLevelNamespaceCodeFixProvider))]
public class OneLevelNamespaceCodeFixProvider : CodeFixProvider {

	private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Strings.OneLevelNamespaceCodeFixTitle), Strings.ResourceManager, typeof(Strings));

	public override ImmutableArray<string> FixableDiagnosticIds => [OneLevelNamespaceAnalyzer.Identifier];

	public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

	public override async Task RegisterCodeFixesAsync(CodeFixContext context) {

		var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
		if (root is null) return;

		var diagnostic = context.Diagnostics[0];

		if (!diagnostic.Properties.TryGetValue(OneLevelNamespaceAnalyzer.Property, out var expectedNamespace)) return;

		var diagnosticSpan = diagnostic.Location.SourceSpan;
		var namespaceNameNode = root.FindNode(diagnosticSpan);

		context.RegisterCodeFix(
			CodeAction.Create(
				title: Title.ToString(),
				createChangedDocument: _ => FixNamespaceAsync(context.Document, root, namespaceNameNode, expectedNamespace!),
				equivalenceKey: nameof(OneLevelNamespaceCodeFixProvider)
			),
			diagnostic
		);

	}

	private static Task<Document> FixNamespaceAsync(Document document, SyntaxNode root, SyntaxNode oldNameNode, string newNamespaceName) {

		var newNameNode = SyntaxFactory.ParseName(newNamespaceName).WithTriviaFrom(oldNameNode);
		var newRoot = root.ReplaceNode(oldNameNode, newNameNode);

		return Task.FromResult(document.WithSyntaxRoot(newRoot));

	}

}
