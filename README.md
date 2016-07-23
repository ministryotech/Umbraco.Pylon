# Umbraco.Pylon #
This project is set up to provide base classes and structures for developing well architected, developer focussed Umbraco solutions that maximise reuse and testability.

### Versioning ###
To keep it straightforward, NuGet packages of Umbraco.Pylon will always reflect the Umbraco version that they are intended to support / are dependent on with an optional supplemental fourth digit version number to indicate a version difference in Pylon itself.

The classic version of Pylon, which has since been retired, can be used on Umbraco 7.2.1 by installing version 7.2.1 or 7.2.1.1. To use the more up to date variation of Pylon on Umbraco 7.2.1 install version 7.2.1.3. The current iteration of Pylon is only available for Umbraco installations from 7.2.1 and above.

## Core Classes and Interfaces ##

### PublishedContentRepository \ IPublishedContentRepository ###
Umbraco.Pylon's heart is the abstract PublishedContentRepository and it's associated interface, IPublishedContentRepository. They contain a very slim selection of key methods for accessing content. The PublishedContentRepository effectively provides a testable wrapper that is created by providing either an UmbracoHelper or an UmbracoContext. The wrapper then provides access to content as needed using provided ContentAccessor and MediaAccessor instances (see below for details on these). 

You CAN create a repository without providing an UmbracoHelper or UmbracoContext but you won't get very far with it. As soon as you try to access content you will get a very explanatory error message to advise you that this is required. The use of the Umbraco property directly is discouraged now in favour of the GetContent and GetMedia properties that return the passed in instances of the ContentAccessor and MediaAccessor classes. The Umbraco property is used internally by these classes when accessing real data. While testing you can provide stub implementations of the ContentAccessor and MediaAccessor that return fake data to enable most of your methods on this calss and any dependencies to be well tested. The ContentAccessor and MediaAccessor are explained in more detail in their own sections below.

It's not recommended that these classes be used directly in a nicely architected site. You must create a site specific implementation of the class and interface, as indicated in the sample project by SamplePublishedContentRepository or in the example below taken from the Ministry website. In your specific implementation you can set up methods to return special content items such as homepage site root IDs etc. Maintaining a matching interface still allows for a good testable framework. The base class contains the following key methods...
* **GetContent** - A protected property containing an instance of IContentAccessor for accessing site content.
* **GetMedia** - A protected property containing an instance of IMediaAccessor for accessing site media.
* **Umbraco** - Exposes the UmbracoHelper class - This is generally used by the GetContent and GetMedia properties now. There is a fair argument for this to become protected or even private.
* **HasValidUmbracoHelper** - Will indicate to the caller if the UmbracoHelper instance is correctly populated or not. Useful for avoiding nasty exceptions.
* **Content()** - Returns a given node Id as Content or as null as appropriate.
* **ContentUrl()** - Returns the Url linked to the given Content node ID.
* **MediaExists()** - Indicates if a given Media ID exists.
* **MediaItem()** - Returns a given media Id as a Media Item.
* **MediaUrl()** - Returns the Url linked to the given media ID.
In addition several methods are provided for accessing Media and Content directly which all through to the methods in the relevant accessors. These are Obsolete and the accessor alternatives should be used instead when writing new code.

If you find you're adding commonly useful features to your site specific content repository, consider proposing the properties / methods be integrated into the base class within the Pylon project.

