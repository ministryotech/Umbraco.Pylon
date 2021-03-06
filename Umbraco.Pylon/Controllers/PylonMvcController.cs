﻿using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace Umbraco.Pylon.Controllers
{
    /// <summary>
    /// Base class for an MVC controller, to use if you want to replace Umbraco's default behaviour for a content route or you need a route in the site with no content.
    /// </summary>
    /// <remarks>
    /// Consider, before using this, whether a Surface Controller is actually more appropriate.
    /// </remarks>
    /// <seealso cref="PylonMvcController{TUmbracoSite,TPublishedContentRepository}" />
    /// <seealso cref="PylonMvcController" />
    public abstract class PylonMvcController : PylonMvcController<IUmbracoSite, IPublishedContentRepository>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonMvcController" /> class.
        /// </summary>
        /// <param name="umbracoSite">The umbraco site.</param>
        protected PylonMvcController(IUmbracoSite umbracoSite)
            : base(umbracoSite)
        { }

        #endregion
    }

    /// <summary>
    /// Base class for an MVC controller, to use if you want to replace Umbraco's default behaviour for a content route or you need a route in the site with no content.
    /// </summary>
    /// <typeparam name="TUmbracoSite">The type of the umbraco site.</typeparam>
    /// <typeparam name="TPublishedContentRepository">The type of the published content repository.</typeparam>
    /// <remarks>
    /// Consider, before using this, whether a Surface Controller is actually more appropriate.
    /// </remarks>
    /// <seealso cref="RenderMvcController" />
    public abstract class PylonMvcController<TUmbracoSite, TPublishedContentRepository> : RenderMvcController
        where TUmbracoSite : IUmbracoSite<TPublishedContentRepository>
        where TPublishedContentRepository : IPublishedContentRepository
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonMvcController" /> class.
        /// </summary>
        /// <param name="umbracoSite">The umbraco site.</param>
        protected PylonMvcController(TUmbracoSite umbracoSite)
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
