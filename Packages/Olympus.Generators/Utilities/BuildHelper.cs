namespace Olympus.Generators;

public static class BuildHelper {

	public const int MaxDocCommentLength = 256;

	private static readonly LocalizableString OG0001_Title = new LocalizableResourceString(nameof(Strings.EmptyResourceFileTitle), Strings.ResourceManager, typeof(Strings));
	private static readonly LocalizableString OG0001_Message = new LocalizableResourceString(nameof(Strings.EmptyResourceFileMessage), Strings.ResourceManager, typeof(Strings));
	private static readonly LocalizableString OG0001_Description = new LocalizableResourceString(nameof(Strings.EmptyResourceFileDescription), Strings.ResourceManager, typeof(Strings));
	public static readonly DiagnosticDescriptor EmptyResourceFile = new("OG0001", OG0001_Title, OG0001_Message, "Resources", DiagnosticSeverity.Warning, true, OG0001_Description);

	private static readonly LocalizableString OG0002_Title = new LocalizableResourceString(nameof(Strings.InvalidResourceKeyTitle), Strings.ResourceManager, typeof(Strings));
	private static readonly LocalizableString OG0002_Message = new LocalizableResourceString(nameof(Strings.InvalidResourceKeyMessage), Strings.ResourceManager, typeof(Strings));
	private static readonly LocalizableString OG0002_Description = new LocalizableResourceString(nameof(Strings.InvalidResourceKeyDescription), Strings.ResourceManager, typeof(Strings));
	public static readonly DiagnosticDescriptor InvalidResourceKey = new("OG0002", OG0002_Title, OG0002_Message, "Resources", DiagnosticSeverity.Warning, true, OG0002_Description);

	private static readonly LocalizableString OG0003_Title = new LocalizableResourceString(nameof(Strings.MissingResourceValueTitle), Strings.ResourceManager, typeof(Strings));
	private static readonly LocalizableString OG0003_Message = new LocalizableResourceString(nameof(Strings.MissingResourceValueMessage), Strings.ResourceManager, typeof(Strings));
	private static readonly LocalizableString OG0003_Description = new LocalizableResourceString(nameof(Strings.MissingResourceValueDescription), Strings.ResourceManager, typeof(Strings));
	public static readonly DiagnosticDescriptor MissingResourceValue = new("OG0003", OG0003_Title, OG0003_Message, "Resources", DiagnosticSeverity.Warning, true, OG0003_Description);

	private static readonly LocalizableString OG0004_Title = new LocalizableResourceString(nameof(Strings.DuplicateResourceKeyTitle), Strings.ResourceManager, typeof(Strings));
	private static readonly LocalizableString OG0004_Message = new LocalizableResourceString(nameof(Strings.DuplicateResourceKeyMessage), Strings.ResourceManager, typeof(Strings));
	private static readonly LocalizableString OG0004_Description = new LocalizableResourceString(nameof(Strings.DuplicateResourceKeyDescription), Strings.ResourceManager, typeof(Strings));
	public static readonly DiagnosticDescriptor DuplicateResourceKey = new("OG0004", OG0004_Title, OG0004_Message, "Resources", DiagnosticSeverity.Warning, true, OG0004_Description);

	private static readonly LocalizableString OG0005_Title = new LocalizableResourceString(nameof(Strings.MissingResourceTranslationTitle), Strings.ResourceManager, typeof(Strings));
	private static readonly LocalizableString OG0005_Message = new LocalizableResourceString(nameof(Strings.MissingResourceTranslationMessage), Strings.ResourceManager, typeof(Strings));
	private static readonly LocalizableString OG0005_Description = new LocalizableResourceString(nameof(Strings.MissingResourceTranslationDescription), Strings.ResourceManager, typeof(Strings));
	public static readonly DiagnosticDescriptor MissingResourceTranslation = new("OG0005", OG0005_Title, OG0005_Message, "Resources", DiagnosticSeverity.Warning, true, OG0005_Description);

