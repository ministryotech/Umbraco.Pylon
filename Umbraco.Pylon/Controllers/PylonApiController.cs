using Umbraco.Web;
using Umbraco.Web.WebApi;

namespace Umbraco.Pylon.Controllers
{
    /// <summary>
    /// Base class for an API controller, to use when a traditional API output / input is required.
    /// </summary>
    /// <remarks>
    /// API controllers should use traditional REST approaches using JSON for input and output and generating standard HTTP responses.
    /// If you need to return content then use a Surface Controller.
    /// </remarks>
    /// <seealso cref="PylonApiController{TUmbracoSite,TPublishedContentRepository}" />
    /// <seealso cref="UmbracoApiController" />
    public abstract class PylonApiController : PylonApiController<IUmbracoSite, IPublishedContentRepository>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonApiController" /> class.
        /// </summary>
        /// <param name="umbracoSite">The umbraco site.</param>
        protected PylonApiController(IUmbracoSite umbracoSite)
            : base(umbracoSite)
        { }

        #endregion
    }

    /// <summary>
    /// Base class for an API controller, to use when a traditional API output / input is required.
    /// </summary>
    /// <typeparam name="TUmbracoSite">The type of the umbraco site.</typeparam>
    /// <typeparam name="TPublishedContentRepository">The type of the published content repository.</typeparam>
    /// <remarks>
    /// API controllers should use traditional REST approaches using JSON for input and output and generating standard HTTP responses.
    /// If you need to return content then use a Surface Controller.
    /// </remarks>
    /// <seealso cref="UmbracoApiController" />
    public abstract class PylonApiController<TUmbracoSite, TPublishedContentRepository> : UmbracoApiController
        where TUmbracoSite : IUmbracoSite<TPublishedContentRepository>
        where TPublishedContentRepository : IPublishedContentRepository
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonApiController" /> class.
        /// </summary>
        /// <param name="umbracoSite">The umbraco site.</param>
        protected PylonApiController(TUmbracoSite umbracoSite)
        {
            UmbracoSite = umbracoSite;
        }

        #endregion

        /// <summary>
        /// Gets the site.
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
