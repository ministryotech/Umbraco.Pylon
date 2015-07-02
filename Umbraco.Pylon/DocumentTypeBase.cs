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

using System.Collections.Generic;
using Umbraco.Core.Dynamics;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Umbraco.Pylon
{
    /// <summary>
    /// Base representation of a document type on a system without a custom published content repository.
    /// </summary>
    public abstract class DocumentTypeBase : IDocumentType
    { 
        private IPublishedContent content;
        
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentTypeBase"/> class.
        /// </summary>
        protected DocumentTypeBase()
        {
            Get = new ContentAccessor();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentTypeBase"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        protected DocumentTypeBase(IPublishedContent content)
        {
            Get = new ContentAccessor { ContentObject = content };
            Content = content;
        }

        #endregion

        /// <summary>
        /// Accessor for formatted content elements.
        /// </summary>
        protected IContentAccessor Get { get; private set; }

        /// <summary>
        /// Gets the dynamic content.
        /// </summary>
        public dynamic DynamicContent { get; set; }

        /// <summary>
        /// Gets the typed content.
        /// </summary>
        public IPublishedContent Content 
        {
            get { return content ?? ConvertDynamicContent(); }
            set
            {
                content = value;
                Get.ContentObject = content;
            }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public int Id { get { return Content.Id; } }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get { return Content.Name; } }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        public virtual string Url { get { return Content.Url(); } }

        /// <summary>
        /// Gets a value indicating whether this item has content.
        /// </summary>
        /// <value>
        ///   <c>true</c> if has content; otherwise, <c>false</c>.
        /// </value>
        public bool HasContent { get { return Content != null && Content.Id != 0; } }

        /// <summary>
        /// Gets the child content.
        /// </summary>
        public IEnumerable<IPublishedContent> Children { get { return Content.Children; } }

        #region | Private Methods |
        
        /// <summary>
        /// Converts the dynamic content.
        /// </summary>
        /// <returns>An instance of content.</returns>
        private IPublishedContent ConvertDynamicContent()
        {
            if (DynamicContent.GetType() == typeof(DynamicNull))
            {
                content = null;
            }
            else
            {
                content = (IPublishedContent)DynamicContent;
            }

            return content;
        }

        #endregion
    }
}