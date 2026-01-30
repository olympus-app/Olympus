namespace Olympus.Analyzers;

[Shared]
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(FileNamingCodeFixProvider))]
public class FileNamingCodeFixProvider : CodeFixProvider {

	private static readonly LocalizableString TitleFormat = new LocalizableResourceString(nameof(Strings.FileNamingCodeFixTitle), Strings.ResourceManager, typeof(Strings));

	public override ImmutableArray<string> FixableDiagnosticIds => [FileNamingAnalyzer.Identifier];

	public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

	public override async Task RegisterCodeFixesAsync(CodeFixContext context) {

		var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
		if (root is null) return;

		var diagnostic = context.Diagnostics[0];
		var diagnosticSpan = diagnostic.Location.SourceSpan;

		var token = root.FindToken(diagnosticSpan.Start);
		var typeName = token.ValueText;

		if (!TryGetNewFileName(context.Document.Name, typeName, out var newFileName)) return;

		var title = string.Format(TitleFormat.ToString(), newFileName);

		context.RegisterCodeFix(
			CodeAction.Create(
				title: title,
				createChangedSolution: cToken => RenameFileAsync(context.Document, newFileName!),
				equivalenceKey: nameof(FileNamingCodeFixProvider)
			),
			diagnostic
		);

	}

	private static Task<Solution> RenameFileAsync(Document document, string newFileName) {

		var newSolution = document.Project.Solution.WithDocumentName(document.Id, newFileName);

		return Task.FromResult(newSolution);

	}

	private static bool TryGetNewFileName(string currentFileName, string typeName, out string? newFileName) {

		newFileName = null;

		var cleanName = Path.GetFileNameWithoutExtension(currentFileName);

		if (cleanName.EndsWith(".razor", StringComparison.OrdinalIgnoreCase)) cleanName = Path.GetFileNameWithoutExtension(cleanName);

		if (!currentFileName.StartsWith(cleanName, StringComparison.Ordinal)) return false;

		var extensionPart = currentFileName.Substring(cleanName.Length);
		newFileName = typeName + extensionPart;

		return true;

	}

}
