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

using Umbraco.Web.Mvc;

namespace Umbraco.Pylon
{
    /// <summary>
    /// An abstract base class for Umbraco views.
    /// </summary>
    public abstract class UmbracoPylonViewPage : UmbracoPylonViewPage<IUmbracoSite, IPublishedContentRepository>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="UmbracoPylonViewPage"/> class.
        /// </summary>
        /// <param name="umbracoSite">The umbraco site.</param>
        protected UmbracoPylonViewPage(IUmbracoSite umbracoSite)
            : base(umbracoSite)
        { }

        #endregion
    }

    /// <summary>
    /// An abstract base class for Umbraco views using strongly typed models.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public abstract class UmbracoPylonViewPage<TModel> : UmbracoPylonViewPage<IUmbracoSite, IPublishedContentRepository, TModel>
    { 
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="UmbracoPylonViewPage"/> class.
        /// </summary>
        /// <param name="umbracoSite">The umbraco site.</param>
        protected UmbracoPylonViewPage(IUmbracoSite umbracoSite)
            : base(umbracoSite)
        { }

        #endregion
    }

    /// <summary>
    /// An abstract base class for Umbraco views with a specific content repository.
    /// </summary>
    /// <typeparam name="TUmbracoSite">The type of the umbraco site.</typeparam>
    /// <typeparam name="TPublishedContentRepository">The type of the published content repository.</typeparam>
    public abstract class UmbracoPylonViewPage<TUmbracoSite, TPublishedContentRepository> : UmbracoTemplatePage
        where TUmbracoSite : IUmbracoSite<TPublishedContentRepository>
        where TPublishedContentRepository : IPublishedContentRepository
    {
        private TUmbracoSite _umbracoSite;

        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="UmbracoPylonViewPage{TUmbracoSite, TPublishedContentRepository}"/> class.
        /// </summary>
        protected UmbracoPylonViewPage()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UmbracoPylonViewPage{TUmbracoSite, TPublishedContentRepository}"/> class.
        /// </summary>
        /// <param name="umbracoSite">The umbraco site.</param>
        protected UmbracoPylonViewPage(TUmbracoSite umbracoSite)
        {
            _umbracoSite = umbracoSite;
        }

        #endregion

        /// <summary>
        /// Gets the model in a dynamic form.
        /// </summary>
        public dynamic DynamicModel { get { return CurrentPage; } }

        /// <summary>
        /// Entry point for helper functions defined at root site level.
        /// </summary>
        /// <value>
        /// The ministryweb helper object.
        /// </value>
        public TUmbracoSite UmbracoSite
        {
            get
            {
                ((UmbracoSite<TPublishedContentRepository>)(IUmbracoSite<TPublishedContentRepository>)_umbracoSite).Umbraco = Umbraco;
                return _umbracoSite;
            }
            set { _umbracoSite = value; }
        }
    }

    /// <summary>
    /// An abstract base class for Umbraco views with a specific content repository using strongly typed models.
    /// </summary>
    /// <typeparam name="TUmbracoSite">The type of the umbraco site.</typeparam>
    /// <typeparam name="TPublishedContentRepository">The type of the published content repository.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public abstract class UmbracoPylonViewPage<TUmbracoSite, TPublishedContentRepository, TModel> : UmbracoViewPage<TModel>
        where TUmbracoSite : IUmbracoSite<TPublishedContentRepository>
        where TPublishedContentRepository : IPublishedContentRepository
    {
        private TUmbracoSite _umbracoSite;

        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="UmbracoPylonViewPage{TUmbracoSite, TPublishedContentRepository, TModel}"/> class.
        /// </summary>
        protected UmbracoPylonViewPage()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UmbracoPylonViewPage{TUmbracoSite, TPublishedContentRepository, TModel}"/> class.
        /// </summary>
        /// <param name="umbracoSite">The umbraco site.</param>
        protected UmbracoPylonViewPage(TUmbracoSite umbracoSite)
        {
            _umbracoSite = umbracoSite;
        }

        #endregion

        /// <summary>
        /// Entry point for helper functions defined at root site level.
        /// </summary>
        /// <value>
        /// The ministryweb helper object.
        /// </value>
        public TUmbracoSite UmbracoSite
        {
            get
            {
                ((UmbracoSite<TPublishedContentRepository>)(IUmbracoSite<TPublishedContentRepository>)_umbracoSite).Umbraco = Umbraco;
                return _umbracoSite;
            }
            set { _umbracoSite = value; }
        }
    }
}