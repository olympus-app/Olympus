namespace Olympus.Core.Backend.Storage;

public abstract class StorageEntity : Entity, IStorageEntity {

	public string Name {

		get => !string.IsNullOrWhiteSpace(field) ? field : "file.bin";

		set {

			var baseName = Path.GetFileNameWithoutExtension(value);
			var extension = Path.GetExtension(value);

			if (string.IsNullOrWhiteSpace(baseName)) baseName = Guid.NewGuidV7().NormalizeLower();
			if (string.IsNullOrWhiteSpace(extension)) extension = ".bin";

			BaseName = baseName;
			Extension = extension.ToLowerInvariant();

			field = BaseName + Extension;

		}

	}

	public string BaseName {

		get => field ?? Path.GetFileNameWithoutExtension(Name);
		private set;

	}

	public string Extension {

		get => field ?? Path.GetExtension(Name).ToLowerInvariant();
		private set;

	}

	public string ContentType { get; set; } = ContentTypes.Stream;

	public StorageLocation StorageBucket { get; set; } = StorageLocation.Private;

	public string StoragePath {

		get => !string.IsNullOrWhiteSpace(field) ? field : $"general/{Name}".ToLowerInvariant();

		set {

			if (string.IsNullOrWhiteSpace(value)) {

				field = null;

				return;

			}

			var normalized = value.Replace('\\', '/').Trim('/');

			if (!Path.HasExtension(normalized)) {

				var extension = Path.GetExtension(Name);

				if (string.IsNullOrWhiteSpace(extension)) extension = ".bin";

				normalized = $"{normalized}/{Guid.NewGuidV7().NormalizeLower()}{extension.ToLowerInvariant()}";

			}

			var finalPath = normalized.Contains('/') ? normalized : $"general/{normalized}";

			field = finalPath.ToLowerInvariant();

		}

	}

	public long Size { get; set; }

}
