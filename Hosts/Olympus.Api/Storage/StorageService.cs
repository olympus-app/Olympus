using System.Collections.Concurrent;
using System.IO.Pipelines;
using Minio;
using Minio.DataModel.Args;

namespace Olympus.Api.Storage;

public class StorageService(IMinioClient client) : IStorageService {

	private static readonly SemaphoreSlim Semaphore = new(1, 1);

	private static readonly ConcurrentDictionary<string, bool> CheckedBuckets = new();

	private async Task EnsureBucketExistsAsync(StorageLocation bucket, CancellationToken cancellationToken) {

		var bucketName = bucket.Name.ToLowerInvariant();

		if (CheckedBuckets.ContainsKey(bucketName)) return;

		await Semaphore.WaitAsync(cancellationToken);

		try {

			if (CheckedBuckets.ContainsKey(bucketName)) return;

			var args = new BucketExistsArgs().WithBucket(bucketName);
			var exists = await client.BucketExistsAsync(args, cancellationToken);

			if (!exists) {

				var newBucketArgs = new MakeBucketArgs().WithBucket(bucketName);
				await client.MakeBucketAsync(newBucketArgs, cancellationToken);

			}

			CheckedBuckets.TryAdd(bucketName, true);

		} finally {

			Semaphore.Release();

		}

	}

	public Task<string> LinkAsync(StorageLocation bucket, string path, int expirationSeconds, CancellationToken cancellationToken = default) {

		var bucketName = bucket.Name.ToLowerInvariant();

		var args = new PresignedGetObjectArgs().WithBucket(bucketName).WithObject(path).WithExpiry(expirationSeconds);

		return client.PresignedGetObjectAsync(args);

	}

	public Task<Stream> DownloadAsync(StorageLocation bucket, string path, CancellationToken cancellationToken = default) {

		var bucketName = bucket.Name.ToLowerInvariant();

		var pipe = new Pipe();

		_ = Task.Run(async () => {

			try {

				var args = new GetObjectArgs().WithBucket(bucketName).WithObject(path).WithCallbackStream(async (minioStream, token) => await minioStream.CopyToAsync(pipe.Writer.AsStream(), token));

				await client.GetObjectAsync(args, cancellationToken);

				await pipe.Writer.CompleteAsync();

			} catch (Exception exception) {

				await pipe.Writer.CompleteAsync(exception);

			}

		}, cancellationToken);

		return Task.FromResult(pipe.Reader.AsStream());

	}

	public async Task UploadAsync(Stream stream, StorageLocation bucket, string path, string contentType, CancellationToken cancellationToken = default) {

		await EnsureBucketExistsAsync(bucket, cancellationToken);

		var bucketName = bucket.Name.ToLowerInvariant();

		var args = new PutObjectArgs().WithBucket(bucketName).WithObject(path).WithStreamData(stream).WithObjectSize(stream.Length).WithContentType(contentType);

		await client.PutObjectAsync(args, cancellationToken);

	}

	public Task DeleteAsync(StorageLocation bucket, string path, CancellationToken cancellationToken = default) {

		var bucketName = bucket.Name.ToLowerInvariant();

		var args = new RemoveObjectArgs().WithBucket(bucketName).WithObject(path);

		return client.RemoveObjectAsync(args, cancellationToken);

	}

}