```C#
/// <summary>
/// A repository for providing access to Umbraco media nodes.
/// </summary>
public interface IMinistrywebPublishedContentRepository : IPublishedContentRepository
{
    IPublishedContent RootAncestor { get; }
    string RootAncestorName { get; }
    IArticle Article(int id);
    IArticle Article(IPublishedContent content);
    IList<IArticle> ArticlesByServiceCategory(string serviceCategory);
    bool CategoryHasArticles(string serviceCategory);
    IBlogRoll BlogRoll { get; }
    IBlogRoll DeveloperBlogRoll { get; }
    IBlogRoll BlogRollByType(BlogType type);
    IEnumerable<IService> Services { get; }
}


/// <summary>
/// A repository for providing access to Umbraco media nodes.
/// </summary>
[UsedImplicitly]
public class MinistrywebPublishedContentRepository : PublishedContentRepository, IMinistrywebPublishedContentRepository
{
    private IArticleBuilder _articleBuilder;
    private IBlogRollBuilder _blogRollBuilder;
    private IServiceBuilder _serviceBuilder;
    private IPublishedContent _rootAncestor;
    private IList<IService> _servicesList;

    /// <summary>
    /// Initializes a new instance of the <see cref="MinistrywebPublishedContentRepository" /> class.
    /// </summary>
    /// <param name="contentAccessor">The content accessor.</param>
    /// <param name="mediaAccessor">The media accessor.</param>
    /// <param name="blogRollBuilder">The blog roll builder.</param>
    /// <param name="articleBuilder">The article builder.</param>
    /// <param name="serviceBuilder">The service builder.</param>
    public MinistrywebPublishedContentRepository(IContentAccessor contentAccessor, 
        IMediaAccessor mediaAccessor, IBlogRollBuilder blogRollBuilder, 
        IArticleBuilder articleBuilder, IServiceBuilder serviceBuilder)
        : base(contentAccessor, mediaAccessor)
    {
        BindContentDependencies(blogRollBuilder, articleBuilder, serviceBuilder);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MinistrywebPublishedContentRepository" /> class.
    /// </summary>
    /// <param name="contentAccessor">The content accessor.</param>
    /// <param name="mediaAccessor">The media accessor.</param>
    /// <param name="blogRollBuilder">The blog roll builder.</param>
    /// <param name="articleBuilder">The article builder.</param>
    /// <param name="projectBuilder">The project builder.</param>
    /// <param name="serviceBuilder">The service builder.</param>
    /// <param name="summaryBlockBuilder">The summary block builder.</param>
    /// <param name="corporatePartnerBuilder">The corporate partner builder.</param>
    /// <param name="teamMemberBuilder">The team member builder.</param>
    /// <param name="testimonialBuilder">The testimonial builder.</param>
    /// <param name="context">The umbraco context.</param>
    public MinistrywebPublishedContentRepository(IContentAccessor contentAccessor, 
        IMediaAccessor mediaAccessor, IBlogRollBuilder blogRollBuilder, 
        IArticleBuilder articleBuilder, IServiceBuilder serviceBuilder, 
        UmbracoContext context)
        : base(contentAccessor, mediaAccessor, context)
    {
        BindContentDependencies(blogRollBuilder, articleBuilder, serviceBuilder);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MinistrywebPublishedContentRepository" /> class.
    /// </summary>
    /// <param name="contentAccessor">The content accessor.</param>
    /// <param name="mediaAccessor">The media accessor.</param>
    /// <param name="blogRollBuilder">The blog roll builder.</param>
    /// <param name="articleBuilder">The article builder.</param>
    /// <param name="projectBuilder">The project builder.</param>
    /// <param name="serviceBuilder">The service builder.</param>
    /// <param name="summaryBlockBuilder">The summary block builder.</param>
    /// <param name="corporatePartnerBuilder">The corporate partner builder.</param>
    /// <param name="teamMemberBuilder">The team member builder.</param>
    /// <param name="testimonialBuilder">The testimonial builder.</param>
    /// <param name="umbraco">The umbraco helper.</param>
    public MinistrywebPublishedContentRepository(IContentAccessor contentAccessor, 
        IMediaAccessor mediaAccessor, IBlogRollBuilder blogRollBuilder, 
        IArticleBuilder articleBuilder, IServiceBuilder serviceBuilder, 
        UmbracoHelper umbraco)
        : base(contentAccessor, mediaAccessor, umbraco)
    {
        BindContentDependencies(blogRollBuilder, articleBuilder, serviceBuilder);
    }

    /// <summary>
    /// Binds the content dependencies.
    /// </summary>
    /// <param name="blogRollBuilder">The blog roll builder.</param>
    /// <param name="articleBuilder">The article builder.</param>
    /// <param name="serviceBuilder">The service builder.</param>
    private void BindContentDependencies(IBlogRollBuilder blogRollBuilder, 
        IArticleBuilder articleBuilder, IServiceBuilder serviceBuilder)
    {
        _articleBuilder = articleBuilder;
        _blogRollBuilder = blogRollBuilder;
        _serviceBuilder = serviceBuilder;
    }

    /// <summary>
    /// Gets the root ancestor.
    /// </summary>
    public IPublishedContent RootAncestor
    {
        get 
        { 
            return _rootAncestor 
                ?? (_rootAncestor = Umbraco.Content(MinistrywebPublishedContentNodeIds.RootAncestor)); 
        }
    }

    /// <summary>
    /// Gets the name of the root ancestor.
    /// </summary>
    public string RootAncestorName
    {
        get { return RootAncestor.Name; }
    }

    /// <summary>
    /// Gets an article with the specified ID.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public IArticle Article(int id)
    {
        var content = GetContent.Content(id);
        return content == null || content.Id < 1 ? null : Article(content);
    }

    /// <summary>
    /// Geta an article from the specified content.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    public IArticle Article(IPublishedContent content)
    {
        var article = (Article)_articleBuilder.Build(content);
        return article;
    }

    /// <summary>
    /// Gets all of the articles associated with a service category.
    /// </summary>
    /// <param name="serviceCategory">The service category.</param>
    /// <returns></returns>
    public IList<IArticle> ArticlesByServiceCategory(string serviceCategory)
    {
        var allArticleContent = GetAllArticleContent();

        return (from item in allArticleContent 
                let prop = item.GetProperty(Models.Article.Properties.Categories) 
                where prop != null && prop.HasValue 
                                    && prop.Value != null 
                                    && prop.Value.ToString().Contains(serviceCategory) 
                select Article(item)).ToList();
    }

    /// <summary>
    /// Gets a flag to indicate if the specified service category has any articles.
    /// </summary>
    /// <param name="serviceCategory">The service category.</param>
    /// <returns></returns>
    public bool CategoryHasArticles(string serviceCategory)
    {
        return ArticlesByServiceCategory(serviceCategory).Any();
    }

    /// <summary>
    /// Gets the blog roll.
    /// </summary>
    public IBlogRoll BlogRoll
    {
        get
        {
            return _blogRollBuilder.Build(GetContent.Content(MinistrywebPublishedContentNodeIds.BlogRoll), 
                BlogType.Standard);
        }
    }

    /// <summary>
    /// Gets the developer blog roll.
    /// </summary>
    public IBlogRoll DeveloperBlogRoll
    {
        get
        {
            return _blogRollBuilder.Build(GetContent.Content(MinistrywebPublishedContentNodeIds.DeveloperBlogRoll), 
                BlogType.Developer);
        }
    }

    /// <summary>
    /// Gets a blog roll for a specific type.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>A blog roll.</returns>
    public IBlogRoll BlogRollByType(BlogType type)
    {
        return type == BlogType.Developer ? DeveloperBlogRoll : BlogRoll;
    }

    /// <summary>
    /// Gets the services.
    /// </summary>
    public IEnumerable<IService> Services
    {
        get
        {
            LoadList(ref _servicesList, MinistrywebPublishedContentNodeIds.ServiceContainer, 
                _serviceBuilder, true);
            return _servicesList;
        }
    }

    /// <summary>
    /// Gets all of the article type content.
    /// </summary>
    /// <returns></returns>
    private IEnumerable<IPublishedContent> GetAllArticleContent()
    {
        var allArticleContent = new List<IPublishedContent>();
        allArticleContent.AddRange(GetContent.Content(MinistrywebPublishedContentNodeIds.BlogRoll).Children());
        allArticleContent.AddRange(GetContent.Content(MinistrywebPublishedContentNodeIds.DeveloperBlogRoll).Children());
        allArticleContent = allArticleContent.OrderByDescending(c => c.CreateDate).ToList();
        return allArticleContent;
    }

    /// <summary>
    /// Loads the lists.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="listToPopulate">The list to populate.</param>
    /// <param name="rootId">The root identifier.</param>
    /// <param name="modelBuilder">The model builder.</param>
    /// <param name="lazy">if set to <c>true</c> [lazy].</param>
    private void LoadList<T>(ref IList<T> listToPopulate, int rootId, IDocumentTypeFactory<T> modelBuilder, bool lazy = false)
        where T : IDocumentType
    {
        if (lazy && listToPopulate != null) return;

        var root = GetContent.Content(rootId);
        listToPopulate = new List<T>();

        foreach (var item in root.Children)
        {
            if (modelBuilder.IsOfValidDocumentType(item))
            {
                listToPopulate.Add(modelBuilder.Build(item));
            }
        }
    }
}
```

