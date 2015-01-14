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

        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="UmbracoPylonControllerBase{TPublishedContentRepository}"/> class.
        /// </summary>
        protected UmbracoPylonControllerBase()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UmbracoPylonControllerBase{TPublishedContentRepository}" /> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        protected UmbracoPylonControllerBase(UmbracoContext umbracoContext)
            : base(umbracoContext)
        { }

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

        /// <summary>
        /// Initializes a new instance of the <see cref="UmbracoPylonControllerBase{TPublishedContentRepository}" /> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        protected UmbracoPylonControllerBase(UmbracoContext umbracoContext)
            : base(umbracoContext)
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