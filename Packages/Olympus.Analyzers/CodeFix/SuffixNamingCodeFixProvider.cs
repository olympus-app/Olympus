using Olympus.Analyzers.Localization;

namespace Olympus.Analyzers;

[Shared]
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(SuffixNamingCodeFixProvider))]
public class SuffixNamingCodeFixProvider : CodeFixProvider {

	public override ImmutableArray<string> FixableDiagnosticIds => [SuffixNamingAnalyzer.Identifier];

	public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

	public override Task RegisterCodeFixesAsync(CodeFixContext context) {

		var diagnostic = context.Diagnostics[0];

		if (!diagnostic.Properties.TryGetValue(SuffixNamingAnalyzer.SuffixProperty, out var suggestedName)) return Task.CompletedTask;

		var title = string.Format(Strings.LambdaParameterNamingCodeFixTitle, suggestedName);

		context.RegisterCodeFix(
			CodeAction.Create(
				title: title,
				createChangedSolution: cToken => RenameParameterAsync(context.Document, diagnostic, suggestedName!, cToken),
				equivalenceKey: nameof(LambdaParameterNamingCodeFixProvider)
			),
			diagnostic
		);

		return Task.CompletedTask;

	}

	private static async Task<Solution> RenameParameterAsync(Document document, Diagnostic diagnostic, string newName, CancellationToken cancellationToken) {

		var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
		var token = root!.FindToken(diagnostic.Location.SourceSpan.Start);

		var parameterNode = token.Parent;

		var semanticModel = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);
		var symbol = semanticModel!.GetDeclaredSymbol(parameterNode!, cancellationToken);

		if (symbol is null) return document.Project.Solution;

		var solution = document.Project.Solution;

		var renameOptions = new SymbolRenameOptions(
			RenameOverloads: false,
			RenameInStrings: false,
			RenameInComments: false,
			RenameFile: false
		);

		return await Renamer.RenameSymbolAsync(solution, symbol, renameOptions, newName, cancellationToken).ConfigureAwait(false);

	}

}
