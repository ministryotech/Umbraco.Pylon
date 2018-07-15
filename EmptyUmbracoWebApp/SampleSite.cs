using EmptyUmbracoWebApp.Repositories;
using Umbraco.Pylon;

namespace EmptyUmbracoWebApp
{
    /// <summary>
    /// Elements for the site.
    /// </summary>
    public interface ISampleSite : IUmbracoSite<ISamplePublishedContentRepository>
    {
        /// <summary>
        /// Gets the name of the site.
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// Elements for the site.
    /// </summary>
    public class SampleSite : UmbracoSite<ISamplePublishedContentRepository>, ISampleSite
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleSite" /> class.
        /// </summary>
        /// <param name="contentRepo">The content repo.</param>
        public SampleSite(ISamplePublishedContentRepository contentRepo)
            : base(contentRepo)
        { }

        #endregion

        /// <summary>
        /// Gets the name of the site.
        /// </summary>
        public string Name => Content.RootAncestor.Name;
    }
}