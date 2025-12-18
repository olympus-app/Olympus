namespace Olympus.Core.Primitives;

public static class StreamExtensions {

	extension(Stream stream) {

		public void ResetPosition() {

			if (stream.Position > 0) stream.Position = 0;

		}

	}

}
