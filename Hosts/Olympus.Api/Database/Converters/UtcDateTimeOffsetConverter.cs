using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Olympus.Api.Database;

public class UtcDateTimeOffsetConverter : ValueConverter<DateTimeOffset, DateTimeOffset> {

	public UtcDateTimeOffsetConverter() : base(value => value.ToUniversalTime(), value => value.ToUniversalTime()) { }

}
