﻿// Copyright (c) 2015 Minotech Ltd.
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

using System.Collections.Generic;
using Umbraco.Core.Models;

namespace Umbraco.PylonLite
{
    /// <summary>
    /// Structural interface of a document type.
    /// </summary>
    public interface IDocumentType
    {
        /// <summary>
        /// Gets the typed content.
        /// </summary>
        IPublishedContent Content { get; set; }

        /// <summary>
        /// Gets the dynamic content.
        /// </summary>
        dynamic DynamicContent { get; set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Gets a value indicating whether this item has content.
        /// </summary>
        /// <value>
        ///   <c>true</c> if has content; otherwise, <c>false</c>.
        /// </value>
        bool HasContent { get; }

        /// <summary>
        /// Gets the children.
        /// </summary>
        IEnumerable<IPublishedContent> Children { get; }
    }
}