@echo off
setlocal

where dotnet >nul 2>nul
if errorlevel 1 (
    echo Das .NET 8 SDK wurde nicht gefunden.
    echo Installiere in Visual Studio die Workload ".NET-Desktopentwicklung".
    pause
    exit /b 1
)

dotnet run --project "%~dp0CSharpKrypt\CSharpKrypt.csproj"
if errorlevel 1 (
    echo.
    echo Das Projekt konnte nicht gestartet werden.
    pause
    exit /b 1
)

echo.
pause