### UmbracoSite ###
In a similar way to the PublishedContentRepository this is intended to be inherited from (See the SampleSite class in the sample code project and the sample below) in order to pass in the necessary implementation of PublishedContentRepository to wrap with the Content property. This class can be considered the root of your site from a code point of view. It's largely syntactic sugar, allowing you to write code like this...

```C#
var mySite = new MyWebSite();
var url = mySite.Content.ContentUrl(123);
```

The value of mySite would generally be provided by an IoC Implementation such as Ninject or AutoFac or wrapped to make it more accessible. In order to allow a good IoC implementation in my own sites I normally use a static class like this one below to load the site class up and store it...

```C#
public static class Construct
{
    private static IMyWebSite _site;

    /// <summary>
    /// Gets the site object.
    /// </summary>
    public static IMyWebSite Site
    {
        get 
        { 
            return _site ?? (_site = DependencyResolver.Current.GetService<IMyWebSite>()); 
        }
    }
}
```
I can then simply call Construct.Site.Content.xxx whenever I need to access content in a static way from views etc. Your implementation of UmbracoSite is a good place for site wide elements to live. The base class contains a single publicly accessible property, `Content`, which exposes the site's implementation of IPublishedContentRepository. A sample site class would look like this - this class exposes both the key content and the custom navigation...

