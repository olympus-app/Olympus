using System.Text.Json;
namespace Olympus.Api.Json;

public class JsonUtcDateTimeConverter : JsonConverter<DateTime> {

	public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {

		if (reader.TryGetDateTime(out var date)) return date.ToUniversalTime();

		return DateTime.Parse(reader.GetString()!);

	}

	public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) {

		writer.WriteStringValue(value.ToUniversalTime().ToString(AppSettings.UtcDateTimeFormat));

	}

}
