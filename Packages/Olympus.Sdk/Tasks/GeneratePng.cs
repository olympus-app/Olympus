using System;
using System.IO;
using ImageMagick;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Olympus.Sdk.Tasks;

public class GeneratePng : Task {

	[Required]
	public string InputFile { get; set; }

	[Required]
	public string OutputFile { get; set; }

	[Required]
	public int Width { get; set; }

	[Required]
	public int Height { get; set; }

	public double Scale { get; set; } = 1.0;

	public string BackgroundColor { get; set; } = "transparent";

	public override bool Execute() {

		try {

			if (!File.Exists(InputFile)) {

				Log.LogError($"Source file not found: {InputFile}");

				return false;

			}

			var directory = Path.GetDirectoryName(OutputFile);

			if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

			var innerWidth = (uint)Math.Round(Width * Scale);
			var innerHeight = (uint)Math.Round(Height * Scale);

			var settings = new MagickReadSettings {
				Width = innerWidth,
				Height = innerHeight,
				BackgroundColor = MagickColors.None,
			};

			using var image = new MagickImage(InputFile, settings);

			if (string.IsNullOrEmpty(BackgroundColor) || BackgroundColor == "transparent") {

				image.BackgroundColor = MagickColors.None;

			} else {

				image.BackgroundColor = new MagickColor(BackgroundColor);
				image.Alpha(AlphaOption.Remove);

			}

			image.Resize(innerWidth, innerHeight);

			if (Scale < 1.0 || innerWidth != Width || innerHeight != Height) {

				image.Extent((uint)Width, (uint)Height, Gravity.Center, image.BackgroundColor);

			}

			image.Strip();
			image.Write(OutputFile);

			return !Log.HasLoggedErrors;

		} catch (Exception exception) {

			Log.LogErrorFromException(exception);

			return false;

		}

	}

}
