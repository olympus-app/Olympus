# pwsh

$ScriptContext = $PSScriptRoot

function Start-Olympus {

	if ((Split-Path -Leaf (Get-Location)) -ne "Olympus") { return }

	$ScriptDir = $ScriptContext
	$RepositoryPath = Split-Path -Parent $ScriptDir

	$DoMaintenance = $false
	$DoKill = $false
	$DoClean = $false
	$DoClear = $false
	$DoRestore = $false
	$DoBuild = $false
	$DoPublish = $false
	$DoPack = $false
	$DoRun = $false
	$DoWatch = $false
	$DoWatchRun = $false
	$Verbosity = "minimal"

	$TargetPath = $RepositoryPath
	$NugetOutput = Join-Path $env:USERPROFILE ".nuget\local"
	$IsSpecificTarget = $false

	$SolutionFile = Get-ChildItem -Path $RepositoryPath -Filter "*.slnx" -ErrorAction SilentlyContinue | Select-Object -First 1
	$SolutionName = ""

	if ($SolutionFile) { $SolutionName = $SolutionFile.BaseName }

	if ($args.Count -eq 0) { $DoBuild = $true }

	foreach ($arg in $args) {

		switch -Regex ($arg) {
			"^maintenance$|^m$" { $DoMaintenance = $true; continue }
			"^kill$|^k$" { $DoKill = $true; continue }
			"^clean$|^c$" { $DoClean = $true; continue }
			"^clear$|^cr$" { $DoClear = $true; continue }
			"^restore$|^re$" { $DoRestore = $true; continue }
			"^build$|^b$" { $DoBuild = $true; continue }
			"^publish$|^p$" { $DoPublish = $true; continue }
			"^pack$|^pk$" { $DoPack = $true; continue }
			"^run$|^r$" { $DoRun = $true; continue }
			"^watch$|^w$" { $DoWatch = $true; continue }
			"^watchrun$|^wr$" { $DoWatchRun = $true; continue }
			"^-v$|^--verbose$" { $Verbosity = "detailed"; continue }
			default {
				if (Test-Path $arg) {
					$TargetPath = Resolve-Path $arg
				} else {
					$TargetPath = Join-Path $RepositoryPath $arg
				}
				$IsSpecificTarget = $true
			}
		}

	}

	if ($DoMaintenance -or $DoKill) {

		Stop-Process -Name "dotnet", "VBCSCompiler" -Force -ErrorAction SilentlyContinue
		Get-Process | Where-Object { $_.ProcessName -match "^Olympus" } | Stop-Process -Force -ErrorAction SilentlyContinue

	}

	if ($DoClean) {

		dotnet clean "$TargetPath" -v "$Verbosity"

	}

	if ($DoMaintenance -or $DoClear) {

		dotnet build-server shutdown | Out-Null

		Get-ChildItem -Path "$TargetPath" -Include "bin", "obj" -Recurse -Directory -ErrorAction SilentlyContinue | Remove-Item -Recurse -Force -ErrorAction SilentlyContinue

		if ($DoRestore) {
			dotnet nuget locals all --clear
		}

	}

	if ($DoMaintenance -or $DoRestore -or $DoBuild -or $DoPublish -or $DoPack) {

		$SourceList = dotnet nuget list source

		if (!($SourceList -match "local")) {

			New-Item -ItemType Directory -Force -Path "$NugetOutput" | Out-Null
			dotnet nuget add source "$NugetOutput" -n local

		}

		$PackageSource = Join-Path $RepositoryPath "Packages"

		if (Test-Path $PackageSource) {

			Get-ChildItem -Path "$PackageSource" -Directory | ForEach-Object {

				$PackageDir = $_.FullName
				$PackageName = $_.Name
				$CachePath = Join-Path "$env:USERPROFILE\.nuget\packages" $PackageName.ToLower()

				if ($DoMaintenance -or $DoRestore) {

					if (Test-Path "$CachePath") {

						Remove-Item -Path "$CachePath" -Recurse -Force -ErrorAction SilentlyContinue

					}

				}

				$ShouldPackNow = $false

				if ($DoMaintenance -or $DoRestore) {

					$ShouldPackNow = $true

				} elseif ($DoBuild -or $DoPublish -or $DoPack) {

					$ExistingPkg = Get-ChildItem -Path "$NugetOutput" -Filter "${PackageName}*.nupkg" | Select-Object -First 1

					if (-not $ExistingPkg) {

						$ShouldPackNow = $true

					}

				}

				if ($ShouldPackNow) {

					dotnet pack "$PackageDir" -c Release -o "$NugetOutput" -v "$Verbosity"

				}

			}

		}

	}

	if ($DoMaintenance -or $DoRestore) {

		if ($IsSpecificTarget) {

			dotnet restore "$TargetPath"

		} else {

			dotnet workload restore "$($SolutionFile.FullName)"
			dotnet tool restore --tool-manifest "$(Join-Path $RepositoryPath '.config\dotnet-tools.json')"
			dotnet restore "$RepositoryPath"

		}

	}

	if ($DoBuild) {

		dotnet build "$TargetPath" -c Debug -v "$Verbosity"

	}

	if ($DoPublish) {

		dotnet publish "$TargetPath" -c Release -v "$Verbosity"

	}

	if ($DoPack) {

		dotnet pack "$TargetPath" -c Release -o "$NugetOutput" -v "$Verbosity"

	}

	if ($DoWatch -and $DoRun) {

		$DoWatchRun = $true

	}

	$BuildConfig = "Debug"

	if ($DoPublish) { $BuildConfig = "Release" }

	if ($DoRun -or $DoWatch -or $DoWatchRun) {

		if (-not $IsSpecificTarget) {

			$AspirePath1 = Join-Path $RepositoryPath "Aspire\$SolutionName.Aspire.Host"
			$AspirePath2 = Join-Path $RepositoryPath "$SolutionName.Aspire.Host"

			if (Test-Path $AspirePath1) {

				$TargetPath = $AspirePath1

			} elseif (Test-Path $AspirePath2) {

				$TargetPath = $AspirePath2

			}

		}

		if ($DoWatchRun) {

			dotnet watch --project "$TargetPath" run -c "$BuildConfig" -v "$Verbosity"

		} elseif ($DoRun) {

			dotnet run --project "$TargetPath" -c "$BuildConfig" -v "$Verbosity"

		} elseif ($DoWatch) {

			dotnet watch --project "$TargetPath" -c "$BuildConfig" -v "$Verbosity"

		}

	}

}

Set-Alias olympus Start-Olympus
Set-Alias oly Start-Olympus
Set-Alias o Start-Olympus
