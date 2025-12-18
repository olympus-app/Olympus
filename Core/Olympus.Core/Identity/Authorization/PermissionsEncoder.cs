using System.Buffers;
using System.IO.Compression;

namespace Olympus.Core.Identity;

public static class PermissionsEncoder {

	private static readonly HashSet<int> EmptySet = [];

	private static readonly ArrayPool<byte> Pool = ArrayPool<byte>.Shared;

	public static string Encode(IEnumerable<int> ids) {

		if (ids is null) return string.Empty;

		var sorted = ids.Where(item => item >= 0).Distinct().Order().ToArray();
		if (sorted.Length == 0) return string.Empty;

		var maxBufferSize = sorted.Length * 5 + 5;
		var buffer = Pool.Rent(maxBufferSize);

		try {

			var span = buffer.AsSpan();
			var offset = 0;

			offset += WriteVarint(span, sorted.Length);

			var last = 0;

			foreach (var id in sorted) {

				offset += WriteVarint(span.Slice(offset), id - last);
				last = id;

			}

			using var outputMs = new MemoryStream();
			using (var ds = new DeflateStream(outputMs, CompressionLevel.Fastest)) {

				ds.Write(buffer, 0, offset);

			}

			if (outputMs.TryGetBuffer(out var segment)) return Convert.ToBase64String(segment);

			return Convert.ToBase64String(outputMs.ToArray());

		} finally {

			Pool.Return(buffer);

		}

	}

	public static HashSet<int> Decode(string? pack) {

		if (string.IsNullOrEmpty(pack)) return EmptySet;

		byte[] compressedBytes;

		try {

			compressedBytes = Convert.FromBase64String(pack);

		} catch {

			return EmptySet;

		}

		using var inputMs = new MemoryStream(compressedBytes);
		using var ds = new DeflateStream(inputMs, CompressionMode.Decompress);
		using var rawMs = new MemoryStream();

		ds.CopyTo(rawMs);

		var rawSpan = new ReadOnlySpan<byte>(rawMs.GetBuffer(), 0, (int)rawMs.Length);
		var offset = 0;

		var count = ReadVarint(rawSpan, ref offset);

		var permissions = new HashSet<int>(count);
		var last = 0;

		for (var i = 0; i < count; i++) {

			if (offset >= rawSpan.Length) break;

			var delta = ReadVarint(rawSpan, ref offset);
			last += delta;
			permissions.Add(last);

		}

		return permissions;

	}

	private static int WriteVarint(Span<byte> span, int value) {

		var bytesWritten = 0;
		var v = (uint)value;

		while (v >= 0x80) {

			span[bytesWritten++] = (byte)(v | 0x80);
			v >>= 7;

		}

		span[bytesWritten++] = (byte)v;

		return bytesWritten;

	}

	private static int ReadVarint(ReadOnlySpan<byte> span, ref int offset) {

		var result = 0;
		var shift = 0;

		while (true) {

			if (offset >= span.Length) return result;

			var b = span[offset++];

			if ((b & 0x80) == 0) {

				result |= b << shift;

				return result;

			}

			result |= (b & 0x7F) << shift;
			shift += 7;

		}

	}

}
