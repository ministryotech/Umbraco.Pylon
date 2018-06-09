using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace Umbraco.PylonTools
{
    /// <summary>
    /// A Pylon based view page.
    /// </summary>
    /// <seealso cref="UmbracoViewPage" />
    /// <remarks>
    /// Use this over <see cref="UmbracoViewPage" /> to access your IPublishedContentRepository implementation.
    /// </remarks>
    public abstract class PylonViewPage : PylonViewPage<IPublishedContent, IPublishedContentRepository>
    { }

    /// <summary>
    /// A Pylon based view page.
    /// </summary>
    /// <seealso cref="UmbracoViewPage" />
    /// <remarks>
    /// Use this over <see cref="UmbracoViewPage" /> to access your IPublishedContentRepository implementation.
    /// </remarks>
    public abstract class PylonViewPage<TModel> : PylonViewPage<TModel, IPublishedContentRepository>
    { }

    /// <summary>
    /// A Pylon based view page.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TSiteContentRepository">The type of the site content repository.</typeparam>
    /// <seealso cref="UmbracoViewPage" />
    /// <remarks>
    /// Use this over <see cref="UmbracoViewPage" /> to access your IPublishedContentRepository implementation.
    /// </remarks>
    public abstract class PylonViewPage<TModel, TSiteContentRepository> : UmbracoViewPage<TModel>
        where TSiteContentRepository : IPublishedContentRepository
    {

    }
}
