# Umbraco.Pylon #
This project is set up to provide base classes and structures for developing well architected, developer focussed Umbraco solutions that maximise reuse and testability.

### Versioning ###
To keep it straightforward, NuGet packages of Umbraco.Pylon will always reflect the Umbraco version that they are intended to support / are dependent on with an optional supplemental fourth digit version number to indicate a version difference in Pylon itself.

The down side to this is it makes keeping track of major Pylon versions difficult.

* Pylon is now in it's third major version from 7.10.4.1 upward (Umbraco 7.10.4). With the advent of integrated Umbraco.ModelsBuilder, the philosophy of Pylon has adapted to be more simplified, removing complex elements that muddy the waters of minimalist high reliability coding. Pylon 3 is a significant change from Pylon 2 but much more streamlined.
* Pylon version 2 is available for Umbraco versions 7.2.1 (Pylon 7.2.1.3) to Umbraco 7.9.6.
* The original version of Pylon, which has since been retired, can be used on Umbraco 7.2.1 by installing version 7.2.1 of Pylon.

Pylon versions are tagged in Github, so if you are looking for older version documentation - It should be there!

## Core Classes and Interfaces ##

### PublishedContentRepository \ IPublishedContentRepository ###
Umbraco.Pylon's heart is the abstract PublishedContentRepository and it's associated interface, IPublishedContentRepository. They contain a very slim selection of key methods for accessing content. The PublishedContentRepository effectively provides a testable wrapper that is created by providing either an UmbracoHelper or an UmbracoContext. The wrapper then provides access to content via an injectable and mockable interface.

