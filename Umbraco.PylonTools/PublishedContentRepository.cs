using Ministry;
using Umbraco.Core.Models;
using Umbraco.PylonTools.Models;
using Umbraco.Web;

namespace Umbraco.PylonTools
{
    #region | Interface |

    /// <summary>
    /// Repository to access non-relative site content elements and collections.
    /// </summary>
    public interface IPublishedContentRepository
    {
        /// <summary>
        /// Determines if a piece of media exists.
        /// </summary>
        /// <param name="nodeId">The media id.</param>
        /// <returns>A flag</returns>
        bool MediaExists(int? nodeId);

        /// <summary>
        /// Gets the media item.
        /// </summary>
        /// <param name="nodeId">The id of the item.</param>
        /// <returns>Content</returns>
        MediaFile MediaItem(int? nodeId);

        /// <summary>
        /// Determines if a piece of content exists.
        /// </summary>
        /// <param name="nodeId">The node id.</param>
        /// <returns>A flag</returns>
        bool ContentExists(int? nodeId);

        /// <summary>
        /// Returns the content at a specific node.
        /// </summary>
        /// <param name="nodeId">The node id.</param>
        /// <returns>Content</returns>
        IPublishedContent Content(int? nodeId);
    }

    #endregion

    /// <summary>
    /// Repository to access non-relative site content elements and collections.
    /// </summary>
    public class PublishedContentRepository : IPublishedContentRepository
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedContentRepository"/> class.
        /// </summary>
        /// <param name="umbraco">The umbraco helper.</param>
        public PublishedContentRepository(UmbracoHelper umbraco)
        {
            Umbraco = umbraco.ThrowIfNull(nameof(umbraco));
        }

        #endregion

        /// <summary>
        /// Gets the umbraco helper.
        /// </summary>
        protected UmbracoHelper Umbraco { get; }

        /// <summary>
        /// Determines if a piece of media exists.
        /// </summary>
        /// <param name="nodeId">The media id.</param>
        /// <returns>A flag</returns>
        public bool MediaExists(int? nodeId) => MediaItem(nodeId) != null;

        /// <summary>
        /// Gets the media item.
        /// </summary>
        /// <param name="nodeId">The id of the item.</param>
        /// <returns>Content</returns>
        public MediaFile MediaItem(int? nodeId) 
            => nodeId == null || nodeId.Value == 0 ? null : new MediaFile(Umbraco.TypedMedia(nodeId));

        /// <summary>
        /// Determines if a piece of content exists.
        /// </summary>
        /// <param name="nodeId">The node id.</param>
        /// <returns>A flag</returns>
        public bool ContentExists(int? nodeId) => Content(nodeId) != null;

        /// <summary>
        /// Returns the content at a specific node.
        /// </summary>
        /// <param name="nodeId">The node id.</param>
        /// <returns>Content</returns>
        public IPublishedContent Content(int? nodeId)
            => nodeId == null || nodeId.Value == 0 ? null : Umbraco.TypedContent(nodeId);
    }
}
