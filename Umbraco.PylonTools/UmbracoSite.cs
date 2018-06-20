namespace Umbraco.PylonTools
{
    #region | Interfaces |

    /// <summary>
    /// Elements for a site defined using a specified Published Content Repository.
    /// </summary>
    /// <remarks>
    /// Make your own implementation of this.
    /// </remarks>
    public interface IUmbracoSite : IUmbracoSite<IPublishedContentRepository>
    { }

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
        TPublishedContentRepository Content { get; set; }
    }

    #endregion

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
    public abstract class UmbracoSite<TPublishedContentRepository> : IUmbracoSite<TPublishedContentRepository> 
        where TPublishedContentRepository : class, IPublishedContentRepository
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
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public TPublishedContentRepository Content { get; set; }
    }
}