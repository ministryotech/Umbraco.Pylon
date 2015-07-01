using Umbraco.Web;

namespace Umbraco.Pylon
{
    /// <summary>
    /// Definition for a custom umbraco data controller AND the data controller that sits inside ait.
    /// </summary>
    /// <remarks>
    /// The main controller and the inner controller should share the same interfaces with methods passed through. This enables testing the inner controllers without pain.
    /// </remarks>
    public interface IUmbracoPylonApiController<TPublishedContentRepository>
        where TPublishedContentRepository : IPublishedContentRepository
    {
        /// <summary>
        /// Gets or Sets the umbraco context.
        /// </summary>
        /// <remarks>
        /// Avoid using this wherever possible for your own sanity.
        /// </remarks>
        UmbracoContext UmbracoContext { get; set; }

        /// <summary>
        /// Gets or sets the content repository.
        /// </summary>
        TPublishedContentRepository ContentRepo { get; set; }
    }
}
