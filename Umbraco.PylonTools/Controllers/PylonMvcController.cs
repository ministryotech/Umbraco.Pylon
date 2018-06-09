using Umbraco.Web.Mvc;

namespace Umbraco.PylonTools.Controllers
{
    /// <summary>
    /// Base class for an MVC controller, to use if you want to replace Umbraco's default behaviour for a content route or you need a route in the site with no content.
    /// </summary>
    /// <seealso cref="PylonMvcController{TSiteContentRepository}" />
    /// <remarks>
    /// Consider, before using this, whether a Surface Controller is actually more appropriate.
    /// </remarks>
    /// <seealso cref="PylonMvcController" />
    public abstract class PylonMvcController : PylonMvcController<IPublishedContentRepository>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonMvcController" /> class.
        /// </summary>
        /// <param name="contentRepository">The content repository.</param>
        protected PylonMvcController(IPublishedContentRepository contentRepository)
            : base(contentRepository)
        { }

        #endregion
    }

    /// <summary>
    /// Base class for an MVC controller, to use if you want to replace Umbraco's default behaviour for a content route or you need a route in the site with no content.
    /// </summary>
    /// <typeparam name="TSiteContentRepository">The type of the site content repository.</typeparam>
    /// <seealso cref="RenderMvcController" />
    /// <remarks>
    /// Consider, before using this, whether a Surface Controller is actually more appropriate.
    /// </remarks>
    /// <seealso cref="PylonMvcController" />
    public abstract class PylonMvcController<TSiteContentRepository> : RenderMvcController
        where TSiteContentRepository : IPublishedContentRepository
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonMvcController" /> class.
        /// </summary>
        /// <param name="contentRepository">The content repository.</param>
        protected PylonMvcController(TSiteContentRepository contentRepository)
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
