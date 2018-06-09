using Umbraco.Web.Mvc;

namespace Umbraco.PylonTools.Controllers
{
    /// <summary>
    /// Base class for a surface controller, to use for key CMS action bindings.
    /// </summary>
    /// <seealso cref="PylonSurfaceController{TSiteContentRepository}" />
    /// <seealso cref="SurfaceController" />
    public abstract class PylonSurfaceController : PylonSurfaceController<IPublishedContentRepository>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonSurfaceController" /> class.
        /// </summary>
        /// <param name="contentRepository">The content repository.</param>
        protected PylonSurfaceController(IPublishedContentRepository contentRepository)
            : base(contentRepository)
        { }

        #endregion
    }

    /// <summary>
    /// Base class for a surface controller, to use for key CMS action bindings.
    /// </summary>
    /// <typeparam name="TSiteContentRepository">The type of the site content repository.</typeparam>
    /// <seealso cref="PylonSurfaceController{TSiteContentRepository}" />
    /// <seealso cref="SurfaceController" />
    public abstract class PylonSurfaceController<TSiteContentRepository> : SurfaceController
        where TSiteContentRepository : IPublishedContentRepository
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonSurfaceController" /> class.
        /// </summary>
        /// <param name="contentRepository">The content repository.</param>
        protected PylonSurfaceController(TSiteContentRepository contentRepository)
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
