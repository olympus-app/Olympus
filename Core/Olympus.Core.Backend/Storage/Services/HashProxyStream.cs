using System.Security.Cryptography;

namespace Olympus.Core.Backend.Storage;

public class HashProxyStream(Stream stream) : Stream {

	private readonly IncrementalHash Hasher = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);

	public string GetHashString() => Convert.ToHexString(Hasher.GetHashAndReset());

	public long TotalBytesRead { get; private set; }

	public override bool CanRead => stream.CanRead;

	public override bool CanSeek => stream.CanSeek;

	public override bool CanWrite => stream.CanWrite;

	public override long Length => stream.Length;

	public override long Position { get => stream.Position; set => stream.Position = value; }

	public override void Flush() => stream.Flush();

	public override int Read(byte[] buffer, int offset, int count) {

		var read = stream.Read(buffer, offset, count);

		if (read > 0) {

			Hasher.AppendData(buffer, offset, read);
			TotalBytesRead += read;

		}

		return read;

	}

	public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default) {

		var read = await stream.ReadAsync(buffer, cancellationToken);

		if (read > 0) {

			Hasher.AppendData(buffer.Slice(0, read).Span);
			TotalBytesRead += read;

		}

		return read;

	}

	public override void Write(byte[] buffer, int offset, int count) => stream.Write(buffer, offset, count);

	public override void SetLength(long value) => stream.SetLength(value);

	public override long Seek(long offset, SeekOrigin origin) => stream.Seek(offset, origin);

	protected override void Dispose(bool disposing) {

		if (disposing) Hasher.Dispose();

		base.Dispose(disposing);

	}

}
