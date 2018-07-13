namespace Umbraco.Pylon
{
    /// <summary>
    /// A default site class.
    /// </summary>
    /// <seealso cref="UmbracoSite{PublishedContentRepository}" />
    public class DefaultUmbracoSite : UmbracoSite<PublishedContentRepository>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultUmbracoSite"/> class.
        /// </summary>
        public DefaultUmbracoSite() 
            : base(new PublishedContentRepository())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultUmbracoSite"/> class.
        /// </summary>
        /// <param name="contentRepo">The content repo.</param>
        public DefaultUmbracoSite(PublishedContentRepository contentRepo)
            : base(contentRepo)
        { }

        #endregion
    }
}