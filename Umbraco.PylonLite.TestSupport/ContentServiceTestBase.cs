using Moq;

namespace Umbraco.PylonLite.TestSupport
{
    /// <summary>
    /// Base test file
    /// </summary>
    /// <typeparam name="TObjUt">The type of the object under test.</typeparam>
    /// <typeparam name="TContentRepo"></typeparam>
    public abstract class ContentServiceTestBase<TObjUt, TContentRepo> : ContentTestBase<TObjUt>
        where TContentRepo : class, IPublishedContentRepository
    {
        private Mock<TContentRepo> _mockRepo;

        #region | Setup and TearDown |

        /// <summary>
        /// Tears down.
        /// </summary>
        protected new void TearDown()
        {
            base.TearDown();

            MockRepo = null;
        }

        #endregion

        /// <summary>
        /// Gets or sets the mock repo.
        /// </summary>
        protected Mock<TContentRepo> MockRepo
        {
            get { return _mockRepo ?? (_mockRepo = new Mock<TContentRepo>()); }
            set { _mockRepo = value; }
        }
    }
}
