using EmptyUmbracoWebApp.Repositories;
using Umbraco.Pylon.Controllers;

namespace EmptyUmbracoWebApp.Controllers
{
    /// <summary>
    /// Base class for a surface controller, to use for key CMS action bindings.
    /// </summary>
    /// <seealso cref="Umbraco.Pylon.Controllers.PylonSurfaceController{ISampleSite, ISamplePublishedContentRepository}" />
    public abstract class SampleSurfaceController : PylonSurfaceController<ISampleSite, ISamplePublishedContentRepository>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleSurfaceController" /> class.
        /// </summary>
        /// <param name="site">The site.</param>
        protected SampleSurfaceController(ISampleSite site)
            : base(site)
        { }

        #endregion
    }
}
