using EmptyUmbracoWebApp.Models.Content;
using Umbraco.Pylon;
using Umbraco.Web;

namespace EmptyUmbracoWebApp.Repositories
{
    #region | Interface |

    /// <summary>
    /// A repository for providing access to Umbraco media nodes.
    /// </summary>
    public interface ISamplePublishedContentRepository : IPublishedContentRepository
    {
        /// <summary>
        /// Gets the root ancestor.
        /// </summary>
        Home RootAncestor { get; }
    }

    #endregion

    /// <summary>
    /// A repository for providing access to Umbraco media nodes.
    /// </summary>
    public class SamplePublishedContentRepository : PublishedContentRepository, ISamplePublishedContentRepository
    {
        private Home rootAncestor;

        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="SamplePublishedContentRepository" /> class.
        /// </summary>
        /// <remarks>
        /// For dynamic view instantiation only.
        /// </remarks>
        public SamplePublishedContentRepository()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SamplePublishedContentRepository" /> class.
        /// </summary>
        /// <param name="umbraco">The umbraco helper.</param>
        /// <param name="context">The context.</param>
        public SamplePublishedContentRepository(UmbracoHelper umbraco, UmbracoContext context)
            : base(umbraco, context)
        { }

        #endregion

        /// <summary>
        /// Gets the root ancestor.
        /// </summary>
        public Home RootAncestor => rootAncestor ?? (rootAncestor = new Home(Umbraco.TypedContent(SamplePublishedContentNodeIds.RootAncestor)));
    }
}