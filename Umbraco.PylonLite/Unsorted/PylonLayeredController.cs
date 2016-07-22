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

using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace Umbraco.PylonLite
{
    /// <summary>
    /// Base Controller class for a custom umbraco controller.
    /// </summary>
    /// <remarks>
    /// The main controller and the inner controller should share the same interfaces with methods passed through. This enables testing the inner controllers without pain.
    /// </remarks>
    public abstract class PylonLayeredController : PylonLayeredController<IPublishedContentRepository, PylonInnerController>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonLayeredController{TPublishedContentRepository, TInnerController}"/> class.
        /// </summary>
        /// <param name="innerController">The inner controller.</param>
        protected PylonLayeredController(PylonInnerController innerController)
            : base(innerController)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonLayeredController{TPublishedContentRepository, TInnerController}" /> class.
        /// </summary>
        /// <param name="innerController">The inner controller.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        protected PylonLayeredController(PylonInnerController innerController, UmbracoContext umbracoContext)
            : base(innerController, umbracoContext)
        { }

        #endregion
    }

    /// <summary>
    /// Base Controller class for a custom umbraco controller.
    /// </summary>
    /// <remarks>
    /// The main controller and the inner controller should share the same interfaces with methods passed through. This enables testing the inner controllers without pain.
    /// </remarks>
    public abstract class PylonLayeredController<TPublishedContentRepository, TInnerController> : RenderMvcController, IPylonController<TPublishedContentRepository>
        where TPublishedContentRepository : IPublishedContentRepository
        where TInnerController : PylonInnerController<TPublishedContentRepository>, IPylonController<TPublishedContentRepository>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonLayeredController{TPublishedContentRepository, TInnerController}"/> class.
        /// </summary>
        /// <param name="innerController">The inner controller.</param>
        protected PylonLayeredController(TInnerController innerController)
        {
            InnerController = innerController;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonLayeredController{TPublishedContentRepository, TInnerController}" /> class.
        /// </summary>
        /// <param name="innerController">The inner controller.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        protected PylonLayeredController(TInnerController innerController, UmbracoContext umbracoContext)
            : base(umbracoContext)
        {
            InnerController = innerController;
            InnerController.UmbracoContext = umbracoContext;
        }

        /// <summary>
        /// Initializes data that might not be available when the constructor is called.
        /// </summary>
        /// <param name="requestContext">The HTTP context and route data.</param>
        /// <remarks>
        /// This override allows passing of the context to the inner controller after it's been initialised.
        /// </remarks>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            InnerController.ControllerContext = ControllerContext;
        }

        #endregion

        /// <summary>
        /// Gets the inner controller.
        /// </summary>
        /// <value>
        /// The inner controller.
        /// </value>
        protected TInnerController InnerController { get; private set; }

        /// <summary>
        /// Gets the umbraco context.
        /// </summary>
        public new UmbracoContext UmbracoContext
        {
            get { return InnerController.UmbracoContext; }
            set { InnerController.UmbracoContext = value; }
        }

        /// <summary>
        /// Gets or sets the view string renderer.
        /// </summary>
        public IViewStringRenderer ViewStringRenderer
        {
            get { return InnerController.ViewStringRenderer; }
            set { InnerController.ViewStringRenderer = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether file checking is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if file checking is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool EnableFileCheck
        {
            get { return InnerController.EnableFileCheck; }
            set { InnerController.EnableFileCheck = value; }
        }

        /// <summary>
        /// Gets or sets the content repository.
        /// </summary>
        public TPublishedContentRepository ContentRepo
        {
            get { return InnerController.ContentRepo; }
            set { InnerController.ContentRepo = value; }
        }

        /// <summary>
        /// Checks to make sure the physical view file exists on disk
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public new bool EnsurePhsyicalViewExists(string template)
        {
            return InnerController.EnsurePhsyicalViewExists(template);
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
        public new ActionResult CurrentTemplate<TModel>(TModel model)
        {
            return InnerController.CurrentTemplate(model);
        }

        /// <summary>
        /// Renders a partial as a string.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        protected string PartialAsString(string viewName, object model)
        {
            return InnerController.PartialAsString(viewName, model);
        }

        /// <summary>
        /// Sets the ControllerContext to a defaults state when no existing ControllerContext is present       
        /// </summary>
        public ControllerContext SetDefaultContext()
        {
            ControllerContext = InnerController.SetDefaultContext();
            return ControllerContext;
        }
    }
}