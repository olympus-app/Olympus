using System;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Olympus.Sdk.Tasks;

public class ProcessTemplate : Task {

	[Required]
	public string InputFile { get; set; }

	[Required]
	public string OutputFile { get; set; }

	public string[] Replacements { get; set; }

	public override bool Execute() {

		try {

			if (!File.Exists(InputFile)) {

				Log.LogError($"Template file not found: {InputFile}");

				return false;

			}

			var content = File.ReadAllText(InputFile);

			if (Replacements is not null && Replacements.Length > 0) {

				foreach (var item in Replacements) {

					var parts = item.Split([','], 2);

					if (parts.Length == 2) {

						var key = parts[0].Trim();
						var value = parts[1].Trim();

						content = content.Replace(key, value);

					}

				}

			}

			var directory = Path.GetDirectoryName(OutputFile);

			if (!string.IsNullOrEmpty(directory)) Directory.CreateDirectory(directory);

			File.WriteAllText(OutputFile, content);

			return true;

		} catch (Exception ex) {

			Log.LogErrorFromException(ex);

			return false;

		}

	}

}
