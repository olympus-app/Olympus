namespace Olympus.Core.Backend.Storage;

public static class IFormFileExtensions {

	extension(IFormFile file) {

		public StorageFile AsStorageFile(string? fileName = null, string? contentType = null, StorageLocation? bucket = null, string? path = null) {

			return new StorageFile() {
				Name = fileName ?? file.FileName,
				ContentType = contentType ?? file.ContentType,
				Bucket = bucket ?? StorageLocation.Private,
				Path = path ?? "gengeral",
				Size = file.Length,
			};

		}

		public StorageImage AsStorageImage(string? fileName = null, string? contentType = null, StorageLocation? bucket = null, string? path = null) {

			return new StorageImage() {
				Name = fileName ?? file.FileName,
				ContentType = contentType ?? file.ContentType,
				Bucket = bucket ?? StorageLocation.Private,
				Path = path ?? "gengeral",
				Size = file.Length,
			};

		}

	}

}
