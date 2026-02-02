namespace Olympus.Core.Backend.Storage;

public class StorageFileService(IStorageService storage) : StorageEntityService<StorageFile>(storage), IStorageFileService {

	public override async Task<StorageFile> UploadAsync(Stream stream, StorageFile file, CancellationToken cancellationToken = default) {

		await Storage.UploadAsync(stream, file.Bucket, file.Path, file.ContentType, cancellationToken);

		file.Size = stream.Length;

		return file;

	}

	public override Task<string> LinkAsync(StorageFile file, int expirationSeconds = 3600, CancellationToken cancellationToken = default) {

		return Storage.LinkAsync(file.Bucket, file.Path, expirationSeconds, cancellationToken);

	}

	public override Task<Stream> DownloadAsync(StorageFile file, CancellationToken cancellationToken = default) {

		return Storage.DownloadAsync(file.Bucket, file.Path, cancellationToken);

	}

	public override Task DeleteAsync(StorageFile file, CancellationToken cancellationToken = default) {

		return Storage.DeleteAsync(file.Bucket, file.Path, cancellationToken);

	}

}