```C#
public interface IMinistrywebSite : IUmbracoSite<IMinistrywebPublishedContentRepository>
{
    INavigationBuilder Navigation { get; }
}

public class MinistrywebSite : UmbracoSite<IMinistrywebPublishedContentRepository>, 
    IMinistrywebSite
{

    /// <summary>
    /// Initializes a new instance of the <see cref="MinistrywebSite" /> class.
    /// </summary>
    /// <param name="contentRepo">The content repo.</param>
    /// <param name="navigationBuilder">The navigation builder.</param>
    public MinistrywebSite(IMinistrywebPublishedContentRepository contentRepo, 
        INavigationBuilder navigationBuilder)
        : base(contentRepo)
    {
        Navigation = navigationBuilder;
    }

    public INavigationBuilder Navigation { get; private set; }
}
```

### ContentAccessor \ IContentAccessor ###
The actual obtaining of content is done through an implementation of IContentAccessor, a default of which, ContentAccessor is provided for you. The ContentAccessor provides many methods for returning property elements as strongly typed data but, most importantly, all of this uses a content object that is obtained by running the overridable function GetContentFunc(). This enables the content provision to be isolated from the rest of your code and enables a clean unit testing pattern where the GetContentFunc() can be replaced in order to return desired test data. A bit like this...

```C#
var stubContentAccessor = new ContentAccessor();
var mockContent = ArticleTestData.MockContentWithBasicProperties();
var mockAuthorContent = TeamMemberTestData.MockContentWithBasicProperties(ArticleTestData.AuthorTestId);
stubContentAccessor.GetContentFunc = i => (i == ArticleTestData.AuthorTestId) 
                                           ? mockAuthorContent.Object 
                                           : MockContent.Object;

ObjUt = new ArticleBuilder(stubContentAccessor, stubMediaAccessor, mockTeamMemberBuilder.Object);
var result = ObjUt.Build(MockContent.Object);
```

The ContentAccessor also has a key method for obtaining the content which takes an Id called Content().

