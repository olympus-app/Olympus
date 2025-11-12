namespace Olympus.Core.Kernel.Primitives;

public static class StringExtensions {

	extension(string value) {

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

	}

}
