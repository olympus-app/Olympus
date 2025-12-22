using Microsoft.Extensions.Hosting;

namespace Olympus.Api.Storage;

public static class StorageRegistrator {

	public static void AddStorageServices(this WebApplicationBuilder builder) {

		builder.Services.AddSingleton<IStorageService, StorageService>();

		builder.Services.AddSingleton<IImageProcessor, ImageProcessor>();

		builder.AddMinioClient(StorageSettings.ServiceName);

	}

}
