using Moq;
using Umbraco.Core.Models;

namespace Umbraco.PylonLite.TestSupport
{
    /// <summary>
    /// Class for mock content.
    /// </summary>
    public class MockContent : Mock<IPublishedContent>
    {
        private Mock<IPublishedContent> _parent;

        #region | Construction |

        /// <summary>
        /// Mocks the content.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="url">The URL.</param>
        /// <param name="bodyText">The body text.</param>
        /// <returns></returns>
        public MockContent(int id, string name, string url, string bodyText = "")
        {
            SetupAllProperties();
            SetupGet(c => c.Id).Returns(id);
            SetupGet(c => c.Name).Returns(name);
            Setup(c => c.Url).Returns(url);
            this.SetupContentProperty("BodyText", bodyText);
        }

        #endregion

        #region | Singletons |

        /// <summary>
        /// Gets the mock content with basic properties.
        /// </summary>
        public static MockContent WithBasicProperties()
        {
            var mockContent = new MockContent(1, TestConstants.NameString, TestConstants.BodyTextTestString);
            mockContent.SetupContentProperty("Number", 15);

            return mockContent;
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
