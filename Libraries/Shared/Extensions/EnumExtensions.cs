namespace Olympus.Shared;

public static class EnumExtensions {

	public static int ToInt<TEnum>(this TEnum value) where TEnum : Enum {

		return Convert.ToInt32(value);

	}

}
