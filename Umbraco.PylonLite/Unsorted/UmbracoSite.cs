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

using Umbraco.Web;

namespace Umbraco.PylonLite
{
    /// <summary>
    /// Elements for a site defined using a specified Published Content Repository.
    /// </summary>
    /// <remarks>
    /// Make your own implementation of this.
    /// </remarks>
    public interface IUmbracoSite : IUmbracoSite<IPublishedContentRepository>
    { }

    // ReSharper disable once TypeParameterCanBeVariant
    /// <summary>
    /// Elements for a site defined using a specified Published Content Repository.
    /// </summary>
    /// <typeparam name="TPublishedContentRepository">The type of the published content repository.</typeparam>
    /// <remarks>
    /// Make your own implementation of this.
    /// </remarks>
    public interface IUmbracoSite<TPublishedContentRepository> where TPublishedContentRepository : IPublishedContentRepository
    {
        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        TPublishedContentRepository Content { get; }
    }


    /// <summary>
    /// Elements for a site defined using a standard Published Content Repository.
    /// </summary>
    public abstract class UmbracoSite : UmbracoSite<IPublishedContentRepository>
    { 
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="UmbracoSite"/> class.
        /// </summary>
        /// <param name="contentRepo">The content repo.</param>
        protected UmbracoSite(IPublishedContentRepository contentRepo)
            : base(contentRepo)
        { }

        #endregion
    }

    /// <summary>
    /// Elements for a site defined using a specified Published Content Repository.
    /// </summary>
    /// <typeparam name="TPublishedContentRepository">The type of the published content repository.</typeparam>
    /// <remarks>
    /// Make your own implementation of this.
    /// </remarks>
    public abstract class UmbracoSite<TPublishedContentRepository> : IUmbracoSite<TPublishedContentRepository> where TPublishedContentRepository : IPublishedContentRepository
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="UmbracoSite{TPublishedContentRepository}"/> class.
        /// </summary>
        /// <param name="contentRepo">The content repo.</param>
        protected UmbracoSite(TPublishedContentRepository contentRepo)
        {
            Content = contentRepo;
        }

        #endregion

        /// <summary>
        /// Sets the umbraco helper instance.
        /// </summary>
        /// <value>
        /// The umbraco helper instance.
        /// </value>
        internal UmbracoHelper Umbraco { set { Content.Umbraco = value; } }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public TPublishedContentRepository Content { get; private set; }
    }
}