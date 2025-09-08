namespace Olympus.Zeus.Archend;

public class ZeusOptions : AppModuleOptions {

	private static AppModule EnumValue => AppModule.Zeus;

	public static string SectionName => $"Modules:{EnumValue}";

	public override AppModule Enum { get; protected set; } = EnumValue;

	public override string Name { get; set; } = EnumValue.ToString();

	public override string Path { get; set; } = $"/{EnumValue.ToString().ToLower()}";

}
