# Publish and create ready-to-upload ZIP for MonsterASP.NET
Write-Host "Publishing Bee Properties Admin Web API in Release mode..." -ForegroundColor Cyan

$publishDir = "./publish"
$zipFile = "beeproperties-deploy.zip"

if (Test-Path $publishDir) {
    Remove-Item -Recurse -Force $publishDir
}

dotnet publish beeproperties.admin.webapi.csproj -c Release -o $publishDir

if ($LASTEXITCODE -ne 0) {
    Write-Error "dotnet publish failed!"
    exit 1
}

Write-Host "Packaging deployment ZIP archive ($zipFile)..." -ForegroundColor Cyan
Add-Type -AssemblyName System.IO.Compression.FileSystem
if (Test-Path $zipFile) {
    Remove-Item -Force $zipFile
}

[System.IO.Compression.ZipFile]::CreateFromDirectory((Resolve-Path $publishDir), (Join-Path (Get-Location) $zipFile))

Write-Host "SUCCESS! Deployment package ready at: $zipFile" -ForegroundColor Green
Write-Host "Upload this ZIP file to MonsterASP.NET File Manager into /site/wwwroot and extract it." -ForegroundColor Yellow
