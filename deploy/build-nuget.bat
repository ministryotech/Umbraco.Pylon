@ECHO OFF

ECHO Preparing NuGet...
del *.nupkg
del *.md

ECHO Packaging for NuGet...
nuget pack ..\Umbraco.Pylon\Umbraco.Pylon.csproj -Prop Configuration=Release
nuget pack ..\Umbraco.Pylon.Autofac\Umbraco.Pylon.Autofac.csproj -Prop Configuration=Release -IncludeReferencedProjects
nuget pack ..\Umbraco.Pylon.Ninject\Umbraco.Pylon.Ninject.csproj -Prop Configuration=Release -IncludeReferencedProjects
copy ..\NUGET-README.md README.md
pause