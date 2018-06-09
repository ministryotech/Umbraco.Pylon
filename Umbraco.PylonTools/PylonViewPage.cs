using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace Umbraco.PylonTools
{
    /// <summary>
    /// A Pylon based view page using a standard IPublishedContent model and a generic site repository.
    /// </summary>
    /// <seealso cref="UmbracoViewPage" />
    /// <remarks>
    /// Use this over <see cref="UmbracoViewPage" /> to access your IPublishedContentRepository implementation.
    /// </remarks>
    public abstract class PylonViewPage : PylonViewPage<PublishedContentRepository, IPublishedContent>
    { }

    /// <summary>
    /// A Pylon based view page using a custom model and a generic site repository.
    /// </summary>
    /// <seealso cref="UmbracoViewPage" />
    /// <remarks>
    /// Use this over <see cref="UmbracoViewPage" /> to access your IPublishedContentRepository implementation.
    /// </remarks>
    public abstract class PylonViewPage<TModel> : PylonViewPage<PublishedContentRepository, TModel>
    { }

    /// <summary>
    /// A Pylon based view page using a custom model and a site specific content repository.
    /// </summary>
    /// <typeparam name="TSiteContentRepository">The type of the site content repository.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="UmbracoViewPage" />
    /// <remarks>
    /// Use this over <see cref="UmbracoViewPage" /> to access your IPublishedContentRepository implementation.
    /// Of the three variants available, this is the preferred usage.
    /// </remarks>
    public abstract class PylonViewPage<TSiteContentRepository, TModel> : UmbracoViewPage<TModel>
        where TSiteContentRepository : IPublishedContentRepository, new()
    {
        private TSiteContentRepository umbracoSite;

        /// <summary>
        /// Entry point for specified site content.
        /// </summary>
        /// <value>
        /// The umbraco site.
        /// </value>
        /// <remarks>
        /// If the content repository has not yet been initialised then it will be done here.
        /// </remarks>
        public TSiteContentRepository UmbracoSite
            => umbracoSite != null ? umbracoSite : umbracoSite = new TSiteContentRepository { Umbraco = Umbraco };
    }
}
