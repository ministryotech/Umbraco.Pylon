using EmptyUmbracoWebApp.Repositories;
using Umbraco.Pylon.Controllers;

namespace EmptyUmbracoWebApp.Controllers
{
    /// <summary>
    /// Base class for an API controller, to use when a traditional API output / input is required.
    /// </summary>
    /// <remarks>
    /// API controllers should use traditional REST approaches using JSON for input and output and generating standard HTTP responses.
    /// If you need to return content then use a Surface Controller.
    /// </remarks>
    /// <seealso cref="Umbraco.Pylon.Controllers.PylonApiController{ISampleSite, ISamplePublishedContentRepository}" />
    public abstract class SampleApiController : PylonApiController<ISampleSite, ISamplePublishedContentRepository>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleApiController" /> class.
        /// </summary>
        /// <param name="site">The site.</param>
        protected SampleApiController(ISampleSite site)
            : base(site)
        { }

        #endregion
    }
}
