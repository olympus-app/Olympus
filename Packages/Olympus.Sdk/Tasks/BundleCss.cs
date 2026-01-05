using System;
using System.IO;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NUglify;
using NUglify.Css;

namespace Olympus.Sdk.Tasks;

public class BundleCss : Task {

	[Required]
	public ITaskItem[] InputFiles { get; set; }

	[Required]
	public string OutputFile { get; set; }

	public override bool Execute() {

		try {

			var builder = new StringBuilder();

			foreach (var file in InputFiles) {

				if (File.Exists(file.ItemSpec)) {

					builder.AppendLine(File.ReadAllText(file.ItemSpec));

				} else {

					Log.LogError($"BundleCss source file not found: {file.ItemSpec}");
				}

			}

			if (Log.HasLoggedErrors) return false;

			var result = Uglify.Css(builder.ToString(), new CssSettings {
				OutputMode = OutputMode.SingleLine,
				CommentMode = CssComment.None,
				ColorNames = CssColor.Hex,
			});

			if (result.HasErrors) {

				foreach (var error in result.Errors) {

					Log.LogError($"BundleCss Error: {error.Message}");

				}

				return false;

			}

			var directory = Path.GetDirectoryName(OutputFile);

			if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

			File.WriteAllText(OutputFile, result.Code);

			return true;

		} catch (Exception ex) {

			Log.LogErrorFromException(ex);

			return false;

		}

	}

}
