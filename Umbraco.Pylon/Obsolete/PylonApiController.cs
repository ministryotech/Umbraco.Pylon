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

using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using Umbraco.Web;
using Umbraco.Web.WebApi;

namespace Umbraco.Pylon
{
    /// <summary>
    /// Base Controller class for a custom umbraco controller.
    /// </summary>
    [Obsolete("Pylon controllers are no longer supported - Use a thin controller and a testable service passed in through DI as a recommended pattern")]
    public abstract class PylonApiController : PylonApiController<IPublishedContentRepository>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonApiController" /> class.
        /// </summary>
        /// <param name="contentAccessor">The content accessor.</param>
        /// <param name="mediaAccessor">The media accessor.</param>
        protected PylonApiController(IContentAccessor contentAccessor, IMediaAccessor mediaAccessor)
            : base(new PublishedContentRepository(), contentAccessor, mediaAccessor)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonApiController" /> class.
        /// </summary>
        /// <param name="contentAccessor">The content accessor.</param>
        /// <param name="mediaAccessor">The media accessor.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        protected PylonApiController(IContentAccessor contentAccessor, IMediaAccessor mediaAccessor, UmbracoContext umbracoContext)
            : base(new PublishedContentRepository(umbracoContext), contentAccessor, mediaAccessor)
        { }

        #endregion
    }

    /// <summary>
    /// Base Controller class for a custom umbraco controller.
    /// </summary>
    /// <remarks>
    /// The main controller and the inner controller should share the same interfaces with methods passed through. This enables testing the inner controllers without pain.
    /// </remarks>
    [Obsolete("Pylon controllers are no longer supported - Use a thin controller and a testable service passed in through DI as a recommended pattern")]
    public abstract class PylonApiController<TPublishedContentRepository> : UmbracoApiController, IPylonApiController<TPublishedContentRepository>
        where TPublishedContentRepository : IPublishedContentRepository
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonApiController{TPublishedContentRepository}" /> class.
        /// </summary>
        /// <param name="contentRepo">The content repo.</param>
        /// <param name="contentAccessor">The content accessor.</param>
        /// <param name="mediaAccessor">The media accessor.</param>
        protected PylonApiController(TPublishedContentRepository contentRepo, IContentAccessor contentAccessor, IMediaAccessor mediaAccessor)
        {
            InitController(contentRepo, contentAccessor, mediaAccessor);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonApiController{TPublishedContentRepository}" /> class.
        /// </summary>
        /// <param name="contentRepo">The content repo.</param>
        /// <param name="contentAccessor">The content accessor.</param>
        /// <param name="mediaAccessor">The media accessor.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        protected PylonApiController(TPublishedContentRepository contentRepo, IContentAccessor contentAccessor,
            IMediaAccessor mediaAccessor, UmbracoContext umbracoContext)
            : base(umbracoContext)
        {
            InitController(contentRepo, contentAccessor, mediaAccessor);
        }

        /// <summary>
        /// Initializes the controller.
        /// </summary>
        /// <param name="contentRepo">The content repo.</param>
        /// <param name="contentAccessor">The content accessor.</param>
        /// <param name="mediaAccessor">The media accessor.</param>
        private void InitController(TPublishedContentRepository contentRepo, IContentAccessor contentAccessor, IMediaAccessor mediaAccessor)
        {
            GetContent = contentAccessor;
            GetMedia = mediaAccessor;
            ContentRepo = contentRepo;
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
        /// Gets or sets the content repository.
        /// </summary>
        public TPublishedContentRepository ContentRepo { get; set; }

        /// <summary>
        /// Sets the inner request.
        /// </summary>
        /// <param name="request">The request.</param>
        public void SetInnerRequest(HttpRequestMessage request)
        {
            if (request != null)
            {
                Request = request;
            }
            else
            {
                Request = new HttpRequestMessage();
                Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            }
        }
    }
}