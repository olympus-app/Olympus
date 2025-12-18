using System.Text.Json;
namespace Olympus.Api.Json;

public class JsonUtcDateTimeOffsetConverter : JsonConverter<DateTimeOffset> {

	public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {

		if (reader.TryGetDateTime(out var date)) return date.ToUniversalTime();

		return DateTimeOffset.Parse(reader.GetString()!);

	}

	public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options) {

		writer.WriteStringValue(value.ToUniversalTime().ToString(AppSettings.UtcDateTimeFormat));

	}

}
