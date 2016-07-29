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

using System.Globalization;
using Umbraco.Core.Models;
using Umbraco.Web.Models;

namespace Umbraco.Pylon
{
    /// <summary>
    /// Use this class as a base class when creating your own custom models that enhance existing content to enable access to dynamic content through Model.DynamicContent
    /// </summary>
    public class PylonRenderModel : RenderModel
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificationRenderModel" /> class.
        /// </summary>
        /// <param name="content"></param>
        public PylonRenderModel(IPublishedContent content)
            : base(content)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificationRenderModel" /> class.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="culture"></param>
        public PylonRenderModel(IPublishedContent content, CultureInfo culture)
            : base(content, culture)
        { }

        #endregion

        /// <summary>
        /// Gets or sets the dynamic content associated with the page.
        /// </summary>
        /// <value>
        /// The dynamic content.
        /// </value>
        public dynamic DynamicContent { get; set; }
    }
}