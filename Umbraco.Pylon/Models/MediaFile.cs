using Ministry;
using Umbraco.Core.Models;

namespace Umbraco.Pylon.Models
{
    /// <summary>
    /// Representation of a media library element.
    /// </summary>
    public class MediaFile
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFile"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public MediaFile(IPublishedContent content)
        {
            Content = content
                .ThrowIfNull(nameof(content))
                .ThrowIf(c => c.ItemType == PublishedItemType.Media, nameof(content));
        }

        #endregion

        /// <summary>
        /// Gets the source content.
        /// </summary>
        protected IPublishedContent Content { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public int Id => Content.Id;

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name => Content.Name;

        /// <summary>
        /// Gets the uploader.
        /// </summary>
        public string Uploader => Content.WriterName;

        /// <summary>
        /// Gets the URL.
        /// </summary>
        public string Url => Content.Url;

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => Name;
    }
}
