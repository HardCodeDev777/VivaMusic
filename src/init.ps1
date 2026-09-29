$ffmpegPath = Join-Path $PSScriptRoot "External\ffmpeg.exe"

if (Test-Path $ffmpegPath -PathType Leaf) {
	Write-Host "Ffmpeg already installed: $ffmpegPath" -ForegroundColor Green
} else {
	Write-Host "Ffmpeg not found, unpacking..." -ForegroundColor Yellow

	$zipPath = Join-Path $PSScriptRoot "Unpack\ffmpeg.zip"
	$destPath = Join-Path $PSScriptRoot "External"

	if (-not(Test-Path $zipPath -PathType Leaf)) {
		Write-Host "Error: zip archive not found at $zipPath" -ForegroundColor Red
		return
	}

	try {
	Expand-Archive -Path $zipPath -DestinationPath $destPath -Force
	Write-Host "Done." -ForegroundColor Green
	} catch {
		Write-Host "Failed to unpack ffmpeg: $_" -ForegroundColor Red
	}
}