	private static readonly LocalizableString OG0006_Title = new LocalizableResourceString(nameof(Strings.InvalidPermissionIdentifierTitle), Strings.ResourceManager, typeof(Strings));
	private static readonly LocalizableString OG0006_Message = new LocalizableResourceString(nameof(Strings.InvalidPermissionIdentifierMessage), Strings.ResourceManager, typeof(Strings));
	private static readonly LocalizableString OG0006_Description = new LocalizableResourceString(nameof(Strings.InvalidPermissionIdentifierDescription), Strings.ResourceManager, typeof(Strings));
	public static readonly DiagnosticDescriptor InvalidPermissionIdentifier = new("OG0006", OG0006_Title, OG0006_Message, "Permissions", DiagnosticSeverity.Warning, true, OG0006_Description);

	private static readonly LocalizableString OG0007_Title = new LocalizableResourceString(nameof(Strings.DuplicatePermissionIdentifierTitle), Strings.ResourceManager, typeof(Strings));
	private static readonly LocalizableString OG0007_Message = new LocalizableResourceString(nameof(Strings.DuplicatePermissionIdentifierMessage), Strings.ResourceManager, typeof(Strings));
	private static readonly LocalizableString OG0007_Description = new LocalizableResourceString(nameof(Strings.DuplicatePermissionIdentifierDescription), Strings.ResourceManager, typeof(Strings));
	public static readonly DiagnosticDescriptor DuplicatePermissionIdentifier = new("OG0007", OG0007_Title, OG0007_Message, "Permissions", DiagnosticSeverity.Warning, true, OG0007_Description);

	private static readonly LocalizableString OG0008_Title = new LocalizableResourceString(nameof(Strings.InvalidPermissionKeyTitle), Strings.ResourceManager, typeof(Strings));
	private static readonly LocalizableString OG0008_Message = new LocalizableResourceString(nameof(Strings.InvalidPermissionKeyMessage), Strings.ResourceManager, typeof(Strings));
	private static readonly LocalizableString OG0008_Description = new LocalizableResourceString(nameof(Strings.InvalidPermissionKeyDescription), Strings.ResourceManager, typeof(Strings));
	public static readonly DiagnosticDescriptor InvalidPermissionKey = new("OG0008", OG0008_Title, OG0008_Message, "Permissions", DiagnosticSeverity.Warning, true, OG0008_Description);

	private static readonly LocalizableString OG0009_Title = new LocalizableResourceString(nameof(Strings.InvalidRouteKeyTitle), Strings.ResourceManager, typeof(Strings));
	private static readonly LocalizableString OG0009_Message = new LocalizableResourceString(nameof(Strings.InvalidRouteKeyMessage), Strings.ResourceManager, typeof(Strings));
	private static readonly LocalizableString OG0009_Description = new LocalizableResourceString(nameof(Strings.InvalidRouteKeyDescription), Strings.ResourceManager, typeof(Strings));
	public static readonly DiagnosticDescriptor InvalidRouteKey = new("OG0009", OG0009_Title, OG0009_Message, "Routes", DiagnosticSeverity.Warning, true, OG0009_Description);

	public static string GetCultureCode(string? defaultCulture = null) {

		var code = $$"""
            /// <summary>Gets or sets the culture to be used for all resource lookups in this class.</summary>
            public global::System.Globalization.CultureInfo? Culture { get; set; } = {{(defaultCulture is null ? "null" : $"new global::System.Globalization.CultureInfo(\"{defaultCulture}\")")}};

        """;

		return code.TrimEnd();

	}

	public static string GetSingletonCode(string className) {

		var code = $$"""
			/// <summary>All values in <see cref="{{className}}"/> as the default instance of this class.</summary>
			public static {{className}} Values => field ??= new {{className}}();

		""";

		return code.TrimEnd();

	}