### MediaAccessor \ IMediaAccessor ###
The obtaining of media works through the MediaAccessor - The key elements are the same as the ContentAccessor, above, an overridable GetContentFunc() method and a Content() method that takes an Id. It lacks the conversion style methods but instead exposes methods to perform simple Media related tasks like checking that a given media item exists.

### ContentFactory \ IContentFactory ###
This is an abstract class and interface pair that wraps a ContentAccessor and a MediaAccessor. IContentFactory is implemented by IDocumentTypeFactory and ContentFactory is inherited by DocumentTypeFactory to provide the direct link between a Document Type and it's associated content although you may have need for a custom implementation of a ContentFactory outside of the standard usage within a DocumentType. In the example class below we are creating a factory class that specifically deals with wrapping a file, stored in media and has no associated document type...

```C#
public interface IFileBuilder : IContentFactory
{
    FileObject Build(int fileId, string fileContent);
    FileObject Build(string fileContent);
}

public class FileBuilder : ContentFactory, IFileBuilder
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

## Document Types, Models and Views ##

### DocumentType \ IDocumentType ###
This is an abstract class and interface pair that tie a document type definition in with the isolating content repository that allows for both dynamic content and content using the IPublishedContent interface. All of your document types and interfaces should inherit from these.

The DocumentType class has the following key Methods...
* **Get()** - Exposes it's own ContentAccessor instance to allow direct access to the content object and methods for converting it's properties. This method is protected.
* **Content** - Exposes the content as an IPublishedContent implementation. If the source content is dynamic then it is converted.
* **DynamicContent** - Exposes the dynamic content if the object is created with a dynamic source.
* **Children** - Returns the content's children.
* **HasContent** - Indicates if the document type has any content.
* **ID / Name / Url** - These are base content properties that every content item in Umbraco has so they are exposed by the base class here.

When using Pylon to build your Umbraco based app you should inherit from DocumentType to create an object for all of your document types that you will want to link to pages. It may not be necessary for small child document types such as links or list item types, but anything with an associated content page will want to have a defined object. Here's a sample...

```C#
public interface ICorporatePartner : IDocumentType
{
    string SummaryText { get; }
    IPublishedContent Badge { get; }
    string ExternalPartnerUrl { get; }
}

    public class CorporatePartner : DocumentType, ICorporatePartner
    {
        public string SummaryText { get; set; }
        public IPublishedContent Badge { get; set; }
        public string ExternalPartnerUrl { get; set; }

        /// <summary>
        /// Property strings
        /// </summary>
        public static class Properties
        {
            public const string SummaryText = "SummaryText";
            public const string Badge = "Badge";
            public const string ExternalPartnerUrl = "ExternalPartnerUrl";
        }
    }
```

### DocumentTypeFactory \ IDocumentTypeFactory ###
This is a generic pairing of an interface and class to factory build an implementation of IDocumentType. The class and interface are very simple and inherit from ContentFactory and IContentFactory respectively to provide access to a ContentAccessor and MediaAccessor for constructing the IDocumentType implementation. The base class has a single protected method `InitBuild()` which should be called by the `Build()` method of any implementation passing in the initial content. It contains the following key methods...
* **Build()** - A default overridable implementation of a method that takes an IPublishedContent instance and turns it into an IDocumentType object. This is generally overridden to populate the object you want to create with data although, if the content you are wrapping just has the key elements in the base DocumentType class (Name, Url etc.), then the standard `Build()` method will do. Any overridden `Build()` method should call `InitBuild()` to initiate the key object data first.
* **IsOfValidDocumentType()** - This is an abstract declaration that must be implemented and allows calling code to interrogate whether the passed in content is of the correct type to be processed by the factory. This is extremely useful when parsing child content of multiple types.

A sample factory would look like this...
```C#
public interface ICorporatePartnerBuilder : IDocumentTypeFactory<ICorporatePartner>
{ }

