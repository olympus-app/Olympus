namespace Olympus.Core.Kernel.Primitives;

public static class GuidExtensions {

	extension(Guid value) {

		public static Guid NewGuidv7() {

			return Guid.CreateVersion7();

		}

	}

}
