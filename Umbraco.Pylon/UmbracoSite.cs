using Umbraco.Web;

namespace Umbraco.Pylon
{
    /// <summary>
    /// Elements for a site defined using a standard Published Content Repository.
    /// </summary>
    public class UmbracoSite : UmbracoSite<PublishedContentRepository>
    { }

    /// <summary>
    /// Elements for a site defined using a specified Published Content Repository.
    /// </summary>
    /// <typeparam name="TPublishedContentRepository">The type of the published content repository.</typeparam>
    public class UmbracoSite<TPublishedContentRepository>
        where TPublishedContentRepository : PublishedContentRepository, new()
    {
        /// <summary>
        /// Sets the umbraco helper instance.
        /// </summary>
        /// <value>
        /// The umbraco helper instance.
        /// </value>
        internal UmbracoHelper Umbraco { set { Content = new TPublishedContentRepository { Umbraco = value }; } }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public TPublishedContentRepository Content { get; private set; }
    }
}