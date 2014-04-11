using System;
using umbraco;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Umbraco.Pylon
{
    /// <summary>
    /// A repository for providing access to Umbraco media nodes.
    /// </summary>
    public class PublishedContentRepository : IPublishedContentRepository
    {
        private UmbracoHelper umbraco;

        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedContentRepository" /> class.
        /// </summary>
        public PublishedContentRepository()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedContentRepository" /> class.
        /// </summary>
        /// <param name="context">The umbraco context.</param>
        public PublishedContentRepository(UmbracoContext context)
            : this(new UmbracoHelper(context))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedContentRepository" /> class.
        /// </summary>
        /// <param name="umbraco">The umbraco helper.</param>
        public PublishedContentRepository(UmbracoHelper umbraco)
        {
            Umbraco = umbraco;
        }

        #endregion

        /// <summary>
        /// Gets the umbraco helper.
        /// </summary>
        /// <exception cref="UmbracoException">The Umbraco context property in the PublishedContentRepository is null. The value must be set by passing either an UmbracoContext or UmbracoHelper instance into the constructor.</exception>
        public UmbracoHelper Umbraco
        {
            get
            {
                if (umbraco == null)
                    throw new UmbracoException("The Umbraco context property in the PublishedContentRepository is null. The value must be set by passing either an UmbracoContext or UmbracoHelper instance into the constructor.");

                return umbraco;
            }
            internal set { umbraco = value; }
        }

        /// <summary>
        /// Determines if a piece of media exists.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        public bool MediaExists(int? mediaId)
        {
            return mediaId.HasValue && mediaId != 0 && MediaItem(mediaId.Value) != null;
        }

        /// <summary>
        /// Gets the media item.
        /// </summary>
        /// <param name="id">The id of the item.</param>
        /// <returns></returns>
        public IPublishedContent MediaItem(int id)
        {
            return Umbraco.TypedMedia(id);
        }

        /// <summary>
        /// Gets the URL for some media content.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns>A nicely formed Url.</returns>
        public string MediaUrl(int? mediaId)
        {
            // ReSharper disable once PossibleInvalidOperationException
            return MediaExists(mediaId)
                ? MediaItem(mediaId.Value).Url
                : String.Empty;
        }

        /// <summary>
        /// Gets the content URL.
        /// </summary>
        /// <param name="nodeId">The node id.</param>
        /// <returns>A nicely formed Url.</returns>
        public string ContentUrl(int nodeId)
        {
            return Umbraco.Url(nodeId);
        }

        /// <summary>
        /// Returns the content at a specific node.
        /// </summary>
        /// <param name="id">The node id.</param>
        /// <returns>Dynamic content.</returns>
        public dynamic Content(int? id)
        {
            return id == null || id.Value == 0 ? null : Umbraco.Content(id);
        }
    }
}