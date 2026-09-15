$ErrorActionPreference = "Stop"

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Write-Error "Das .NET 8 SDK wurde nicht gefunden. Installiere in Visual Studio die Workload '.NET-Desktopentwicklung'."
}

$projectPath = Join-Path $PSScriptRoot "CSharpKrypt\CSharpKrypt.csproj"
dotnet run --project $projectPath