	public static string GetResourceManagerCode(string resourceName, string className, bool enableHostOverride) {

		var baseManager = $$"""
		    /// <summary>Returns the cached ResourceManager instance used by this class.</summary>
		    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		    public global::System.Resources.ResourceManager ResourceManager { get; } = new global::System.Resources.ResourceManager("{{resourceName}}", typeof({{className}}).Assembly);

		""";

		var hostManager = $$"""

		    /// <summary>Returns the cached ResourceManager instance used by this class for host overrides.</summary>
		    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		    public global::System.Resources.ResourceManager? HostResourceManager => hostResourceManagerLazy.Value;

			/// <summary>Lazy initialization of the Host ResourceManager used for overrides.</summary>
		    private static readonly global::System.Lazy<global::System.Resources.ResourceManager?> hostResourceManagerLazy = new global::System.Lazy<global::System.Resources.ResourceManager?>(() => {

		        var resourceName = "{{resourceName}}";

		        var hostAssembly = global::System.Reflection.Assembly.GetEntryAssembly() ?? global::System.Reflection.Assembly.GetExecutingAssembly();

		        if (!hostAssembly.GetManifestResourceNames().Contains(resourceName + ".resources")) return null;

				var manager = new global::System.Resources.ResourceManager(resourceName, hostAssembly);

				return manager.GetResourceSet(global::System.Globalization.CultureInfo.InvariantCulture, true, true) is not null ? manager  : null;

		    });

		""";

		return (baseManager + (enableHostOverride ? hostManager : string.Empty)).TrimEnd();

	}

	public static string GetGetStringCode(bool enableHostOverride) {

		var baseMethod = """
		    /// <summary>Gets a resource of the <see cref="ResourceManager"/> with the configured <see cref="Culture"/> as a string.</summary>
			/// <param name="key">The resource key to look up.</param>
			/// <returns>The localized string if found; otherwise, the resource key.</returns>
		    [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		    public string GetString(string key) => ResourceManager.GetString(key, Culture) ?? key;

		""";

		var hostMethod = """
			/// <summary>Gets a resource of the <see cref="ResourceManager"/> with the configured <see cref="Culture"/> as a string.</summary>
			/// <param name="key">The resource key to look up.</param>
			/// <returns>The localized string if found; otherwise, the resource key.</returns>
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public string GetString(string key) => HostResourceManager?.GetString(key, Culture)  ?? ResourceManager.GetString(key, Culture)  ?? key;

		""";

		return (enableHostOverride ? hostMethod : baseMethod).TrimEnd();

	}

	public static bool TryGetResourceDataAndValues(this AdditionalText additionalText, in List<Diagnostic> diagnostics, [NotNullWhen(true)] out Dictionary<string, XElement>? resourceNames, CancellationToken cancellationToken) {

		var text = additionalText.GetText(cancellationToken);

		if (text is null) {

			diagnostics.Add(Diagnostic.Create(EmptyResourceFile, Location.Create(additionalText.Path, default, default)));
			resourceNames = null;

			return false;

		}

		using var sourceTextReader = new SourceTextReader(text);
		resourceNames = [];

		foreach (var node in XDocument.Load(sourceTextReader, LoadOptions.SetLineInfo).Descendants("data")) {

			var nameAttribute = node.Attribute("name");
			var name = nameAttribute?.Value;

			if (nameAttribute is null || name is null || string.IsNullOrWhiteSpace(name)) {

				diagnostics.Add(Diagnostic.Create(InvalidResourceKey, GetXElementLocation(additionalText, node, name), name));

				continue;

			}

			var valueAttribute = node.Elements("value").FirstOrDefault();

			if (valueAttribute is null) {

				diagnostics.Add(Diagnostic.Create(MissingResourceValue, GetXElementLocation(additionalText, nameAttribute, name), name));

				continue;

			}

			if (resourceNames.ContainsKey(name)) {

				diagnostics.Add(Diagnostic.Create(DuplicateResourceKey, GetXElementLocation(additionalText, nameAttribute, name), name));

				continue;

			}

			resourceNames[name] = valueAttribute;

		}

		return true;

	}

	public static Location GetXElementLocation(AdditionalText text, IXmlLineInfo line, string? memberName) {

		return Location.Create(
			filePath: text.Path,
			textSpan: new TextSpan(),
			lineSpan: new LinePositionSpan(
				start: new LinePosition(line.LineNumber - 1, line.LinePosition - 1),
				end: new LinePosition(line.LineNumber - 1, line.LinePosition - 1 + memberName?.Length ?? 0)
			)
		);

	}