public class CorporatePartnerBuilder : DocumentTypeFactory<CorporatePartner, ICorporatePartner>, 
    ICorporatePartnerBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CorporatePartnerBuilder" /> class.
    /// </summary>
    /// <param name="contentAccessor">The content accessor.</param>
    /// <param name="mediaAccessor">The media accessor.</param>
    public CorporatePartnerBuilder(IContentAccessor contentAccessor, 
        IMediaAccessor mediaAccessor)
        : base(contentAccessor, mediaAccessor)
    { }

    /// <summary>
    /// Builds the specified content.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    public override ICorporatePartner Build(IPublishedContent content)
    {
        var result = InitBuild(content);

        result.SummaryText = Get.StringValue(CorporatePartner.Properties.SummaryText);
        result.ExternalPartnerUrl = Get.StringValue(CorporatePartner.Properties.ExternalPartnerUrl);
        result.Badge = GetMedia.Content(Get.NumericValue(CorporatePartner.Properties.Badge));

        return result;
    }

    /// <summary>
    /// Determines whether the content provided is of a valid document type for this builder.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    public override bool IsOfValidDocumentType(IPublishedContent content)
    {
        return content.DocumentTypeAlias == "CorporatePartner";
    }
}
```

The strategy of using calls to InitBuild() rather than separating the code to populate from content in a different method was intentional as this gives far more flexibility to create additional overloaded versions of Build() that take different parameters. For example, the following code allows the same builder to be used for two different blogs within a site...
```C#
/// <summary>
/// Builds the specified content.
/// </summary>
/// <param name="content">The content.</param>
/// <returns></returns>
public override IBlogRoll Build(IPublishedContent content)
{
    return Build(content, BlogType.Standard);
}

/// <summary>
/// Builds a blog roll for the specified content.
/// </summary>
/// <param name="content">The content.</param>
/// <param name="blogType">Type of the blog.</param>
/// <returns></returns>
/// <exception cref="System.ArgumentException">Invalid Blog Type;blogType</exception>
public IBlogRoll Build(IPublishedContent content, BlogType blogType)
{
    var result = InitBuild(content);

    result.Type = blogType;
    result.ArticlesPerPage = Get.NumericValue(BlogRoll.Properties.ArticlesPerPage);

    switch (blogType)
    {
        case BlogType.Standard:
            result.FeedName = "Blog";
            result.NavSource = "Main";
            result.UrlRoot = "blog";
            break;
        case BlogType.Developer:
            result.FeedName = "Developers Blog";
            result.NavSource = "Developers";
            result.UrlRoot = "developers/blog";
            break;
        default:
            throw new ArgumentException("Invalid Blog Type", "blogType");
    }

    var articles = GetArticles(content);
    result.Articles = articles.OrderByDescending(a => a.DateWritten).ToList();

    return result;
}
```

### LinkedViewModel ###
The DocumentType class is enhanced by the LinkedViewModel class which creates a ViewModel wrapper around any single document types. ViewModels in a Pylon based app are completely optional. Depending on your code style you may do manipulation of data in your views or, if you prefer more solidly testable code, you may prefer to use a ViewModel class to structure the data from a DocumentType for you to keep the View code as simple as humanly possible. 

A ViewModel class linked to a DocumentType must implement the abstract method InitModel() which is responsible for populating the ViewModel's properties from the passed in IDocumentType implementation. When the InnerObject property is set the InitModel() method is automatically triggered. A sample ViewModel could look like this...

```C#
public class ArticleViewModel : LinkedViewModel<IArticle>
{
    #region | Construction |

    /// <summary>
    /// Initializes a new instance of the <see cref="ArticleViewModel" /> class.
    /// </summary>
    /// <param name="article">The article.</param>
    public ArticleViewModel(IArticle article)
    {
        InnerObject = article;
    }

    /// <summary>
    /// Initializes the model.
    /// </summary>
    protected override void InitModel()
    {
        BodyText = InnerObject.BodyText;
        Categories = InnerObject.Categories;
        DisplayDate = InnerObject.DateWritten.ToString("dd/MM/yyyy");
        ParentBlog = InnerObject.ParentBlog;

        AuthorName = InnerObject.Author != null
            ? (InnerObject.Author.FirstName + " " + InnerObject.Author.LastName).Trim()
            : String.Empty;

        AuthorUrl = InnerObject.Author != null ? InnerObject.Author.Url : String.Empty;
        AvatarPath = InnerObject.Author != null ? InnerObject.Author.AvatarPath : String.Empty;
        Tags = new TagBuilder(InnerObject).BuildString();

        if (InnerObject.LinkedImage != null)
        {
            LinkedImageUrl = InnerObject.LinkedImage.Url;
        }
    }

