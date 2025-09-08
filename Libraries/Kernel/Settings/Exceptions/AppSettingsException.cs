using System.Net;

namespace Olympus.Kernel;

public class AppSettingsException(AppErrors.Keys key = AppErrors.Keys.GenericSettingsError, AppSettingsType type = AppSettingsType.Global, Exception? inner = null) : AppException(key, type.GetType().Name, type.ToString(), HttpStatusCode.InternalServerError, inner) { }