We recommend that an IPublishedContentRepository implementation should be super light (as you can't test it directly) and act purely as a gateway for accessing content data.

You CAN create a repository without providing an UmbracoHelper or UmbracoContext but you won't get very far with it. As soon as you try to access content you will get a very explanatory error message to advise you that this is required. The use of the Umbraco property directly is discouraged now in favour of the repository implementation. The latest versions of the Pylon Controller base classes will throw an error if you try to bypass the repository.

It's not recommended that these classes be used directly in a nicely architected site. You should create a site specific implementation of the class and interface, as indicated in the example below taken from the Ministry website. In your specific implementation you can set up methods to return special content items such as homepages etc. Maintaining a matching interface still allows for a good testable framework. The base class contains the following key methods...
* **Umbraco** - Exposes the UmbracoHelper class. There is a fair argument for this to become protected or even private.
* **Context** - Exposes the UmbracoContext class. There is a fair argument for this to become protected or even private.
* **Content()** - Returns a given node Id as Content or as null as appropriate.
* **ContentExists()** - Indicates if a given Content ID exists.
* **MediaExists()** - Indicates if a given Media ID exists.
* **MediaItem()** - Returns a given media Id as a Media Item.

If you find you're adding commonly useful features to your site specific content repository, consider proposing the properties / methods be integrated into the base class within the Pylon project.

```C#
/// <summary>
/// A repository for providing access to Umbraco media nodes.
/// </summary>
public interface IMinistrywebPublishedContentRepository : IPublishedContentRepository
{
    Home RootAncestor { get; }
    Article Article(int id);
    StandardBlog BlogRoll { get; }
    Contact Contact { get; }
    DevelopersBlog DeveloperBlogRoll { get; }
    IEnumerable<Project> OssProjects { get; }
    IEnumerable<MinistryService> Services { get; }
    IEnumerable<CorporatePartner> Partners { get; }
    IEnumerable<TeamMember> Associates { get; }
    IEnumerable<Testimonial> Testimonials { get; }
}

/// <summary>
/// A repository for providing access to Umbraco media nodes.
/// </summary>
public class MinistrywebPublishedContentRepository : PublishedContentRepository, IMinistrywebPublishedContentRepository
{
    private Home rootAncestor;
    private SiteSettings siteSettings;
    private DataSources data;
    private StandardBlog blogRoll;
    private DevelopersBlog developerBlogRoll;
    private Contact contact;
        
    private IEnumerable<MinistryService> servicesList;
    private IEnumerable<Project> projectsList;
    private IEnumerable<CorporatePartner> partnersList;
    private IEnumerable<TeamMember> associatesList;
    private IEnumerable<Testimonial> testimonialsList;

    #region | Construction |

    public MinistrywebPublishedContentRepository()
    { }

    public MinistrywebPublishedContentRepository(UmbracoHelper umbraco, UmbracoContext context)
        : base(umbraco, context)
    { }

    #endregion

    #region | Core Nodes |

    public Home RootAncestor => rootAncestor ?? (rootAncestor = new Home(Umbraco.TypedContent(MinistrywebPublishedContentNodeIds.RootAncestor)));

    protected SiteSettings Settings
        => siteSettings ?? (siteSettings = new SiteSettings(Umbraco.TypedContent(MinistrywebPublishedContentNodeIds.SiteSettings)));

    protected DataSources Data
        => data ?? (data = new DataSources(Umbraco.TypedContent(MinistrywebPublishedContentNodeIds.DataSources)));

    #endregion

    public Article Article(int id)
    {
        var content = Umbraco.TypedContent(id);
        return content == null || content.Id < 1 ? null : new Article(content);
    }

    #region | Blog Rolls |

    public StandardBlog BlogRoll 
        => blogRoll ?? (blogRoll = new StandardBlog(Settings.BlogRoll));

    public DevelopersBlog DeveloperBlogRoll 
        => developerBlogRoll ?? (developerBlogRoll = new DevelopersBlog(Settings.DevelopersBlogRoll));

    #endregion

    public Contact Contact
        => contact ?? (contact = new Contact(Settings.Contact));

    public IEnumerable<Project> OssProjects
        => projectsList ?? (projectsList = Settings.OssProjectsContainer.Children<Project>());

    #region | Partners |

    public IEnumerable<CorporatePartner> Partners
        => partnersList ?? ( partnersList = Settings.PartnersContainer.Children<CorporatePartner>());

    public IEnumerable<TeamMember> Associates
        => associatesList ?? (associatesList = Settings.PartnersContainer.Children<TeamMember>());

    #endregion

    public IEnumerable<MinistryService> Services
        => servicesList ?? (servicesList = Settings.ServicesContainer.Children<MinistryService>());

    public IEnumerable<Testimonial> Testimonials
        => testimonialsList ?? (testimonialsList = Data.FirstChild<Testimonials>().Children<Testimonial>());
}
```

### UmbracoSite ###
In a similar way to the PublishedContentRepository this is intended to be inherited from (See the sample code below) in order to pass in the necessary implementation of PublishedContentRepository. This class can be considered the root of your site from a code point of view. It's largely syntactic sugar, allowing you to write code like this...

```C#
var mySite = new MyWebSite();
var url = mySite.Content.ContentUrl(123);
```

This is where you can add an acces point to globally required items that are based on content but you CAN properly unit test these by mocking IPublishedContentRepository.

The value of mySite would generally be provided by an IoC Implementation such as Ninject or AutoFac or wrapped to make it more accessible.

In the section below on 'PylonViewPage' you can see how the Ministry site uses the DependencyResolver directly (generally bad practice, but this is an unusual use case) to inject into the base view.

```C#
public interface IMinistrywebSite : IUmbracoSite<IMinistrywebPublishedContentRepository>
{
    INavigationBuilder Navigation { get; }
}

public class MinistrywebSite : UmbracoSite<IMinistrywebPublishedContentRepository>, 
    IMinistrywebSite
{
    #region | Construction |

    public MinistrywebSite(IMinistrywebPublishedContentRepository contentRepo, 
        INavigationBuilder navigationBuilder)
        : base(contentRepo)
    {
        Navigation = navigationBuilder;
    }

    #endregion

    public INavigationBuilder Navigation { get; private set; }
}
```

### PylonViewPage ###
This view base class brings everything together, providing acces to the UmbracoSite implmentation from the view, removing direct access to Umbraco and UmbracoContext and discuraging bad design. 

There are effectively 4 variants of this class with different generic parameters...
* One of these takes a model and effectively replaces the UmbracoViewPage class (with a standard, plain, UmbracoSite implmentation)
* One does not take a model and exposes content as an IPublishedContent (with a standard, plain, UmbracoSite implmentation)
* The remaining two are as above but take instances of the site specific implementations of IUmbracoSite and IPublishedContentRepository (recommended)

All provide a property that allows easy access to an implementation of UmbracoSite, as follows...

```C#
var homeUrl = UmbracoSite.Content.Home.Url;
```

I recommend creating your own site wide base classes for views and inheriting the appropriate variations of PylonViewPage to enable you to add your own layers. For the Ministry web site I wrote this...

```C#
/// <summary>
/// An abstract base class for Ministryweb views.
/// </summary>
public abstract class MinistrywebViewPage : PylonViewPage<IMinistrywebSite, IMinistrywebPublishedContentRepository>
{
    #region | Construction |

    /// <summary>
    /// Initializes a new instance of the <see cref="MinistrywebViewPage"/> class.
    /// </summary>
    protected MinistrywebViewPage()
        : base(DependencyResolver.Current.GetService<IMinistrywebSite>())
    { }

    #endregion
}

/// <summary>
/// An abstract base class for Ministryweb views.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
public abstract class MinistrywebViewPage<TModel> : PylonViewPage<IMinistrywebSite, IMinistrywebPublishedContentRepository, TModel>
{
    #region | Construction |

    /// <summary>
    /// Initializes a new instance of the <see cref="MinistrywebViewPage"/> class.
    /// </summary>
    protected MinistrywebViewPage()
        : base(DependencyResolver.Current.GetService<IMinistrywebSite>())
    { }

    #endregion
}
```
By doing this I am binding everything together through the IoC dependency resolver so I only need to register the dependencies in one place.

## Controllers ##
Umbraco.Pylon provides 3 base controllers...
* PylonMvcController - Use for route hijacking
* PylonSurfaceController - Use for handling actions to build partial results (Html.Action). Do NOT use these to produce Json.
* PylonApiController - Use to return or take in Json or XML data.
* PylonAutthorizedApiController - Uas a standard Api controller but requires back office authorization. Used generally for back office operations.

These controllers should be used in favour of the Umbraco controllers with similar names - They inherit from them but they ALSO provide access to the IUmbracoSite implementation and block direct access to UmbracoHelper and UmbracoContext (with the exception of PylonAuthorizedApiController, which allows direct access to UmbracoHelper and UmbracoContext as they may be required in the back office).

## Supporting Classes ##

### ViewStringRenderer ###
This class is used within the various controller classes and will convert the rendered output of a view into a string. This is useful for taking a view output as an HTML property for JSON output.

### HttpResponseMessageGenerator ###
Replacing the need for the Api Controller variation, this class generates an HttpMessageResponse dependent on the passed in function returning an object of some kind, null or throwing an error. The class returns a 404 or 500 as appropriate.

## Umbraco.Pylon.Autofac & Umbraco.Pylon.Ninject ##
Umbraco Pylon 3 comes with 2 extra supporting libraries that give you the code you need to quicly wire up not just Pylon, but Umbraco itself, using these two IoC containers.

### IocHelper ###
This static class provides a simple method for registering all of the dependencies required by Umbraco.Pylon with IoC. This assumes you are using Autofac as at the time of coding this was the default IoC used by Umbraco. If you want to take the plunge with a different IoC Container then the code in this class should give you an idea where to start.

## Retired Elements ##
The following elements of Pylon have been retired in Pylon 3...
* ContentAccessor \ IContentAccessor - Now done directly through the PublishedContentRepository.
* MediaAccessor \ IMediaAccessor - Now done directly through the PublishedContentRepository.
* ContentFactory \ IContentFactory
* DocumentType \ IDocumentType - Replaced by a choice of Umbraco.ModelsBuilder (recommended) or custom model generation.
* DocumentTypeFactory \ IDocumentTypeFactory
* LinkedViewModel - Pylon controllers and views will now work with any model, so this base class is now redundant.

## The Ministry of Technology Open Source Products ##
Welcome to The Ministry of Technology open source products. All open source Ministry of Technology products are distributed under the MIT License for maximum re-usability. Details on more of our products and services can be found on our website at http://www.ministryotech.co.uk

Our other open source repositories can be found here...
* [http://www.ministryotech.co.uk/developers/open-source-projects](http://www.ministryotech.co.uk/developers/open-source-projects)
* [https://github.com/ministryotech](https://github.com/ministryotech)

Our content is stored on Github. 

### Where can I get it? ###
You can download the package for this project from NuGet...

* **Umbraco.Pylon** - [https://www.nuget.org/packages/Umbraco.Pylon](/https://www.nuget.org/packages/Umbraco.Pylon)
* **Umbraco.Pylon.Autofac** - [https://www.nuget.org/packages/Umbraco.Pylon.Autofac](https://www.nuget.org/packages/Umbraco.Pylon.Autofac)
* **Umbraco.Pylon.Autofac** - [https://www.nuget.org/packages/Umbraco.Pylon.Ninject](https://www.nuget.org/packages/Umbraco.Pylon.Ninject)

Remember to look for the version that matches your Umbraco install.

### Contribution guidelines ###
If you would like to contribute to the project, please contact me.

### Who do I talk to? ###
* Keith Jackson - keith@ministryotech.co.uk
