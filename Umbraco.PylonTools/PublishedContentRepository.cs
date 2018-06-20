using System;
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
        /// Sets the umbraco helper.
        /// </summary>
        /// <remarks>
        /// Used to set the helper within a view when using <see cref="PylonViewPage"/>.
        /// </remarks>
        UmbracoHelper Umbraco { set; }

        /// <summary>
        /// Gets or Sets the umbraco context.
        /// </summary>
        /// <remarks>
        /// Used to set the context within a view when using <see cref="PylonViewPage"/>.
        /// </remarks>
        UmbracoContext Context { get; set; }

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
        private UmbracoContext context;
        private UmbracoHelper umbraco;

        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedContentRepository" /> class.
        /// </summary>
        /// <param name="umbraco">The umbraco helper.</param>
        /// <param name="context">The context.</param>
        public PublishedContentRepository(UmbracoHelper umbraco, UmbracoContext context)
        {
            this.umbraco = umbraco.ThrowIfNull(nameof(umbraco));
            this.context = context.ThrowIfNull(nameof(context));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedContentRepository"/> class.
        /// </summary>
        /// <param name="umbraco">The umbraco helper.</param>
        public PublishedContentRepository(UmbracoHelper umbraco)
        {
            this.umbraco = umbraco.ThrowIfNull(nameof(umbraco));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedContentRepository"/> class.
        /// </summary>
        /// <remarks>
        /// If constructed without an Umbraco helper instance, the helper must be set before any methods are called.
        /// </remarks>
        public PublishedContentRepository()
        { }

        #endregion

        /// <summary>
        /// Gets or sets the umbraco helper.
        /// </summary>
        /// <remarks>
        /// The set is used to set the helper within a view when using <see cref="PylonViewPage"/>.
        /// </remarks>
        public UmbracoHelper Umbraco
        {
            protected get
            {
                if (umbraco == null)
                    throw new ApplicationException("Unable to use methods on the Content Repository until the UmbracoHelper instance is set. " +
                                                   "Either use the constructor that thakes a helper or check your IoC configuration.");
                return umbraco;
            }
            set => umbraco = value;
        }

        /// <summary>
        /// Gets or sets the umbraco helper.
        /// </summary>
        /// <remarks>
        /// The set is used to set the helper within a view when using <see cref="PylonViewPage"/>.
        /// </remarks>
        public UmbracoContext Context
        {
            get
            {
                if (context == null)
                    throw new ApplicationException("No context is available. " +
                                                   "Either use the constructor that thakes a context or check your IoC configuration.");
                return context;
            }
            set => context = value;
        }

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
