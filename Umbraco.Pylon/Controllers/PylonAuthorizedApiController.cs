using Umbraco.Web.WebApi;

namespace Umbraco.Pylon.Controllers
{
    /// <summary>
    /// Base class for an API controller, to use when a traditional API output / input is required.
    /// </summary>
    /// <remarks>
    /// Authorized API controllers should use traditional REST approaches using JSON for input and output and generating standard HTTP responses.
    /// These require back office login and should be used for back office operations.
    /// </remarks>
    /// <seealso cref="PylonAuthorizedApiController{TUmbracoSite,TPublishedContentRepository}" />
    /// <seealso cref="UmbracoApiController" />
    public abstract class PylonAuthorizedApiController : PylonAuthorizedApiController<IUmbracoSite, IPublishedContentRepository>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonAuthorizedApiController" /> class.
        /// </summary>
        /// <param name="umbracoSite">The umbraco site.</param>
        protected PylonAuthorizedApiController(IUmbracoSite umbracoSite)
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
    /// Authorized API controllers should use traditional REST approaches using JSON for input and output and generating standard HTTP responses.
    /// These require back office login and should be used for back office operations.
    /// </remarks>
    /// <seealso cref="UmbracoAuthorizedApiController" />
    public abstract class PylonAuthorizedApiController<TUmbracoSite, TPublishedContentRepository> : UmbracoAuthorizedApiController
        where TUmbracoSite : IUmbracoSite<TPublishedContentRepository>
        where TPublishedContentRepository : IPublishedContentRepository
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonAuthorizedApiController" /> class.
        /// </summary>
        /// <param name="umbracoSite">The umbraco site.</param>
        protected PylonAuthorizedApiController(TUmbracoSite umbracoSite)
        {
            UmbracoSite = umbracoSite;
        }

        #endregion

        /// <summary>
        /// Gets the site.
        /// </summary>
        protected TUmbracoSite UmbracoSite { get; }
    }
}
