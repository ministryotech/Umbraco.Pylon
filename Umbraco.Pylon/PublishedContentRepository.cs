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
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedContentRepository" /> class.
        /// </summary>
        /// <param name="context">The umbraco context.</param>
        public PublishedContentRepository(UmbracoContext context)
            : this(new UmbracoHelper(context))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedContentRepository" /> class.
        /// </summary>
        /// <param name="umbraco">The umbraco helper.</param>
        public PublishedContentRepository(UmbracoHelper umbraco)
        {
            Umbraco = umbraco;
        }

        #endregion

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
            internal set { umbraco = value; }
        }

        /// <summary>
        /// Determines if a piece of media exists.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        public bool MediaExists(int? mediaId)
        {
            return mediaId.HasValue && mediaId != 0 && MediaItem(mediaId.Value) != null;
        }

        /// <summary>
        /// Gets the media item.
        /// </summary>
        /// <param name="id">The id of the item.</param>
        /// <returns></returns>
        public IPublishedContent MediaItem(int id)
        {
            return Umbraco.TypedMedia(id);
        }

        /// <summary>
        /// Gets the URL for some media content.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns>A nicely formed Url.</returns>
        public string MediaUrl(int? mediaId)
        {
            // ReSharper disable once PossibleInvalidOperationException
            return MediaExists(mediaId)
                ? MediaItem(mediaId.Value).Url
                : String.Empty;
        }

        /// <summary>
        /// Gets the content URL.
        /// </summary>
        /// <param name="nodeId">The node id.</param>
        /// <returns>A nicely formed Url.</returns>
        public string ContentUrl(int nodeId)
        {
            return Umbraco.Url(nodeId);
        }

        /// <summary>
        /// Returns the content at a specific node.
        /// </summary>
        /// <param name="id">The node id.</param>
        /// <returns>Dynamic content.</returns>
        public dynamic Content(int? id)
        {
            return id == null || id.Value == 0 ? null : Umbraco.Content(id);
        }
    }
}