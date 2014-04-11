using System;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace Umbraco.Pylon
{
    /// <summary>
    /// Base Controller class for a standard content repository.
    /// </summary>
    public abstract class UmbracoPylonControllerBase : UmbracoPylonControllerBase<IPublishedContentRepository>
    {
        private IPublishedContentRepository contentRepo;

        /// <summary>
        /// Gets or sets the content repository.
        /// </summary>
        public override IPublishedContentRepository ContentRepo
        {
            get
            {
                if (contentRepo != null)
                    return contentRepo;

                return UmbracoContext != null ? new PublishedContentRepository(UmbracoContext) : new PublishedContentRepository();
            }
            set { contentRepo = value; }
        }
    }

    /// <summary>
    /// Base Controller class for a specified content repository.
    /// </summary>
    public abstract class UmbracoPylonControllerBase<TPublishedContentRepository> : RenderMvcController
        where TPublishedContentRepository : IPublishedContentRepository
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="UmbracoPylonControllerBase{TPublishedContentRepository}"/> class.
        /// </summary>
        protected UmbracoPylonControllerBase()
        {
            EnableFileCheck = true;
        }

        #endregion

        /// <summary>
        /// Gets or sets a value indicating whether file checking is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if file checking is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool EnableFileCheck { get; set; }

        /// <summary>
        /// Gets the umbraco context.
        /// </summary>
        protected new UmbracoContext UmbracoContext 
        {
            get
            {
                try
                {
                    return base.UmbracoContext;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the content repository.
        /// </summary>
        public abstract TPublishedContentRepository ContentRepo { get; set; }

        /// <summary>
        /// Checks to make sure the physical view file exists on disk
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        protected new bool EnsurePhsyicalViewExists(string template)
        {
            return !EnableFileCheck || base.EnsurePhsyicalViewExists(template);
        }

        /// <summary>
        /// Returns an ActionResult based on the template name found in the route values and the given model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <remarks>
        /// If the template found in the route values doesn't physically exist, then an empty ContentResult will be returned.
        /// </remarks>
        protected new ActionResult CurrentTemplate<TModel>(TModel model)
        {
            var template = ControllerContext.RouteData.Values["action"].ToString();
            if (!EnsurePhsyicalViewExists(template))
            {
                return Content("");
            }
            return View(template, model);
        }
    }
}