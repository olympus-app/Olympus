using System.Collections.Concurrent;
using System.IO.Pipelines;
using Minio;
using Minio.DataModel.Args;

namespace Olympus.Api.Storage;

public class StorageService(IMinioClient client) : IStorageService {

	private static readonly SemaphoreSlim Semaphore = new(1, 1);

	private static readonly ConcurrentDictionary<string, bool> CheckedBuckets = new();

	private async Task EnsureBucketExistsAsync(string storageBucket, CancellationToken cancellationToken) {

		if (CheckedBuckets.ContainsKey(storageBucket)) return;

		await Semaphore.WaitAsync(cancellationToken);

		try {

			if (CheckedBuckets.ContainsKey(storageBucket)) return;

			var args = new BucketExistsArgs().WithBucket(storageBucket);
			var bucket = await client.BucketExistsAsync(args, cancellationToken);

			if (!bucket) {

				var newBucketArgs = new MakeBucketArgs().WithBucket(storageBucket);
				await client.MakeBucketAsync(newBucketArgs, cancellationToken);

			}

			CheckedBuckets.TryAdd(storageBucket, true);

		} finally {

			Semaphore.Release();

		}

	}

	public Task<string> LinkAsync(string storageBucket, string storagePath, int expirationSeconds, CancellationToken cancellationToken = default) {

		var args = new PresignedGetObjectArgs().WithBucket(storageBucket).WithObject(storagePath).WithExpiry(expirationSeconds);

		return client.PresignedGetObjectAsync(args);

	}

	public async Task UploadAsync(Stream stream, string storageBucket, string storagePath, string contentType, CancellationToken cancellationToken = default) {

		await EnsureBucketExistsAsync(storageBucket, cancellationToken);

		var args = new PutObjectArgs().WithBucket(storageBucket).WithObject(storagePath).WithStreamData(stream).WithObjectSize(stream.Length).WithContentType(contentType);

		await client.PutObjectAsync(args, cancellationToken);

	}

	public Task<Stream> DownloadAsync(string storageBucket, string storagePath, CancellationToken cancellationToken = default) {

		var pipe = new Pipe();

		_ = Task.Run(async () => {

			try {

				var args = new GetObjectArgs().WithBucket(storageBucket).WithObject(storagePath).WithCallbackStream(async (minioStream, token) => await minioStream.CopyToAsync(pipe.Writer.AsStream(), token));

				await client.GetObjectAsync(args, cancellationToken);

				await pipe.Writer.CompleteAsync();

			} catch (Exception ex) {

				await pipe.Writer.CompleteAsync(ex);

			}

		}, cancellationToken);

		return Task.FromResult(pipe.Reader.AsStream());

	}

	public Task DeleteAsync(string storageBucket, string storagePath, CancellationToken cancellationToken = default) {

		var args = new RemoveObjectArgs().WithBucket(storageBucket).WithObject(storagePath);

		return client.RemoveObjectAsync(args, cancellationToken);

	}

}
