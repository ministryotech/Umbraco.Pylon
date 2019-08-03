# Umbraco.Pylon #
This project is set up to provide base classes and structures for developing well architected, developer focussed Umbraco solutions that maximise reuse and testability.

### Versioning ###
Until recently, NuGet packages of Umbraco.Pylon always reflected the Umbraco version that they were intended to support / are dependent on with an optional supplemental fourth digit version number to indicate a version difference in Pylon itself.

The down side to this was that it made keeping track of major Pylon versions difficult.

Pylon is now in it's third major version. With the advent of integrated Umbraco.ModelsBuilder, the philosophy of Pylon has adapted to be more simplified, removing complex elements that muddy the waters of minimalist high reliability coding. To simplify versioning, the main package has been renamed from Umbraco.Pylon to UmbracoPylon and direct Umbraco dependencies have been removed.

The intent is that, hopefully, any version of pylon SHOULD be compatible with any Umbraco version in it's major version number.

The current version has been tested and works with 7.10.X, 7.11.X and 7.12.X but will probably work fine with any version form 7.4.X upward.

New versions are as follows...

```
{pylon-major-version}.{umbraco-dev-version}.{minor-version}
```

* Pylon 3 is a significant change from Pylon 2 but much more streamlined. Classically versioned releases are available numbered to match Umbraco 7.10.4 and 7.11.1.
* Pylon version 2 is available for Umbraco versions 7.2.1 (Pylon 7.2.1.3) to Umbraco 7.9.6.
* The original version of Pylon, which has since been retired, can be used on Umbraco 7.2.1 by installing version 7.2.1 of Pylon.

Pylon versions are tagged in Github, so if you are looking for older version documentation - It should be there!

### Where can I get it? ###
You can download the package for this project from NuGet...

* **UmbracoPylon** - [https://www.nuget.org/packages/UmbracoPylon](/https://www.nuget.org/packages/UmbracoPylon)
* **UmbracoPylon.Autofac** - [https://www.nuget.org/packages/UmbracoPylon.Autofac](https://www.nuget.org/packages/UmbracoPylon.Autofac)
* **UmbracoPylon.Autofac** - [https://www.nuget.org/packages/UmbracoPylon.Ninject](https://www.nuget.org/packages/UmbracoPylon.Ninject)

Remember to look for the version that matches your Umbraco install.

### Contribution guidelines ###
If you would like to contribute to the project, please contact me.

### Who do I talk to? ###
* Keith Jackson - keith@ministryotech.co.uk