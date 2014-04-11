using Umbraco.Core.Models;

namespace Umbraco.Pylon
{
    /// <summary>
    /// A base view model class for view models wrapping a single document type content item.
    /// </summary>
    public abstract class LinkedViewModelBase<TInnerObject>
        where TInnerObject : class, IDocumentType
    {
        /// <summary>
        /// Gets or sets the inner object.
        /// </summary>
        /// <value>
        /// The inner object.
        /// </value>
        protected TInnerObject InnerObject { get; set; }

        /// <summary>
        /// Gets a value indicating whether the model has content.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the model has content; otherwise, <c>false</c>.
        /// </value>
        public bool HasContent { get { return InnerObject != null && InnerObject.HasContent; } }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual string Name { get { return InnerObject.Name; } }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get { return InnerObject.Url; } }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public IPublishedContent Content { get { return InnerObject.Content; } }
    }
}