using System.Runtime.CompilerServices;

namespace Olympus.Core.Primitives;

public static class EnumExtensions {

	extension<TEnum>(TEnum value) where TEnum : struct, Enum {

		public int Value {
			get {

				if (Unsafe.SizeOf<TEnum>() == sizeof(byte)) return Unsafe.As<TEnum, byte>(ref value);
				if (Unsafe.SizeOf<TEnum>() == sizeof(short)) return Unsafe.As<TEnum, short>(ref value);
				if (Unsafe.SizeOf<TEnum>() == sizeof(int)) return Unsafe.As<TEnum, int>(ref value);

				return Convert.ToInt32(value);

			}
		}

		public string Name => FastEnum.GetName(value) ?? value.ToString();

		public string Humanized => (FastEnum.GetName(value) ?? value.ToString()).Humanize(LetterCasing.Sentence) + '.';

	}

}
