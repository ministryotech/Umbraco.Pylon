using Umbraco.Core.Models;

namespace Umbraco.Pylon.Sample.Repositories
{
    public interface ISamplePublishedContentRepository : IPublishedContentRepository
    {
        /// <summary>
        /// Gets the root ancestor.
        /// </summary>
        IPublishedContent RootAncestor { get; }

        /// <summary>
        /// Gets the name of the root ancestor.
        /// </summary>
        string RootAncestorName { get; }
    }
}