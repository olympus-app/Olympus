namespace Olympus.Core.Primitives;

public static class GuidExtensions {

	extension(Guid value) {

		public static Guid NewGuidV7() {

			return Guid.CreateVersion7();

		}

		public string NormalizeLower() {

			return value.ToString("N");

		}

		public string NormalizeUpper() {

			return value.ToString("N").ToUpper();

		}

	}

}
