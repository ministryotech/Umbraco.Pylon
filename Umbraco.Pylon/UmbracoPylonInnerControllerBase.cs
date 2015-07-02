// Copyright (c) 2015 Minotech Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files
// (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, 
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do 
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Web;

namespace Umbraco.Pylon
{
    /// <summary>
    /// Base class for a controller that sits inside a custom umbraco controller.
    /// </summary>
    /// <remarks>
    /// The main controller and the inner controller should share the same interfaces with methods passed through. This enables testing the inner controllers without pain.
    /// </remarks>
    public abstract class UmbracoPylonInnerControllerBase : UmbracoPylonInnerControllerBase<IPublishedContentRepository> 
    {
        private IPublishedContentRepository contentRepo;

        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="UmbracoPylonInnerControllerBase{TPublishedContentRepository}" /> class.
        /// </summary>
        /// <param name="contentAccessor">The content accessor.</param>
        /// <param name="mediaAccessor">The media accessor.</param>
        protected UmbracoPylonInnerControllerBase(IContentAccessor contentAccessor, IMediaAccessor mediaAccessor)
            : base(contentAccessor, mediaAccessor)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UmbracoPylonInnerControllerBase{TPublishedContentRepository}" /> class.
        /// </summary>
        /// <param name="contentAccessor">The content accessor.</param>
        /// <param name="mediaAccessor">The media accessor.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        protected UmbracoPylonInnerControllerBase(IContentAccessor contentAccessor, IMediaAccessor mediaAccessor, UmbracoContext umbracoContext)
            : base(contentAccessor, mediaAccessor)
        {
            UmbracoContext = umbracoContext;
        }

        #endregion

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
    /// Base class for a controller that sits inside a custom umbraco controller.
    /// </summary>
    /// <remarks>
    /// The main controller and the inner controller should share the same interfaces with methods passed through. This enables testing the inner controllers without pain.
    /// </remarks>
    public abstract class UmbracoPylonInnerControllerBase<TPublishedContentRepository> : Controller, IUmbracoPylonController<TPublishedContentRepository> 
        where TPublishedContentRepository : IPublishedContentRepository
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="UmbracoPylonInnerControllerBase{TPublishedContentRepository}" /> class.
        /// </summary>
        /// <param name="contentAccessor">The content accessor.</param>
        /// <param name="mediaAccessor">The media accessor.</param>
        protected UmbracoPylonInnerControllerBase(IContentAccessor contentAccessor, IMediaAccessor mediaAccessor)
        {
            GetContent = contentAccessor;
            GetMedia = mediaAccessor;
            ViewStringRenderer = new ViewStringRenderer();

            EnableFileCheck = true;
        }

        #endregion

        /// <summary>
        /// Gets Typed content.
        /// </summary>
        protected IContentAccessor GetContent { get; private set; }

        /// <summary>
        /// Gets Media.
        /// </summary>
        protected IMediaAccessor GetMedia { get; private set; }

        /// <summary>
        /// Gets or sets the view string renderer.
        /// </summary>
        /// <value>
        /// The view string renderer.
        /// </value>
        public IViewStringRenderer ViewStringRenderer { get; set; }

        /// <summary>
        /// Gets or Sets the umbraco context.
        /// </summary>
        /// <remarks>
        /// Avoid using this wherever possible for your own sanity.
        /// </remarks>
        public UmbracoContext UmbracoContext { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether file checking is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if file checking is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool EnableFileCheck { get; set; }

        /// <summary>
        /// Gets or sets the content repository.
        /// </summary>
        public abstract TPublishedContentRepository ContentRepo { get; set; }

        /// <summary>
        /// Checks to make sure the physical view file exists on disk
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public bool EnsurePhsyicalViewExists(string template)
        {
            if (!EnableFileCheck)
                return true;

            var result = ViewEngines.Engines.FindView(ControllerContext, template, null);
            return result.View != null;
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
        public ActionResult CurrentTemplate<TModel>(TModel model)
        {
            var template = ControllerContext.RouteData.Values["action"].ToString();
            if (!EnsurePhsyicalViewExists(template))
            {
                return Content("");
            }
            return View(template, model);
        }

        /// <summary>
        /// Renders a partial as a string.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        protected internal string PartialAsString(string viewName, object model)
        {
            SetDefaultContext();
            return ViewStringRenderer.Render(viewName, model, this);
        }



        /// <summary>
        /// Sets the ControllerContext to a defaults state when no existing ControllerContext is present       
        /// </summary>
        public ControllerContext SetDefaultContext()
        {
            if (ControllerContext != null)
                return ControllerContext;

            HttpContextBase wrapper = null;
            if (System.Web.HttpContext.Current != null)
                wrapper = new HttpContextWrapper(System.Web.HttpContext.Current);

            var routeData = new RouteData();

            if (!routeData.Values.ContainsKey("controller") && !routeData.Values.ContainsKey("Controller"))
                routeData.Values.Add("controller", GetType().Name
                                                            .ToLower()
                                                            .Replace("controller", ""));

            ControllerContext = new ControllerContext(wrapper, routeData, this);
            return ControllerContext;
        }
    }
}