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

using Umbraco.Core.Models;

namespace Umbraco.PylonLite
{
    // ReSharper disable once TypeParameterCanBeVariant

    /// <summary>
    /// Base representation of a factory for constructing document types.
    /// </summary>
    public abstract class DocumentTypeFactory<TDocumentType, TDocumentTypeInterface> : ContentFactory, IDocumentTypeFactory<TDocumentTypeInterface>
        where TDocumentTypeInterface : IDocumentType
        where TDocumentType : class, TDocumentTypeInterface, new()
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentTypeFactory{TDocumentType, TDocumentTypeInterface}"/> class.
        /// </summary>
        /// <param name="contentAccessor">The content accessor.</param>
        /// <param name="mediaAccessor">The media accessor.</param>
        protected DocumentTypeFactory(IContentAccessor contentAccessor, IMediaAccessor mediaAccessor)
            : base(contentAccessor, mediaAccessor)
        { }

        #endregion

        /// <summary>
        /// Builds the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public virtual TDocumentTypeInterface Build(IPublishedContent content)
        {
            var result = InitBuild(content);

            return result;
        }

        /// <summary>
        /// Determines whether the content provided is of a valid document type for this builder.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public abstract bool IsOfValidDocumentType(IPublishedContent content);

        /// <summary>
        /// Builds the specified content into a model.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        protected TDocumentType InitBuild(IPublishedContent content)
        {
            Get.ContentObject = content;
            var retVal = new TDocumentType { Content = content };

            return retVal;
        }
    }
}