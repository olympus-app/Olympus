namespace Olympus.Core.Primitives;

public static class TypeExtensions {

	extension(Type type) {

		public bool IsSimple() {

			return type.IsPrimitive
				|| type.IsEnum
				|| type == typeof(string)
				|| type == typeof(decimal)
				|| type == typeof(DateOnly)
				|| type == typeof(DateTime)
				|| type == typeof(DateTimeOffset)
				|| type == typeof(TimeSpan)
				|| type == typeof(TimeOnly)
				|| type == typeof(Guid);

		}

		public bool IsComplex() {

			return !type.IsSimple();

		}

	}

}
