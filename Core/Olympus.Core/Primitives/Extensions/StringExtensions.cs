using System.Text;

namespace Olympus.Core.Primitives;

public static class StringExtensions {

	extension(string value) {

		public byte[] ToByteArray() {

			return Encoding.UTF8.GetBytes(value);

		}

		public string Slugify() {

			var sluger = new Slugify.SlugHelperForNonAsciiLanguages();

			return sluger.GenerateSlug(value).Trim('-', '_', '.');

		}

		public string Crop(char oldChar) {

			return value.Replace(oldChar.ToString(), string.Empty);

		}

		public string Crop(string oldValue) {

			return value.Replace(oldValue, string.Empty);

		}

		public string? Slices(int number, char separator = ' ', StringSliceDirection direction = StringSliceDirection.LeftToRight) {

			if (number <= 0) return string.Empty;
			if (string.IsNullOrEmpty(value)) return value;

			if (direction == StringSliceDirection.LeftToRight) {

				var lastIndex = -1;

				for (var i = 0; i < number; i++) {

					var nextIndex = value.IndexOf(separator, lastIndex + 1);
					if (nextIndex == -1) return value;
					lastIndex = nextIndex;

				}

				return value.Substring(0, lastIndex);

			} else {

				var lastIndex = value.Length;

				for (var i = 0; i < number; i++) {

					var nextIndex = value.LastIndexOf(separator, lastIndex - 1);
					if (nextIndex == -1) return value;
					lastIndex = nextIndex;

				}

				return value.Substring(lastIndex + 1);

			}

		}

		public string? Slices(int number, string separator = " ", StringSliceDirection direction = StringSliceDirection.LeftToRight) {

			if (number <= 0) return string.Empty;
			if (string.IsNullOrEmpty(value)) return value;

			if (direction == StringSliceDirection.LeftToRight) {

				var lastIndex = -1;

				for (var i = 0; i < number; i++) {

					var nextIndex = value.IndexOf(separator, lastIndex + 1);
					if (nextIndex == -1) return value;
					lastIndex = nextIndex;

				}

				return value.Substring(0, lastIndex);

			} else {

				var lastIndex = value.Length;

				for (var i = 0; i < number; i++) {

					var nextIndex = value.LastIndexOf(separator, lastIndex - 1);
					if (nextIndex == -1) return value;
					lastIndex = nextIndex;

				}

				return value.Substring(lastIndex + 1);

			}

		}

	}

}
