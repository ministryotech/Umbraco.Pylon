// Copyright (c) 2014 Minotech Ltd.
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

using Umbraco.Web;
using Umbraco.Web.WebApi;

namespace Umbraco.PylonLite
{
    /// <summary>
    /// Base Controller class for a custom umbraco controller.
    /// </summary>
    /// <remarks>
    /// The main controller and the inner controller should share the same interfaces with methods passed through. This enables testing the inner controllers without pain.
    /// </remarks>
    public abstract class PylonLayeredApiController : PylonLayeredApiController<IPublishedContentRepository, PylonInnerApiController>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonLayeredApiController{TPublishedContentRepository, TInnerController}"/> class.
        /// </summary>
        /// <param name="innerController">The inner controller.</param>
        protected PylonLayeredApiController(PylonInnerApiController innerController)
            : base(innerController)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonLayeredApiController{TPublishedContentRepository, TInnerController}" /> class.
        /// </summary>
        /// <param name="innerController">The inner controller.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        protected PylonLayeredApiController(PylonInnerApiController innerController, UmbracoContext umbracoContext)
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
    public abstract class PylonLayeredApiController<TPublishedContentRepository, TInnerController> : UmbracoApiController, IPylonApiController<TPublishedContentRepository>
        where TPublishedContentRepository : IPublishedContentRepository
        where TInnerController : PylonInnerApiController<TPublishedContentRepository>, IPylonApiController<TPublishedContentRepository>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonLayeredController{TPublishedContentRepository, TInnerController}"/> class.
        /// </summary>
        /// <param name="innerController">The inner controller.</param>
        protected PylonLayeredApiController(TInnerController innerController)
        {
            InnerController = innerController;
            InnerController.SetInnerRequest(Request);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonLayeredController{TPublishedContentRepository, TInnerController}" /> class.
        /// </summary>
        /// <param name="innerController">The inner controller.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        protected PylonLayeredApiController(TInnerController innerController, UmbracoContext umbracoContext)
            : base(umbracoContext)
        {
            InnerController = innerController;
            InnerController.UmbracoContext = umbracoContext;
            InnerController.SetInnerRequest(Request);
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
        /// Gets or sets the content repository.
        /// </summary>
        public TPublishedContentRepository ContentRepo
        {
            get { return InnerController.ContentRepo; }
            set { InnerController.ContentRepo = value; }
        }
    }
}