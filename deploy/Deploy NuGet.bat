call ../../set-nuget-key.bat

del *.nupkg

nuget pack ../Umbraco.Pylon/Umbraco.Pylon.csproj -Prop Configuration=Release
nuget push *.nupkg

pause