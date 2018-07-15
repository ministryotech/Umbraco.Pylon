using EmptyUmbracoWebApp.Repositories;
using Umbraco.Pylon.Controllers;

namespace EmptyUmbracoWebApp.Controllers
{
    /// <summary>
    /// Base class for an MVC controller, to use if you want to replace Umbraco's default behaviour for a content route or you need a route in the site with no content.
    /// </summary>
    /// <remarks>
    /// Consider, before using this, whether a Surface Controller is actually more appropriate.
    /// </remarks>
    /// <seealso cref="Umbraco.Pylon.Controllers.PylonMvcController{ISampleSite, ISamplePublishedContentRepository}" />
    public abstract class SampleMvcController : PylonMvcController<ISampleSite, ISamplePublishedContentRepository>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleMvcController" /> class.
        /// </summary>
        /// <param name="site">The site.</param>
        protected SampleMvcController(ISampleSite site)
            : base(site)
        { }

        #endregion
    }
}
