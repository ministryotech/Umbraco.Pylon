# Umbraco.Pylon #
This project is set up to provide base classes and structures for developing well architected, developer focussed Umbraco solutions that maximise reuse and testability.

### Versioning ###
To keep it straightforward, NuGet packages of Umbraco.Pylon will always reflect the Umbraco version that they are intended to support / are dependent on with an optional supplemental fourth digit version number to indicate a version difference in Pylon itself.

The classic version of Pylon, which has since been retired, can be used on Umbraco 7.2.1 by installing version 7.2.1 or 7.2.1.1. To use the more up to date variation of Pylon on Umbraco 7.2.1 install version 7.2.1.2. The current iteration of Pylon is only available for Umbraco installations from 7.2.1 and above.

## This Documentation is Being Rewritten ##

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

### ContentAccessor \ IContentAccessor ###
The actual obtaining of content is done through an implementation of IContentAccessor, a default of which, ContentAccessor is provided for you. The ContentAccessor provides many methods for returning property elements as strongly typed data but, most importantly, all of this uses a content object that is obtained by running the overridable function GetContentFunc(). This enables the content provision to be isolated from the rest of your code and enables a clean unit testing pattern where the GetContentFunc() can be replaced in order to return desired test data. A bit link this...

```C#
var stubContentAccessor = new ContentAccessor();
var mockContent = ArticleTestData.MockContentWithBasicProperties();
var mockAuthorContent = TeamMemberTestData.MockContentWithBasicProperties(ArticleTestData.AuthorTestId);
stubContentAccessor.GetContentFunc = i => (i == ArticleTestData.AuthorTestId) ? mockAuthorContent.Object : MockContent.Object;

ObjUt = new ArticleBuilder(stubContentAccessor, stubMediaAccessor, mockTeamMemberBuilder.Object);
var result = ObjUt.Build(MockContent.Object);
```

The ContentAccessor also has a key method for obtaining the content which takes an Id called Content().

### MediaAccessor \ IMediaAccessor ###
The obtaining of media works through the MediaAccessor - The key elements are the same as the ContentAccessor, above, an overridable GetContentFunc() method and a Content() method that takes an Id. It lacks the conversion style methods but instead exposes methods to perform simple Media related taks like check that a given media item exists.

### DocumentTypeBase \ IDocumentType ###
This is an abstract class and interface pair that tie a document type definition in with the isolating content repository that allows for both dynamic content and content using the IPublishedContent interface. All of your document types and interfaces should inherit from these.

The DocumentTypeBase class has the following key Methods...
* **Get()** - Exposes it's own ContentAccessor instance to allow direct access to the content object and methods for converting it's properties. This method is protected.
* **Content** - Exposes the content as an IPublishedContent implementation. If the source content is dynamic then it is converted.
* **DynamicContent** - Exposes the dynamic content if the object is created with a dynamic source.
* **Children** - Returns the content's children.
* **HasContent** - Indicates if the document type has any content.
* **ID / Name / Url** - These are base content properties that every content item in Umbraco has so they are exposed by the base class here.

When using Pylon to build your Umbraco based app you should inherit from DocumentTypeBase to create an object for all of your document types that you will want to link to pages. It may not be necessary for small child document types such as links or list item types, but anything with an associated content page will want to have a child object. Here's a sample...

```C#
public interface ICorporatePartner : IDocumentType
{
    string SummaryText { get; }
    IPublishedContent Badge { get; }
    string ExternalPartnerUrl { get; }
}

    public class CorporatePartner : DocumentTypeBase, ICorporatePartner
    {
        public string SummaryText { get; set; }
        public IPublishedContent Badge { get; set; }
        public string ExternalPartnerUrl { get; set; }

        #region | Properties |

        /// <summary>
        /// Property strings
        /// </summary>
        public static class Properties
        {
            public const string SummaryText = "SummaryText";
            public const string Badge = "Badge";
            public const string ExternalPartnerUrl = "ExternalPartnerUrl";
        }

        #endregion
    }
```

TODO: ADD DOCUMENTTYPEFACTORY HERE WITH SAME EXAMPLE

### ContentFactoryBase \ IContentFactory ###
This is an abstract class and interface pair that wraps a ContentAccessor and a MediaAccessor. IContentFactory is implemented by IDocumentTypeFactory and ContentFactoryBase is inherrited by DocumentTypeFactoryBase to provide the direct link between a Document Type and it's associated content although you may have need for a custom implementation of a ContentFactory outside of the standard usage within a DocumentType. In the example class below we are creating a factory class that specifically deals with wrapping a file, stored in media and has no associated document type...

```C#
public interface IFileBuilder : IContentFactory
{
    FileObject Build(int fileId, string fileContent);
    FileObject Build(string fileContent);
}

public class FileBuilder : ContentFactoryBase, IFileBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileBuilder" /> class.
    /// </summary>
    /// <param name="contentAccessor">The content repository.</param>
    /// <param name="mediaAccessor">The media accessor.</param>
    public FileBuilder(IContentAccessor contentAccessor, IMediaAccessor mediaAccessor)
        : base(contentAccessor, mediaAccessor)
    { }

    /// <summary>
    /// Builds the object.
    /// </summary>
    /// <param name="fileId">The file identifier.</param>
    /// <param name="fileContent">Content of the file.</param>
    /// <returns></returns>
    public FileObject Build(int fileId, string fileContent)
    {
        var file = GetMedia.Content(fileId);
        return GetFile(fileContent, file);
    }

    /// <summary>
    /// Builds the object.
    /// </summary>
    /// <param name="fileContent">Content of the file.</param>
    /// <returns></returns>
    public FileObject Build(string fileContent)
    {
        return GetFile(fileContent);
    }

    /// <summary>
    /// Gets the file.
    /// </summary>
    /// <param name="fileContent">Content of the file.</param>
    /// <param name="file">The file.</param>
    /// <returns></returns>
    private static FileObject GetFile(string fileContent, IPublishedContent file = null)
    {
        return new FileObject
        {
            Content = fileContent,
            FileUrl = file == null ? String.Empty : file.Url,
            HasFileAttachment = file != null
        };
    }
}
```

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
* [http://www.ministryotech.co.uk/developers/open-source-projects](http://www.ministryotech.co.uk/developers/open-source-projects)
* [https://github.com/ministryotech](https://github.com/ministryotech)
* [https://bitbucket.org/ministryotech](https://bitbucket.org/ministryotech)

Most of our content is stored on both Github and Bitbucket. Our Umbraco related repositories are on Github only.

### Where can I get it? ###
You can download the package for this project from any of the following package managers...

- **NUGET** - [https://www.nuget.org/packages/https://www.nuget.org/packages/Umbraco.Pylon](https://www.nuget.org/packages/https://www.nuget.org/packages/Umbraco.Pylon)

### Contribution guidelines ###
If you would like to contribute to the project, please contact me.

The source code can be used in a simple text editor or within Visual Studio using NodeJS Tools for Visual Studio.

### Who do I talk to? ###
* Keith Jackson - keith@ministryotech.co.uk
