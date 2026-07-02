$ErrorActionPreference = 'Stop'; 

$shortCutFile = Join-Path $([Environment]::GetFolderPath("CommonStartMenu")) "Calteo\DuplicateFiles.lnk"
Remove-Item $shortCutFile -Force
