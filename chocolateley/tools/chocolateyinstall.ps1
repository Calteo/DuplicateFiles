$ErrorActionPreference = 'Stop'; # stop on all errors
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"

$target = Join-Path $toolsDir "DuplicateFiles.exe"
$shortCutFile = Join-Path $([Environment]::GetFolderPath("CommonStartMenu")) "Calteo\DuplicateFiles.lnk"
Install-ChocolateyShortcut -shortcutFilePath $shortCutFile -targetPath $target -workDirectory $toolsDir -description "Duplicate Files"