namespace Olympus.Core.Kernel.Primitives;

public static class EnumExtensions {

	extension<TEnum>(TEnum value) where TEnum : Enum {

		public int Value => Convert.ToInt32(value);

		public string Name => value.ToString();

	}

}
