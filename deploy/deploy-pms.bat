@ECHO OFF

ECHO Preparing NuGet...
CALL ..\..\set-nuget-key.bat
del *.nupkg
pause

ECHO Publishing to NuGet...
nuget pack ../Umbraco.Pylon/Umbraco.Pylon.csproj -Prop Configuration=Release
nuget push *.nupkg

pause