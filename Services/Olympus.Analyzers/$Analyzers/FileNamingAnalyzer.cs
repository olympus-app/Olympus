namespace Olympus.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class FileNamingAnalyzer : DiagnosticAnalyzer {

	public const string Identifier = "OL0008";

	public const string Category = "Naming";

	private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Strings.FileNamingTitle), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString Message = new LocalizableResourceString(nameof(Strings.FileNamingMessage), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Strings.FileNamingDescription), Strings.ResourceManager, typeof(Strings));

	private static readonly DiagnosticDescriptor Rule = new(Identifier, Title, Message, Category, DiagnosticSeverity.Info, true, Description);

	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

	public override void Initialize(AnalysisContext context) {

		context.EnableConcurrentExecution();
		context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
		context.RegisterCompilationStartAction(compilationContext => {
			var assemblyName = compilationContext.Compilation.AssemblyName;
			compilationContext.RegisterSyntaxTreeAction(ctx => AnalyzeSyntaxTree(ctx, assemblyName));
		});

	}

	private static void AnalyzeSyntaxTree(SyntaxTreeAnalysisContext context, string? assemblyName) {

		var root = context.Tree.GetRoot(context.CancellationToken);

		var typeDeclarations = GetTopLevelTypeDeclarations(root).ToList();

		if (typeDeclarations.Count == 0) return;

		var filePath = context.Tree.FilePath;
		if (string.IsNullOrEmpty(filePath)) return;

		var fileName = GetCleanFileName(filePath);

		if (!string.IsNullOrEmpty(assemblyName) && string.Equals(fileName, assemblyName, StringComparison.OrdinalIgnoreCase)) return;

		var firstType = typeDeclarations[0];
		var firstTypeName = firstType.Identifier.Text;

		var isValid = typeDeclarations.Count == 1
			? string.Equals(fileName, firstTypeName, StringComparison.Ordinal)
			: string.Equals(fileName, firstTypeName, StringComparison.Ordinal) || string.Equals(fileName, firstTypeName + "s", StringComparison.Ordinal);

		if (!isValid) {

			var expectedName = typeDeclarations.Count > 1 ? $"{firstTypeName} (or {firstTypeName}s)" : firstTypeName;

			var diagnostic = Diagnostic.Create(Rule, firstType.Identifier.GetLocation(), fileName, expectedName);

			context.ReportDiagnostic(diagnostic);

		}

	}

	private static IEnumerable<BaseTypeDeclarationSyntax> GetTopLevelTypeDeclarations(SyntaxNode root) {

		if (root is CompilationUnitSyntax compilationUnit) {

			foreach (var member in compilationUnit.Members) {

				if (member is BaseTypeDeclarationSyntax typeDecl) {

					yield return typeDecl;

				} else if (member is NamespaceDeclarationSyntax namespaceDecl) {

					foreach (var nestedMember in namespaceDecl.Members) {

						if (nestedMember is BaseTypeDeclarationSyntax nestedType) yield return nestedType;

					}

				} else if (member is FileScopedNamespaceDeclarationSyntax fileNamespaceDecl) {

					foreach (var nestedMember in fileNamespaceDecl.Members) {

						if (nestedMember is BaseTypeDeclarationSyntax nestedType) yield return nestedType;

					}

				}

			}

		}

	}

	private static string GetCleanFileName(string filePath) {

		var fileName = Path.GetFileNameWithoutExtension(filePath);

		if (fileName.EndsWith(".razor", StringComparison.OrdinalIgnoreCase)) return Path.GetFileNameWithoutExtension(fileName);

		return fileName;

	}

}