    #endregion

    public string BodyText { get; set; }
    public IEnumerable<String> Categories { get; set; }
    public string Tags { get; set; }
    public string DisplayDate { get; set; }
    public string AuthorName { get; set; }
    public string AuthorUrl { get; set; }
    public string AvatarPath { get; set; }

    /// <summary>
    /// Gets a value indicating whether to show a linked image.
    /// </summary>
    /// <value>
    ///   <c>true</c> if show a linked image; otherwise, <c>false</c>.
    /// </value>
    public bool ShowLinkedImage
    {
        get { return !String.IsNullOrEmpty(LinkedImageUrl); }
    }

    public string LinkedImageUrl { get; set; }
    public BlogType ParentBlog { get; set; }

    /// <summary>
    /// Gets the list of Categorieses as markup.
    /// </summary>
    /// <returns>Html markup.</returns>
    public MvcHtmlString CategoriesAsMarkup()
    {
        var categoryBuilder = new StringBuilder();
        foreach (var item in Categories)
        {
            if (categoryBuilder.Length > 0) { categoryBuilder.Append(", "); }
            categoryBuilder.Append("<a href='/services/" + item.ToLower().Replace(" ", "-") + "'>");
            categoryBuilder.Append(item);
            categoryBuilder.Append("</a>");
        }

        return new MvcHtmlString(categoryBuilder.ToString());
    }
}
```

The ViewModel should never expose the InnerObject itself. This allows ViewModels to be easily mocked for testing and ensures no innapropriate leaks of data. To access properties on the inner IDocumentType implementation the ViewModel should be coded to pass the data through to it's own properties. A pragmatic approach is often necessary as to whether a given page is best served a DocumentType implementation or a custom ViewModel.

### PylonViewPage ###
This final base class brings everything together. There are effectively two slight variants of this class with different generic parameters. One of these takes a model and effectively replaces the UmbracoViewPage class. All of your site views should inherit from this class when they have a defined model. The other does not take a model and exposes a dynamic object (It also wraps `CurrentPage` with `DynamicModel` as an effective alias for all those three people who preferred the DynamicModel to the CurrentPage syntax!). Both provide a property that allows easy access to the site's defined implementation of UmbracoSite, as follows...

```C#
var url = UmbracoSite.Content.ContentUrl(123);
```

I recommend creating your own site wide base classes for views and inheriting the appropriate variations of UmbracoPylonViewPage to enable you to add your own layers. For the Ministry web site I wrote this...

```C#
public abstract class MinistrywebViewPage 
    : PylonViewPage<IMinistrywebSite, IMinistrywebPublishedContentRepository>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MinistrywebViewPage"/> class.
    /// </summary>
    protected MinistrywebViewPage()
        : base(Construct.Site)
    { }
}

