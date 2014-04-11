using Umbraco.Web.Mvc;

namespace Umbraco.Pylon
{
    /// <summary>
    /// An abstract base class for Umbraco views.
    /// </summary>
    public abstract class UmbracoPylonViewPage : UmbracoPylonViewPage<UmbracoSite, PublishedContentRepository>
    { }

    /// <summary>
    /// An abstract base class for Umbraco views using strongly typed models.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public abstract class UmbracoPylonViewPage<TModel> : UmbracoPylonViewPage<UmbracoSite, PublishedContentRepository, TModel>
    { }

    /// <summary>
    /// An abstract base class for Umbraco views with a specific content repository.
    /// </summary>
    /// <typeparam name="TUmbracoSite">The type of the umbraco site.</typeparam>
    /// <typeparam name="TPublishedContentRepository">The type of the published content repository.</typeparam>
    public abstract class UmbracoPylonViewPage<TUmbracoSite, TPublishedContentRepository> : UmbracoTemplatePage
        where TUmbracoSite : UmbracoSite<TPublishedContentRepository>, new()
        where TPublishedContentRepository : PublishedContentRepository, new()
    {
        private TUmbracoSite umbracoSite;

        /// <summary>
        /// Gets the model in a dynamic form.
        /// </summary>
        public dynamic DynamicModel { get { return CurrentPage; } }

        /// <summary>
        /// Entry point for helper functions defined at root site level.
        /// </summary>
        /// <value>
        /// The ministryweb helper object.
        /// </value>
        public TUmbracoSite UmbracoSite
        {
            get
            {
                umbracoSite = umbracoSite ?? new TUmbracoSite { Umbraco = Umbraco };
                return umbracoSite;
            }
        }
    }

    /// <summary>
    /// An abstract base class for Umbraco views with a specific content repository using strongly typed models.
    /// </summary>
    /// <typeparam name="TUmbracoSite">The type of the umbraco site.</typeparam>
    /// <typeparam name="TPublishedContentRepository">The type of the published content repository.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public abstract class UmbracoPylonViewPage<TUmbracoSite, TPublishedContentRepository, TModel> : UmbracoViewPage<TModel>
        where TUmbracoSite : UmbracoSite<TPublishedContentRepository>, new()
        where TPublishedContentRepository : PublishedContentRepository, new()
    {
        private TUmbracoSite umbracoSite;

        /// <summary>
        /// Entry point for helper functions defined at root site level.
        /// </summary>
        /// <value>
        /// The ministryweb helper object.
        /// </value>
        public TUmbracoSite UmbracoSite
        {
            get
            {
                umbracoSite = umbracoSite ?? new TUmbracoSite { Umbraco = Umbraco };
                return umbracoSite;
            }
        }
    }
}