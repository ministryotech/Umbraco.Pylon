using Umbraco.ModelsBuilder;
using Umbraco.Web;

namespace EmptyUmbracoWebApp.Models.Content
{
    /// <summary>
    /// Home page content.
    /// </summary>
    public partial class Home
    {
        ///<summary>
        /// Optional Article: An optional extra featured article to display
        ///</summary>
        [ImplementPropertyType("optionalArticle")]
        public string OptionalArticle => this.GetPropertyValue<string>("optionalExtraArticle");
        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName
            => !string.IsNullOrEmpty(PrimaryHeader) ? PrimaryHeader : Name;

        /// <summary>
        /// Gets a value indicating whether the page has an optional extra article.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the page has an optional extra article; otherwise, <c>false</c>.
        /// </value>
        public bool HasOptionalArticle => OptionalArticle != null;
    }
}
