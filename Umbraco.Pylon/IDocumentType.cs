using Umbraco.Core.Models;

namespace Umbraco.Pylon
{
    /// <summary>
    /// Structural interface of a document type.
    /// </summary>
    public interface IDocumentType
    {
        /// <summary>
        /// Gets the typed content.
        /// </summary>
        IPublishedContent Content { get; set; }

        int Id { get; }
        string Name { get; }
        string Url { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Article" /> has content.
        /// </summary>
        /// <value>
        ///   <c>true</c> if has content; otherwise, <c>false</c>.
        /// </value>
        bool HasContent { get; }
    }
}