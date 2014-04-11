using Umbraco.Core.Models;
using Umbraco.Web;

namespace Umbraco.Pylon
{
    /// <summary>
    /// An interface for accessing published content.
    /// </summary>
    public interface IPublishedContentRepository
    {
        /// <summary>
        /// Gets the media item.
        /// </summary>
        /// <param name="id">The id of the item.</param>
        /// <returns></returns>
        IPublishedContent MediaItem(int id);

        /// <summary>
        /// Gets the umbraco helper.
        /// </summary>
        UmbracoHelper Umbraco { get; }

        /// <summary>
        /// Determines if a piece of media exists.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        bool MediaExists(int? mediaId);

        /// <summary>
        /// Gets the URL for some media content.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns>A nicely formed Url.</returns>
        string MediaUrl(int? mediaId);

        /// <summary>
        /// Gets the content URL.
        /// </summary>
        /// <param name="nodeId">The node id.</param>
        /// <returns>A nicely formed Url.</returns>
        string ContentUrl(int nodeId);

        /// <summary>
        /// Returns the content at a specific node.
        /// </summary>
        /// <param name="id">The node id.</param>
        /// <returns>Dynamic content.</returns>
        dynamic Content(int? id);
    }
}