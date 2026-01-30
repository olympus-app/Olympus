using System;
using System.IO;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NUglify;
using NUglify.JavaScript;

namespace Olympus.Sdk.Tasks;

public class BundleJs : Task {

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
					builder.AppendLine(";");

				} else {

					Log.LogError($"BundleJs source file not found: {file.ItemSpec}");

				}

			}

			if (Log.HasLoggedErrors) return false;

			var result = Uglify.Js(builder.ToString(), new CodeSettings {
				OutputMode = OutputMode.SingleLine,
				LocalRenaming = LocalRenaming.KeepAll,
				ScriptVersion = ScriptVersion.EcmaScript6,
				PreserveImportantComments = false,
				MinifyCode = false,
			});

			if (result.HasErrors) {

				foreach (var error in result.Errors) {

					Log.LogError($"BundleJs Error: {error.Message}");

				}

				return false;

			}

			var directory = Path.GetDirectoryName(OutputFile);

			if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

			File.WriteAllText(OutputFile, result.Code);

			return true;

		} catch (Exception exception) {

			Log.LogErrorFromException(exception);

			return false;

		}

	}

}
