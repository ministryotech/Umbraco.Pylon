using Ministry;
using Umbraco.Core.Models;

namespace Umbraco.Pylon.Models
{
    /// <summary>
    /// Representation of links to place on a page either sourced manually or via content.
    /// </summary>
    public class Link
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="Link" /> class initialised with external properties.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="text">The text.</param>
        /// <param name="openInNewTab">if set to <c>true</c> [open in new tab].</param>
        public Link(string url, string text, bool openInNewTab = false)
        {
            Url = url.ThrowIfNullOrEmpty(nameof(url));
            Text = text.ThrowIfNullOrEmpty(nameof(text));
            OpenInNewTab = openInNewTab;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Link" /> class to point at specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="text">Optional text. If not provided the content name is used instead.</param>
        /// <param name="openInNewTab">if set to <c>true</c> [open in new tab].</param>
        public Link(IPublishedContent content, string text = "", bool openInNewTab = false)
            : this(content.ThrowIfNull(nameof(content)).Url, string.IsNullOrWhiteSpace(text) ? content.Name : text, openInNewTab)
        { }

        #endregion

        /// <summary>
        /// Gets or sets a value indicating whether the link should open in a new tab / window.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the link should open in a new tab; otherwise, <c>false</c>.
        /// </value>
        public bool OpenInNewTab { get; set; }

        /// <summary>
        /// Gets the target.
        /// </summary>
        public string Target => OpenInNewTab ? "_blank" : "_self";

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => Url;
    }
}
