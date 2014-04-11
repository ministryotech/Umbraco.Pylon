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
        public SamplePublishedContentRepository()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SamplePublishedContentRepository" /> class.
        /// </summary>
        /// <param name="context">The umbraco context.</param>
        public SamplePublishedContentRepository(UmbracoContext context)
            : base(context)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SamplePublishedContentRepository" /> class.
        /// </summary>
        /// <param name="umbraco">The umbraco helper.</param>
        public SamplePublishedContentRepository(UmbracoHelper umbraco)
            : base(umbraco)
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