namespace Olympus.Api.Json;

public static class JsonRegistrator {

	public static void AddJsonServices(this WebApplicationBuilder builder) {

		builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(static options => {

			options.JsonSerializerOptions.PropertyNamingPolicy = null;
			options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
			options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
			options.JsonSerializerOptions.UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow;
			options.JsonSerializerOptions.Converters.Add(new JsonUtcDateTimeConverter());
			options.JsonSerializerOptions.Converters.Add(new JsonUtcDateTimeOffsetConverter());

		});

		builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(static options => {

			options.SerializerOptions.PropertyNamingPolicy = null;
			options.SerializerOptions.PropertyNameCaseInsensitive = true;
			options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
			options.SerializerOptions.UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow;
			options.SerializerOptions.Converters.Add(new JsonUtcDateTimeConverter());
			options.SerializerOptions.Converters.Add(new JsonUtcDateTimeOffsetConverter());

		});

	}

}
