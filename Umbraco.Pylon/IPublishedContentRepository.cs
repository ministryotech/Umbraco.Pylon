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
    /// An interface for accessing published content.
    /// </summary>
    public interface IPublishedContentRepository
    {
        /// <summary>
        /// Gets the umbraco helper.
        /// </summary>
        /// <exception cref="UmbracoException">The Umbraco context property in the PublishedContentRepository is null. The value must be set by passing either an UmbracoContext or UmbracoHelper instance into the constructor.</exception>
        UmbracoHelper Umbraco { get; set; }

        /// <summary>
        /// Gets the media item.
        /// </summary>
        /// <param name="mediaId">The id of the item.</param>
        /// <returns></returns>
        IPublishedContent MediaItem(int mediaId);

        /// <summary>
        /// Gets the URL for some media content.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns>A nicely formed Url.</returns>
        string MediaUrl(int? mediaId);

        /// <summary>
        /// Gets the content URL.
        /// </summary>
        /// <param name="nodeId">The node id.</param>
        /// <returns>A nicely formed Url.</returns>
        string ContentUrl(int nodeId);

        /// <summary>
        /// Returns the content at a specific node.
        /// </summary>
        /// <param name="nodeId">The node id.</param>
        /// <returns>Dynamic content.</returns>
        dynamic Content(int? nodeId);

        /// <summary>
        /// Determines if a piece of media exists.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        bool MediaExists(int? mediaId);
    }
}