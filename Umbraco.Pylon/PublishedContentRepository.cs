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
using umbraco;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Umbraco.Pylon
{
    /// <summary>
    /// A repository for providing access to Umbraco media nodes.
    /// </summary>
    public class PublishedContentRepository : IPublishedContentRepository
    {
        private UmbracoHelper umbraco;

        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedContentRepository" /> class.
        /// </summary>
        public PublishedContentRepository()
            : this(new ContentAccessor(), new MediaAccessor())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedContentRepository" /> class.
        /// </summary>
        /// <param name="context">The umbraco context.</param>
        public PublishedContentRepository(UmbracoContext context)
            : this(new ContentAccessor(), new MediaAccessor(), new UmbracoHelper(context))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedContentRepository" /> class.
        /// </summary>
        /// <param name="umbraco">The umbraco helper.</param>
        public PublishedContentRepository(UmbracoHelper umbraco)
            : this(new ContentAccessor(), new MediaAccessor(), umbraco)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedContentRepository" /> class.
        /// </summary>
        /// <param name="contentAccessor">The content accessor.</param>
        /// <param name="mediaAccessor">The media accessor.</param>
        public PublishedContentRepository(IContentAccessor contentAccessor, IMediaAccessor mediaAccessor)
        {
            SetUpAccessors(contentAccessor, mediaAccessor);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedContentRepository" /> class.
        /// </summary>
        /// <param name="contentAccessor">The content accessor.</param>
        /// <param name="mediaAccessor">The media accessor.</param>
        /// <param name="context">The umbraco context.</param>
        public PublishedContentRepository(IContentAccessor contentAccessor, IMediaAccessor mediaAccessor, UmbracoContext context)
            : this(contentAccessor, mediaAccessor, new UmbracoHelper(context))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedContentRepository" /> class.
        /// </summary>
        /// <param name="contentAccessor">The content accessor.</param>
        /// <param name="mediaAccessor">The media accessor.</param>
        /// <param name="umbraco">The umbraco helper.</param>
        public PublishedContentRepository(IContentAccessor contentAccessor, IMediaAccessor mediaAccessor, UmbracoHelper umbraco)
        {
            Umbraco = umbraco;
            SetUpAccessors(contentAccessor, mediaAccessor);
        }

        /// <summary>
        /// Sets up accessors.
        /// </summary>
        /// <param name="contentAccessor">The content accessor.</param>
        /// <param name="mediaAccessor">The media accessor.</param>
        private void SetUpAccessors(IContentAccessor contentAccessor, IMediaAccessor mediaAccessor)
        {
            if (contentAccessor == null)
                throw new ArgumentNullException("contentAccessor");
            if (contentAccessor == null)
                throw new ArgumentNullException("mediaAccessor");

            GetContent = contentAccessor;
            GetMedia = mediaAccessor;

            if (!HasValidUmbracoHelper) return;

            GetContent.GetContentFunc = Umbraco.TypedContent;
            GetMedia.GetContentFunc = Umbraco.TypedMedia;
        }

        #endregion

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        /// <summary>
        /// Gets or sets the content accessor.
        /// </summary>
        /// <value>
        /// The content accessor (Get).
        /// </value>
        protected IContentAccessor GetContent { get; set; }

        /// <summary>
        /// Gets or sets the media accessor.
        /// </summary>
        /// <value>
        /// The media accessor (GetMedia).
        /// </value>
        protected IMediaAccessor GetMedia { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has a valid umbraco helper.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has an umbraco helper; otherwise, <c>false</c>.
        /// </value>
        public bool HasValidUmbracoHelper
        {
            get
            {
                try
                {
                    return umbraco != null;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the umbraco helper.
        /// </summary>
        /// <exception cref="UmbracoException">The Umbraco context property in the PublishedContentRepository is null. The value must be set by passing either an UmbracoContext or UmbracoHelper instance into the constructor.</exception>
        public UmbracoHelper Umbraco
        {
            get
            {
                if (umbraco == null)
                    throw new UmbracoException("The Umbraco context property in the PublishedContentRepository is null. The value must be set by passing either an UmbracoContext or UmbracoHelper instance into the constructor.");

                return umbraco;
            }
            set
            {
                umbraco = value; 

                if (GetContent != null && GetMedia != null)
                    SetUpAccessors(GetContent, GetMedia);
            }
        }

        /// <summary>
        /// Determines if a piece of media exists.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        public bool MediaExists(int? mediaId)
        {
            return GetMedia.Exists(mediaId);
        }

        /// <summary>
        /// Gets the media item.
        /// </summary>
        /// <param name="mediaId">The id of the item.</param>
        /// <returns></returns>
        public IPublishedContent MediaItem(int mediaId)
        {
            return GetMedia.Content(mediaId);
        }

        /// <summary>s
        /// Gets the URL for some media content.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns>A nicely formed Url.</returns>
        public string MediaUrl(int? mediaId)
        {
            return GetMedia.Url(mediaId);
        }

        /// <summary>
        /// Gets the content URL.
        /// </summary>
        /// <param name="nodeId">The node id.</param>
        /// <returns>A nicely formed Url.</returns>
        public string ContentUrl(int nodeId)
        {
            return GetContent.Content(nodeId).Url;
        }

        /// <summary>
        /// Returns the content at a specific node.
        /// </summary>
        /// <param name="nodeId">The node id.</param>
        /// <returns>Dynamic content.</returns>
        public dynamic Content(int? nodeId)
        {
            return nodeId == null || nodeId.Value == 0 ? null : GetContent.Content(nodeId.Value);
        }
    }
}