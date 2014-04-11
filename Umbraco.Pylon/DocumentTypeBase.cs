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
using System.Collections.Generic;
using Umbraco.Core.Dynamics;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Umbraco.Pylon
{
    /// <summary>
    /// Base representation of a document type on a system without a custom published content repository.
    /// </summary>
    public abstract class DocumentTypeBase : DocumentTypeBase<IPublishedContentRepository>
    { 
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentTypeBase"/> class.
        /// </summary>
        /// <param name="contentRepo">The content repo.</param>
        protected DocumentTypeBase(IPublishedContentRepository contentRepo)
            : base(contentRepo)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentTypeBase"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="contentRepo">The content repo.</param>
        protected DocumentTypeBase(IPublishedContent content, IPublishedContentRepository contentRepo)
            : base(content, contentRepo)
        { }

        #endregion
    }


    /// <summary>
    /// Base representation of a document type on a system with a custom published content repository.
    /// </summary>
    public abstract class DocumentTypeBase<TPublishedContentRepository> : IDocumentType
        where TPublishedContentRepository : IPublishedContentRepository
    {
        private IPublishedContent content;

        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentTypeBase"/> class.
        /// </summary>
        /// <param name="contentRepo">The content repo.</param>
        protected DocumentTypeBase(TPublishedContentRepository contentRepo)
        {
            ContentRepo = contentRepo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentTypeBase"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="contentRepo">The content repo.</param>
        protected DocumentTypeBase(IPublishedContent content, TPublishedContentRepository contentRepo)
            : this(contentRepo)
        {
            Content = content;
        }

        #endregion

        /// <summary>
        /// Gets the dynamic content.
        /// </summary>
        protected dynamic DynamicContent { get; set; }

        /// <summary>
        /// Gets the content repo.
        /// </summary>
        protected TPublishedContentRepository ContentRepo { get; private set; }

        /// <summary>
        /// Gets the typed content.
        /// </summary>
        public IPublishedContent Content 
        {
            get { return content ?? ConvertDynamicContent(); }
            set { content = value; }
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


        #region | Protected Methods |

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        protected string GetContent(string propertyName)
        {
            if (Content == null) return null;

            var objectContent = Content.GetProperty(propertyName);
            if (objectContent == null) return null;

            return objectContent.Value == null ? null : objectContent.Value.ToString();
        }

        /// <summary>
        /// Gets the content in numeric form
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        protected int GetNumericContent(string propertyName)
        {
            var potentialContent = GetContent(propertyName);
            int value;
            return (potentialContent != null && int.TryParse(potentialContent, out value)) ? value : 0;
        }

        /// <summary>
        /// Gets the content in boolean form
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        protected bool GetBooleanContent(string propertyName)
        {
            var potentialContent = GetContent(propertyName);
            bool value;
            return (potentialContent != null && bool.TryParse(potentialContent, out value)) && value;
        }

        /// <summary>
        /// Gets the content in date form
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="defaultDateTime">The default date time.</param>
        /// <returns></returns>
        protected DateTime GetDateContent(string propertyName, DateTime defaultDateTime)
        {
            var potentialContent = GetContent(propertyName);
            DateTime value;
            return (potentialContent != null && DateTime.TryParse(potentialContent, out value)) ? value : defaultDateTime;
        }

        #endregion

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