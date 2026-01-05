using System;
using System.IO;
using ImageMagick;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Olympus.Sdk.Tasks;

public class GenerateIco : Task {

	[Required]
	public string InputFile { get; set; }

	[Required]
	public string OutputFile { get; set; }

	[Required]
	public string Sizes { get; set; }

	public override bool Execute() {

		try {

			if (!File.Exists(InputFile)) {

				Log.LogError($"Source file not found: {InputFile}");

				return false;

			}

			var directory = Path.GetDirectoryName(OutputFile);

			if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

			using var collection = new MagickImageCollection();

			var sizes = Sizes.Split([';'], StringSplitOptions.RemoveEmptyEntries);

			foreach (var sizeStr in sizes) {

				if (uint.TryParse(sizeStr, out var size)) {

					var settings = new MagickReadSettings {
						Width = size,
						Height = size,
						BackgroundColor = MagickColors.None,
					};

					var image = new MagickImage(InputFile, settings);

					image.Strip();
					image.Resize(size, size);

					collection.Add(image);

				}

			}

			collection.Write(OutputFile);

			return !Log.HasLoggedErrors;

		} catch (Exception ex) {

			Log.LogErrorFromException(ex);

			return false;

		}

	}

}
