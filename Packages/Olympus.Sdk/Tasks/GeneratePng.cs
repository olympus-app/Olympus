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

	public override bool Execute() {

		try {

			if (!File.Exists(InputFile)) {

				Log.LogError($"Source file not found: {InputFile}");

				return false;

			}

			Directory.CreateDirectory(Path.GetDirectoryName(OutputFile));

			var innerWidth = (uint)Math.Round(Width * Scale);
			var innerHeight = (uint)Math.Round(Height * Scale);

			var settings = new MagickReadSettings { 
                BackgroundColor = MagickColors.None,
                Width = innerWidth,
                Height = innerHeight
            };

			using var image = new MagickImage(InputFile, settings);

			image.BackgroundColor = MagickColors.None;

			image.Resize(innerWidth, innerHeight);

			if (Scale < 1.0 || innerWidth != Width || innerHeight != Height) {

				image.Extent((uint)Width, (uint)Height, Gravity.Center, MagickColors.None);

			}

			image.Strip();
			image.Write(OutputFile);

			return !Log.HasLoggedErrors;

		} catch (Exception ex) {

			Log.LogErrorFromException(ex);

			return false;

		}

	}

}
