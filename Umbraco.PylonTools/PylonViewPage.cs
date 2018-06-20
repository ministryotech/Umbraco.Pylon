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
    public abstract class PylonViewPage : PylonViewPage<DefaultUmbracoSite, PublishedContentRepository, IPublishedContent>
    { }

    /// <summary>
    /// A Pylon based view page using a custom model and a generic site repository.
    /// </summary>
    /// <seealso cref="UmbracoViewPage" />
    /// <remarks>
    /// Use this over <see cref="UmbracoViewPage" /> to access your IPublishedContentRepository implementation.
    /// </remarks>
    public abstract class PylonViewPage<TModel> : PylonViewPage<DefaultUmbracoSite, PublishedContentRepository, TModel>
    { }

    /// <summary>
    /// A Pylon based view page using a custom model and a site specific content repository.
    /// </summary>
    /// <typeparam name="TUmbracoSite">The type of the umbraco site.</typeparam>
    /// <typeparam name="TSiteContentRepository">The type of the site content repository.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="UmbracoViewPage" />
    /// <remarks>
    /// Use this over <see cref="UmbracoViewPage" /> to access your IPublishedContentRepository implementation.
    /// Of the three variants available, this is the preferred usage.
    /// </remarks>
    public abstract class PylonViewPage<TUmbracoSite, TSiteContentRepository, TModel> : UmbracoViewPage<TModel>
        where TUmbracoSite : class, IUmbracoSite<TSiteContentRepository>, new()
        where TSiteContentRepository : class, IPublishedContentRepository, new()
    {
        private TUmbracoSite umbracoSite;

        /// <summary>
        /// Entry point for specified site content.
        /// </summary>
        /// <value>
        /// The umbraco site.
        /// </value>
        /// <remarks>
        /// If the content repository has not yet been initialised then it will be done here.
        /// </remarks>
        public TUmbracoSite UmbracoSite
            => umbracoSite ?? (umbracoSite = new TUmbracoSite {
                Content = new TSiteContentRepository { Umbraco = Umbraco }
            });
    }
}
