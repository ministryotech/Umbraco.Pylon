using Umbraco.Core.Models;
using Umbraco.Web;

namespace Umbraco.Pylon.Sample.Repositories
{
    /// <summary>
    /// A repository for providing access to Umbraco media nodes.
    /// </summary>
    public class SamplePublishedContentRepository : PublishedContentRepository, ISamplePublishedContentRepository
    {
        private IPublishedContent rootAncestor;

        private const int RootAncestorId = 1047;

        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="SamplePublishedContentRepository" /> class.
        /// </summary>
        /// <param name="contentAccessor">The content accessor.</param>
        /// <param name="mediaAccessor">The media accessor.</param>
        public SamplePublishedContentRepository(IContentAccessor contentAccessor, IMediaAccessor mediaAccessor)
            : base(contentAccessor, mediaAccessor)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SamplePublishedContentRepository" /> class.
        /// </summary>
        /// <param name="contentAccessor">The content accessor.</param>
        /// <param name="mediaAccessor">The media accessor.</param>
        /// <param name="context">The umbraco context.</param>
        public SamplePublishedContentRepository(IContentAccessor contentAccessor, IMediaAccessor mediaAccessor, UmbracoContext context)
            : base(contentAccessor, mediaAccessor, context)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SamplePublishedContentRepository" /> class.
        /// </summary>
        /// <param name="contentAccessor">The content accessor.</param>
        /// <param name="mediaAccessor">The media accessor.</param>
        /// <param name="umbraco">The umbraco helper.</param>
        public SamplePublishedContentRepository(IContentAccessor contentAccessor, IMediaAccessor mediaAccessor, UmbracoHelper umbraco)
            : base(contentAccessor, mediaAccessor, umbraco)
        { }

        #endregion

        /// <summary>
        /// Gets the root ancestor.
        /// </summary>
        public IPublishedContent RootAncestor
        {
            get { return rootAncestor ?? (rootAncestor = Umbraco.Content(RootAncestorId)); }
        }
        
        /// <summary>
        /// Gets the name of the root ancestor.
        /// </summary>
        public string RootAncestorName
        {
            get { return RootAncestor.Name; }
        }
    }
}