namespace Olympus.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class OneLevelNamespaceAnalyzer : DiagnosticAnalyzer {

	public const string Identifier = "OL0001";

	public const string Category = "Architecture";

	public const string Property = "ExpectedNamespace";

	private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Strings.OneLevelNamespaceTitle), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString Message = new LocalizableResourceString(nameof(Strings.OneLevelNamespaceMessage), Strings.ResourceManager, typeof(Strings));

	private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Strings.OneLevelNamespaceDescription), Strings.ResourceManager, typeof(Strings));

	private static readonly DiagnosticDescriptor Rule = new(Identifier, Title, Message, Category, DiagnosticSeverity.Warning, true, Description);

	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

	public override void Initialize(AnalysisContext context) {

		context.EnableConcurrentExecution();
		context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
		context.RegisterSyntaxNodeAction(AnalyzeNamespace, SyntaxKind.NamespaceDeclaration, SyntaxKind.FileScopedNamespaceDeclaration);

	}

	private void AnalyzeNamespace(SyntaxNodeAnalysisContext context) {

		var namespaceDeclaration = (BaseNamespaceDeclarationSyntax)context.Node;
		var currentNamespace = namespaceDeclaration.Name.ToString();
		var assemblyName = context.Compilation.AssemblyName;
		if (string.IsNullOrEmpty(assemblyName)) return;

		var filePath = context.Node.SyntaxTree.FilePath;
		if (string.IsNullOrEmpty(filePath)) return;

		var normalizedPath = filePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
		var pathSegments = normalizedPath.Split(Path.DirectorySeparatorChar);
		var rootIndex = Array.LastIndexOf(pathSegments, assemblyName);
		if (rootIndex == -1 || rootIndex + 1 >= pathSegments.Length) return;

		var targetFolder = pathSegments[rootIndex + 1];
		if (targetFolder.EndsWith(".cs")) return;

		var expectedNamespace = targetFolder.StartsWith("$") ? assemblyName : $"{assemblyName}.{targetFolder}";

		if (currentNamespace.Equals(expectedNamespace, StringComparison.Ordinal)) return;

		var properties = ImmutableDictionary<string, string?>.Empty.Add(Property, expectedNamespace);
		var diagnostic = Diagnostic.Create(Rule, namespaceDeclaration.Name.GetLocation(), properties, currentNamespace, expectedNamespace);

		context.ReportDiagnostic(diagnostic);

	}

}
