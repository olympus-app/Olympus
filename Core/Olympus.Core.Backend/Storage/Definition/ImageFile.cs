namespace Olympus.Core.Backend.Storage;

public class ImageFile : FileEntity {

	public static string GetThumbnailPath(string path, ThumbnailSize? size = null) {

		if (size is null) return path;

		var suffix = size.Value.Name.ToLower();

		var dotIndex = path.LastIndexOf('.');

		if (dotIndex == -1) return $"{path}_{suffix}";

		return path.Insert(dotIndex, $"_{suffix}");

	}

}
