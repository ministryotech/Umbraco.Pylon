using Umbraco.Web.WebApi;

namespace Umbraco.PylonTools.Controllers
{
    /// <summary>
    /// Base class for an API controller, to use when a traditional API output / input is required.
    /// </summary>
    /// <seealso cref="PylonApiController{TSiteContentRepository}" />
    /// <remarks>
    /// API controllers should use traditional REST approaches using JSON for input and output and generating standard HTTP responses.
    /// If you need to return content then use a Surface Controller.
    /// </remarks>
    /// <seealso cref="UmbracoApiController" />
    public abstract class PylonApiController : PylonApiController<IPublishedContentRepository>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonApiController" /> class.
        /// </summary>
        /// <param name="contentRepository">The content repository.</param>
        protected PylonApiController(IPublishedContentRepository contentRepository)
            : base(contentRepository)
        { }

        #endregion
    }

    /// <summary>
    /// Base class for an API controller, to use when a traditional API output / input is required.
    /// </summary>
    /// <typeparam name="TSiteContentRepository">The type of the site content repository.</typeparam>
    /// <seealso cref="PylonApiController{TSiteContentRepository}" />
    /// <remarks>
    /// API controllers should use traditional REST approaches using JSON for input and output and generating standard HTTP responses.
    /// If you need to return content then use a Surface Controller.
    /// </remarks>
    /// <seealso cref="UmbracoApiController" />
    public abstract class PylonApiController<TSiteContentRepository> : UmbracoApiController
        where TSiteContentRepository : IPublishedContentRepository
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonApiController" /> class.
        /// </summary>
        /// <param name="contentRepository">The content repository.</param>
        protected PylonApiController(TSiteContentRepository contentRepository)
        {
            Site = contentRepository;
        }

        #endregion

        /// <summary>
        /// Gets the site repository.
        /// </summary>
        protected TSiteContentRepository Site { get; }
    }
}
