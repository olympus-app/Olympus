using System.Collections.Concurrent;

namespace Olympus.Core.Backend.Storage;

public abstract class FileUploadEndpoint<TEntity, TFile, TEntityWithFile, TUploadRequest, TConfiguration> : EntityEndpoint<TEntity, TUploadRequest> where TEntity : class, IEntity where TFile : class, IFileEntity, new() where TEntityWithFile : class, IEntityWithFile<TEntity, TFile>, new() where TUploadRequest : class, IFileUploadRequest where TConfiguration : class, IFileUploadConfiguration, new() {

	public IStorageService Storage { get; set; } = default!;

	private static readonly ConcurrentDictionary<Type, TConfiguration> Configurations = new();

	protected TConfiguration Configuration => field ?? Configurations.GetOrAdd(GetType(), static _ => new());

	protected virtual void StorageOptions(StorageLocation bucket = StorageLocation.Private, string? folderName = null, string? fileName = null, string? contentType = null) {

		Configuration.Bucket = bucket;

		if (folderName is not null) Configuration.FolderName = folderName;

		if (fileName is not null) Configuration.FileName = fileName;

		if (contentType is not null) Configuration.ContentType = contentType;

	}

	protected virtual IFormFile GetUpload() {

		if (Files.Count == 0) ThrowError(ErrorsStrings.Values.NoFileSent);

		return Files[0];

	}

	protected virtual IQueryable<TEntityWithFile> Query(TUploadRequest request) {

		return Database.Set<TEntityWithFile>().AsTracking().DefaultFilter(entity => entity.EntityId == request.Id);

	}

	protected virtual Task<TEntityWithFile?> ReadAsync(IQueryable<TEntityWithFile> query, TUploadRequest request, CancellationToken cancellationToken = default) {

		return query.Include(entity => entity.File).SingleOrDefaultAsync(cancellationToken);

	}

	protected virtual TEntityWithFile Initialize(IFormFile upload, TUploadRequest request) {

		var id = Guid.CreateVersion7();

		var fileName = !string.IsNullOrEmpty(Configuration.FileName) ? Configuration.FileName : upload.FileName;
		var folderName = !string.IsNullOrEmpty(Configuration.FolderName) ? Configuration.FolderName : typeof(TEntityWithFile).Name;
		var contentType = !string.IsNullOrEmpty(Configuration.ContentType) ? Configuration.ContentType : upload.ContentType;
		var baseName = Path.GetFileNameWithoutExtension(fileName);
		var extension = Path.GetExtension(fileName).ToLowerInvariant();

		return new TEntityWithFile() {
			EntityId = request.Id,
			File = new TFile() {
				Id = id,
				Name = fileName,
				BaseName = baseName,
				Extension = extension,
				ContentType = contentType,
				ContentHash = string.Empty,
				StorageBucket = Configuration.Bucket.GetLabel()!,
				StoragePath = $"{folderName.ToLower()}/{request.Id.NormalizeLower()}/{id.NormalizeLower()}{extension}",
				Size = upload.Length,
			},
		};

	}

	protected virtual async Task UploadAsync(TEntityWithFile? entity, IFormFile upload, TUploadRequest request, CancellationToken cancellationToken = default) {

		if (entity is not null && entity.File is not null) {

			await Storage.DeleteAsync(entity.File.StorageBucket, entity.File.StoragePath, cancellationToken);

		}

		if (entity is null || entity.File is null) {

			entity = Initialize(upload, request);

			Database.Set<TEntityWithFile>().Add(entity);

		}

		var stream = upload.OpenReadStream();

		await using var hashStream = new HashProxyStream(stream);

		await Storage.UploadAsync(hashStream, entity.File.StorageBucket, entity.File.StoragePath, entity.File.ContentType, cancellationToken);

		entity.File.Size = hashStream.TotalBytesRead;
		entity.File.ContentHash = hashStream.GetHashString();

		await Database.SaveChangesAsync(cancellationToken);

	}

	public override async Task<Void> HandleAsync(TUploadRequest request, CancellationToken cancellationToken) {

		var upload = GetUpload();

		var query = Query(request);

		var entity = await ReadAsync(query, request, cancellationToken);

		await UploadAsync(entity, upload, request, cancellationToken);

		return await Send.UpdatedAsync(cancellationToken);

	}

}
