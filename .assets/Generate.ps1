$InputFolder = ".assets"
$OutputFolder = ".assets/Generated"

function Initialize-Output {

	if (!(Test-Path $OutputFolder)) {

		New-Item -ItemType Directory -Path $OutputFolder | Out-Null

	}

}

function Export-AppIcons {

	Initialize-Output

	$InputFile = "$InputFolder/Application/Icon (Base).svg"

	Copy-Item -Path $InputFile -Destination "$OutputFolder/icon.svg" -Force

	magick -background none "$InputFile" -define icon:auto-resize="256,128,64,48,32,16" -strip "$OutputFolder/AppIcon.ico"

}

function Export-WebIcons {

	Initialize-Output

	$InputFile = "$InputFolder/Application/Icon (Base).svg"
	$AnySizes = 16, 24, 32, 44, 48, 64, 128, 180, 192, 256, 512
	$MaskableSizes = 128, 180, 192, 256, 512

	foreach ($Size in $AnySizes) {

		$OutputFile = "$OutputFolder/icon-$Size-any.png"

		magick -background none "$InputFile" -resize $Size -strip "$OutputFile"

	}

	foreach ($Size in $MaskableSizes) {

		$OutputFile = "$OutputFolder/icon-$Size-maskable.png"

		$InnerSize = [math]::Round($Size * 0.7)

		magick -background none "$InputFile" -resize $InnerSize -gravity center -extent "$($Size)x$($Size)" -strip "$OutputFile"

	}

}

Export-AppIcons

Export-WebIcons