public abstract class MinistrywebViewPage<TModel> : PylonViewPage<IMinistrywebSite, 
    IMinistrywebPublishedContentRepository, TModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MinistrywebViewPage"/> class.
    /// </summary>
    protected MinistrywebViewPage()
        : base(Construct.Site)
    { }
}
```
By doing this I am using the static Construct declaration to bind everything together through the IoC dependency resolver so I only need to register the dependencies in one place.

## Controllers ##
Controllers in Umbraco.Pylon have been deprecated and may have been removed entirely at the point of reading. I now recommend that controllers be created as thin as possible with a supporting service class (which can then be tested) passed in. The complexity behind maintaining these as testable controllers made the code far more complex and unmanageable than it needed to be for relatively little gain.

## Supporting Classes ##

### ViewStringRenderer ###
This class is used within the various controller classes and will convert the rendered output of a view into a string. This is useful for taking a view output as an HTML property for JSON output.

### HttpResponseMessageGenerator ###
Replacing the need for the Api Controller variation, this class generates an HttpMessageResponse dependent on the passed in function returning an object of some kind, null or throwing an error. The class returns a 404 or 500 as appropriate.

### IocHelper ###
This static class provides a simple method for registering all of the dependencies required by Umbraco.Pylon with IoC. This assumes you are using Autofac as at the time of coding this was the default IoC used by Umbraco. If you want to take the plunge with a different IoC Container then the code in this class should give you an idea where to start.

## Umbraco.Pylon.TestSupport ##
This class is available as a separate NuGet install and provides a suite of base classes and helpers to make Unit Testing in a Pylon based Umbraco environment as painless as possible. The TestSupport library uses and is dependent on Moq.

### Mock Objects ###
The library declares specific classes for Mocked Media and Mocked Content. Each item is set up with the default Id, Name and Url properties and inherits from `Mock<IPublishedContent>`. The MockContent class also includes BodyText. Both have a `Parent` property that can be set and is wired up appropriately. The MockContent class has the following method...
* **WithBasicProperties()** - Creates an instance with standard property values for quick testing.

In addition to the two key Mock class declarations, adding the `Umbraco.Pylon.TestSupport` namespace to your test file will enable the following extension methods to be added to `Mock<IPublishedContent>`...
* **SetupContentProperty()** - Adds a property to a piece of mocked content.
* **VerifyContentPropertiesCalled()** - Verifies that the specified properties were called a certain number of times.

### Concretes ###
The `Umbraco.Pylon.TestSupport.Concretes` namespace contains several classes which act as concrete interface implementations for stubbing out test dependencies including...
* **ConcreteDocumentType** - A stub for Umbraco.Pylon.DocumentType
* **ConcretePublishedContentProperty** - A stub for IPublishedProperty

### ContentBuilderTestBase ###
TestBase is a generic class which takes the type of the Object Under Test as the generic parameter. This feeds a protected property called `ObjUt` of this type. It also exposes a protected `TearDown()` method that resets all of it's internal values to null. It exposes the following protected shortcut properties...
* **MockContent** - Stores a MockContent instance.
* **StubContentAccessor** - Stores a stubbed content accessor. If one is not set then an empty accessor is returned.
* **StubMediaAccessor** - Stores a stubbed media accessor. If one is not set then an empty accessor is returned.
* **MockContentAccessor** - Stores a mock content accessor. If one is not set then an empty mock accessor is returned.
* **MockMediaAccessor** - Stores a mock media accessor. If one is not set then an empty mock accessor is returned.

### ContentServiceTestBase ###
This class inherits from TestBase and provides additional items to shortcut testing content services that back up controllers...
* **MockRepo** - Stores a mocked implementation of the Content Repository. If one is not set then a default mock is returned for the interface.

## The Ministry of Technology Open Source Products ##
Welcome to The Ministry of Technology open source products. All open source Ministry of Technology products are distributed under the MIT License for maximum re-usability. Details on more of our products and services can be found on our website at http://www.ministryotech.co.uk

Our other open source repositories can be found here...
* [http://www.ministryotech.co.uk/developers/open-source-projects](http://www.ministryotech.co.uk/developers/open-source-projects)
* [https://github.com/ministryotech](https://github.com/ministryotech)
* [https://bitbucket.org/ministryotech](https://bitbucket.org/ministryotech)

Most of our content is stored on both Github and Bitbucket. Our Umbraco related repositories are on Github only.

### Where can I get it? ###
You can download the package for this project from NuGet...

* **Umbraco.Pylon** - [https://www.nuget.org/packages/Umbraco.Pylon](/https://www.nuget.org/packages/Umbraco.Pylon)
* **Umbraco.Pylon.TestSupport** - [https://www.nuget.org/packages/Umbraco.Pylon.TestSupport](https://www.nuget.org/packages/Umbraco.Pylon.TestSupport)

Remember to look for the version that matches your Umbraco install.

### Contribution guidelines ###
If you would like to contribute to the project, please contact me.

### Who do I talk to? ###
* Keith Jackson - keith@ministryotech.co.uk