	public static string GetIdentifierFromResourceName(string name) {

		if (name.All(CharExtensions.IsIdentifierPartCharacter)) {

			return name[0].IsIdentifierStartCharacter() ? name : "_" + name;

		}

		var builder = new StringBuilder(name.Length);
		var initial = name[0];

		if (initial.IsIdentifierPartCharacter() && !initial.IsIdentifierStartCharacter()) {

			builder.Append('_');

		}

		foreach (var @char in name) {

			builder.Append(@char.IsIdentifierPartCharacter() ? @char : '_');

		}

		return builder.ToString();

	}

	public static string GetTrimmedDocComment(string value) {

		var trimmedValue = value.Length > MaxDocCommentLength ? value[..MaxDocCommentLength] + " ..." : value;
		var escaped = System.Security.SecurityElement.Escape(trimmedValue);

		return escaped.Replace("\n", "<br/>").Replace("\r", "");

	}

	public static bool SplitName(string fullName, [NotNullWhen(true)] out string? namespaceName, out string className) {

		var lastDot = fullName.LastIndexOf('.');

		if (lastDot == -1) {

			namespaceName = null;
			className = fullName;

			return false;

		}

		namespaceName = fullName[..lastDot];
		className = fullName[(lastDot + 1)..];

		return true;

	}

	public static bool IsChildFile(string fileToCheck, IEnumerable<string> availableFiles, [NotNullWhen(true)] out CultureInfo? cultureInfo) {

		SplitName(fileToCheck, out var parentFileName, out var languageExtension);

		if (!availableFiles.Contains(parentFileName)) {

			cultureInfo = null;

			return false;

		}

		var lastNumberOfCodes = 0;
		var sections = 0;

		foreach (var character in languageExtension) {

			switch (character) {

				case '-' when lastNumberOfCodes < 2 || sections > 1:
					cultureInfo = null;
					return false;

				case '-':
					lastNumberOfCodes = 0;
					sections++;
					continue;

				default:
					lastNumberOfCodes++;
					break;

			}

		}

		if (lastNumberOfCodes is > 4 or < 2) {

			cultureInfo = null;

			return false;

		}

		try {

			cultureInfo = CultureInfo.GetCultureInfo(languageExtension);

		} catch (CultureNotFoundException) {

			cultureInfo = null;

			return false;

		}

		return true;

	}

	public static bool TryGetString(this AnalyzerConfigOptions options, string key, [NotNullWhen(true)] out string? value) {

		return options.TryGetValue($"build_property.{key}", out value) && !string.IsNullOrWhiteSpace(value);

	}

	public static bool TryGetBool(this AnalyzerConfigOptions options, string key) {

		return options.TryGetValue($"build_property.{key}", out var value) && bool.TryParse(value, out var result) && result;

	}

	public static bool TryGetHostInfo(this AnalyzerConfigOptions options, [NotNullWhen(true)] out string? builderType, [NotNullWhen(true)] out string? hostPrefix) {

		if (!options.TryGetBool("IsApplicationHost")) {

			builderType = null;
			hostPrefix = null;

			return false;

		}

		var isAspire = options.TryGetBool("IsAspireApplicationHost");
		var isApi = options.TryGetBool("IsApiApplicationHost");
		var isWeb = options.TryGetBool("IsWebApplicationHost");

		if (isAspire) {

			builderType = "global::Aspire.Hosting.IDistributedApplicationBuilder";
			hostPrefix = "Aspire";

		} else if (isApi) {

			builderType = "global::Microsoft.AspNetCore.Builder.WebApplicationBuilder";
			hostPrefix = "Api";

		} else if (isWeb) {

			builderType = "global::Microsoft.AspNetCore.Components.WebAssembly.Hosting.WebAssemblyHostBuilder";
			hostPrefix = "Web";

		} else {

			builderType = "global::Microsoft.Extensions.Hosting.IHostApplicationBuilder";
			hostPrefix = "Generic";

		}

		return true;

	}

	public static string? GetValue(this AnalyzerConfigOptions options, string key) {

		return options.TryGetValue(key, out var value) && !string.IsNullOrWhiteSpace(value) ? value : null;

	}

	public static bool? GetBoolValue(this AnalyzerConfigOptions options, string key) {

		return options.TryGetValue(key, out var value) && bool.TryParse(value, out var result) ? result : null;

	}

}
