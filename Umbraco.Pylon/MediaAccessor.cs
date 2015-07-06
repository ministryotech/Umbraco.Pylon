using System;
using Umbraco.Core.Models;
using Umbraco.Pylon.Annotations;

namespace Umbraco.Pylon
{
    /// <summary>
    /// Accesses media elements
    /// </summary>
    public interface IMediaAccessor
    {
        /// <summary>
        /// Gets or sets the get content function.
        /// </summary>
        /// <value>
        /// The get content function.
        /// </value>
        Func<int, IPublishedContent> GetContentFunc { set; }

        /// <summary>
        /// Determines if a piece of media exists.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        bool Exists(int? mediaId);

        /// <summary>
        /// Gets the media item.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        IPublishedContent Content(int mediaId);

        /// <summary>
        /// Gets the media item.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <param name="getContentFunc">The get content function.</param>
        /// <returns></returns>
        /// <exception cref="System.MissingMethodException">No method content has been specified for the GetContentFunc property</exception>
        IPublishedContent Content(int mediaId, Func<int, IPublishedContent> getContentFunc);

        /// <summary>
        /// Gets the URL for some media content.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns>A nicely formed Url.</returns>
        string Url(int? mediaId);
    }

    /// <summary>
    /// Accesses media elements
    /// </summary>
    [UsedImplicitly]
    public class MediaAccessor : IMediaAccessor
    {
        /// <summary>
        /// Gets or sets the get content function.
        /// </summary>
        /// <value>
        /// The get content function.
        /// </value>
        public Func<int, IPublishedContent> GetContentFunc { private get; set; }

        /// <summary>
        /// Determines if a piece of media exists.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        public bool Exists(int? mediaId)
        {
            return mediaId.HasValue && mediaId != 0 && Content(mediaId.Value) != null;
        }

        /// <summary>
        /// Gets the media item.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        public IPublishedContent Content(int mediaId)
        {
            if (GetContentFunc == null)
                throw new MissingMethodException("No method content has been specified for the GetContentFunc property");

            return GetContentFunc(mediaId);
        }

        /// <summary>
        /// Gets the media item.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <param name="getContentFunc">The get content function.</param>
        /// <returns></returns>
        /// <exception cref="System.MissingMethodException">No method content has been specified for the GetContentFunc property</exception>
        public IPublishedContent Content(int mediaId, Func<int, IPublishedContent> getContentFunc)
        {
            if (getContentFunc == null)
                throw new MissingMethodException("No method content has been specified for the GetContentFunc property");

            GetContentFunc = getContentFunc;
            return GetContentFunc(mediaId);
        }

        /// <summary>
        /// Gets the URL for some media content.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns>A nicely formed Url.</returns>
        public string Url(int? mediaId)
        {
            // ReSharper disable once PossibleInvalidOperationException
            return Exists(mediaId)
                ? Content(mediaId.Value).Url
                : String.Empty;
        }
    }
}