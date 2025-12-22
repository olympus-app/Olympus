using System.Security.Cryptography;

namespace Olympus.Core.Primitives;

public static class GuidExtensions {

	extension(Guid guid) {

		public static Guid NewGuidV7() {

			return Guid.CreateVersion7();

		}

		public string NormalizeLower() {

			return guid.ToString("N");

		}

		public string NormalizeUpper() {

			return guid.ToString("N").ToUpper();

		}

		public static Guid From(int value) {

			if (value <= 0) return Guid.Empty;

			return new Guid($"00000000-0000-0000-0000-{value:D12}");

		}

		public static Guid Combine(Guid left, Guid right) {

			Span<byte> buffer = stackalloc byte[32];

			left.TryWriteBytes(buffer[..16]);
			right.TryWriteBytes(buffer[16..]);

			var hash = SHA256.HashData(buffer.ToArray());

			return new Guid(hash.AsSpan(0, 16));

		}

	}

}
