using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace Umbraco.PylonTools.Controllers
{
    /// <summary>
    /// Base class for a surface controller, to use for key CMS action bindings.
    /// </summary>
    /// <seealso cref="PylonSurfaceController{TUmbracoSite,TPublishedContentRepository}" />
    /// <seealso cref="SurfaceController" />
    public abstract class PylonSurfaceController : PylonSurfaceController<IUmbracoSite, IPublishedContentRepository>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonSurfaceController" /> class.
        /// </summary>
        /// <param name="umbracoSite">The umbraco site.</param>
        protected PylonSurfaceController(IUmbracoSite umbracoSite)
            : base(umbracoSite)
        { }

        #endregion
    }

    /// <summary>
    /// Base class for a surface controller, to use for key CMS action bindings.
    /// </summary>
    /// <typeparam name="TUmbracoSite">The type of the umbraco site.</typeparam>
    /// <typeparam name="TPublishedContentRepository">The type of the published content repository.</typeparam>
    /// <seealso cref="SurfaceController" />
    public abstract class PylonSurfaceController<TUmbracoSite, TPublishedContentRepository> : SurfaceController
        where TUmbracoSite : IUmbracoSite<TPublishedContentRepository>
        where TPublishedContentRepository : IPublishedContentRepository
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonSurfaceController" /> class.
        /// </summary>
        /// <param name="umbracoSite">The umbraco site.</param>
        protected PylonSurfaceController(TUmbracoSite umbracoSite)
        {
            UmbracoSite = umbracoSite;
        }

        #endregion

        /// <summary>
        /// Gets the site repository.
        /// </summary>
        protected TUmbracoSite UmbracoSite { get; }

        /// <summary>
        /// Blocks returning an UmbracoHelper object
        /// </summary>
        /// <exception cref="PylonException">The Umbraco Helper should only be accessed via the implementation of IPublishedContentRepository.</exception>
        protected new UmbracoHelper Umbraco => throw new PylonException(
            "The Umbraco Helper should only be accessed via the implementation of IPublishedContentRepository.");

        /// <summary>
        /// Blocks returning the current UmbracoContext
        /// </summary>
        /// <exception cref="PylonException">The Umbraco Context should only be accessed via the implementation of IPublishedContentRepository.</exception>
        protected new UmbracoContext UmbracoContext => throw new PylonException(
            "The Umbraco Context should only be accessed via the implementation of IPublishedContentRepository.");
    }
}
