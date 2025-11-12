namespace Olympus.Core.Kernel.Settings;

public class AppSettingsException(AppErrors.Keys key = AppErrors.Keys.GenericSettingsError, AppSettingsEnvironment type = AppSettingsEnvironment.Global, Exception? inner = null) : AppException(key, type.GetType().Name, type.ToString(), inner) { }
