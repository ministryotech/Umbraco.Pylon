# Umbraco.Pylon #
This project is set up to provide base classes and structures for developing well architected, developer focussed Umbraco solutions that maximise reuse and testability.

### Versioning ###
To keep it straightforward, nuget packages of Umbraco.Pylon will always reflect the Umbraco version that they are intended to support / are dependent on.

## The Classes and Interfaces ##
### PublishedContentRepository | IPublishedContentRepository ###
Umbraco.Pylon's heart is the abstract PublishedContentRepository and it's associated interface, IPublishedContentRepository. They contain a very slim selection of key methods for accessing content. As the project grows this may increase. The PublishedContentRepository effectively provides a testable wrapper that is created by providing either an UmbracoHelper or an UmbracoContext. The wrapper then provides access to content as needed.

It's not recommended that these classes be used directly in a nicely architected site. You must create a site specific implementation of the class and interface, as indicated in the sample project by SamplePublishedContentRepository. In your specific implementation you can set up methods to return special content items such as homepage site root IDs etc. Maintaining a matching interface still allows for a good testable framework.

If you find you're adding commonly useful features to your site specific content repository, consider proposing the properties / methods be integrated into the base class within the Pylon project.

### UmbracoSite ###
In a similar way to the PublishedContentRepository this is intended to be inherrited from (See the SampleSite class in the sample code project) in order to pass in the necessary implementation of PublishedContentRepository to wrap with the Content property. This class can be considered the root of your site from a code point of view. It's largely syntactic sugar, allowing you to write code like this...

```C#
var mySite = new MyWebSite();
var url = mySite.Content.ContentUrl(123);
```

The value of mySite would generally be provided by an IOC Implementation such as Ninject or AutoFac or wrapped to make it more accessible (as done in UmbracoPylonViewPage below).

### DocumentTypeBase | IDocumentType ###
This is an abstract class and interface pair that tie a document type definition in with the isolating content repository that allows for both dynamic content and content using the IPublishedContent interface. All of your document types should inherit from these.

### LinkedViewModelBase ###
The DocumentTypeBase class is enhanced by the LinkedViewModelBase class which creates a ViewModel wrapper around any single document types.

### UmbracoPylonControllerBase ###
This is another abstract class that is intended to be inherrited from in your site as a base class for all of your controllers. It sits between your own controllers and Umbraco's 'RenderMvcController'. It gives access to content through the provided site's override of PublishedContentRepository (where provided) and adds an option using 'EnableFileCheck' that allows you to test that a template for the controller exists before attempting to render it. Again, samples for the simplest implementation in a code focussed site cam be found in the Sample code project, in this case in the SampleControllerBase class.

### UmbracoPylonViewPage ###
This final base class brings everything together. There are effectively two slight variants of this class with different generic parameters. One of these takes a model and effectively replaces the UmbracoViewPage class. All of your site views should inherit from this class when they have a defined model. The other does not take a model and exposes a dynamic object (It also wraps 'CurrentPage' with 'DynamicModel' as an effective alias for all those three people who preferred the DynamicModel to the CurrentPage syntax!). Both provide a property that allows easy access to the site's defined implementation of UmbracoSite, as follows...

```C#
var url = UmbracoSite.Content.ContentUrl(123);
```

## Using PylonSampleWeb ##
If you try and run PylonSampleWeb on it's own you'll find it doesn't work. It needs to be put on top of an existing Umbraco installation in order to see it working and the Umbraco installation must match the nearest available version of Umbraco.Pylon.

## The Ministry of Technology Open Source Products ##
Welcome to The Ministry of Technology open source products. All open source Ministry of Technology products are distributed under the MIT License for maximum re-usability. Details on more of our products and services can be found on our website at http://www.ministryotech.co.uk

Our other open source repositories can be found here...
* [https://bitbucket.org/ministryotech](https://bitbucket.org/ministryotech)
* [https://github.com/ministryotech](https://github.com/ministryotech)
* [https://github.com/tiefling](https://github.com/tiefling)

Most of our content is stored on both Github and Bitbucket. Our Umbraco related repositories are on Github only.

### Where can I get it? ###
You can download the package for this project from any of the following package managers...

- **NUGET** - [https://www.nuget.org/packages/https://www.nuget.org/packages/Umbraco.Pylon](https://www.nuget.org/packages/https://www.nuget.org/packages/Umbraco.Pylon)

### Contribution guidelines ###
If you would like to contribute to the project, please contact me.

The source code can be used in a simple text editor or within Visual Studio using NodeJS Tools for Visual Studio.

### Who do I talk to? ###
* Keith Jackson - keith@ministryotech.co.uk
