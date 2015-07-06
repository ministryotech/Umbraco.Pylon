using Moq;
using Umbraco.Core.Models;

namespace Umbraco.Pylon.TestSupport
{
    /// <summary>
    /// Class for mock media.
    /// </summary>
    public class MockMedia : Mock<IPublishedContent>
    {
        private Mock<IPublishedContent> _parent;

        #region | Construction |

        /// <summary>
        /// Mocks the content.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public MockMedia(int id, string name, string url)
        {
            SetupAllProperties();
            SetupGet(c => c.Id).Returns(id);
            SetupGet(c => c.Name).Returns(name);
            Setup(c => c.Url).Returns(url);
        }

        #endregion

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public Mock<IPublishedContent> Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                Setup(c => c.Parent).Returns(_parent.Object);
            }
        }
    }
}
