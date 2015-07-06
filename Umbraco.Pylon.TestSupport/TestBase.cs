using Moq;

namespace Umbraco.Pylon.TestSupport
{
    /// <summary>
    /// Base test file
    /// </summary>
    /// <typeparam name="TObjUt">The type of the object under test.</typeparam>
    public abstract class TestBase<TObjUt>
    {
        private IContentAccessor _contentAccessor = new ContentAccessor();
        private IMediaAccessor _mediaAccessor = new MediaAccessor();
        private Mock<IContentAccessor> _mockContentAccessor = new Mock<IContentAccessor>();
        private Mock<IMediaAccessor> _mockMediaAccessor = new Mock<IMediaAccessor>();

        #region | Setup and TearDown |

        /// <summary>
        /// Tears down.
        /// </summary>
        protected void TearDown()
        {
            ObjUt = default(TObjUt);

            MockContent = null;
            StubContentAccessor = null;
            StubMediaAccessor = null;
        }

        #endregion

        /// <summary>
        /// The object under test
        /// </summary>
        protected TObjUt ObjUt { get; set; }

        /// <summary>
        /// Gets or sets the content of the mock.
        /// </summary>
        protected MockContent MockContent { get; set; }

        /// <summary>
        /// Gets or sets the stubbed content accessor.
        /// </summary>
        protected IContentAccessor StubContentAccessor
        {
            get { return _contentAccessor ?? (_contentAccessor = new ContentAccessor()); }
            set { _contentAccessor = value; }
        }

        /// <summary>
        /// Gets or sets the stubbed media accessor.
        /// </summary>
        protected IMediaAccessor StubMediaAccessor
        {
            get { return _mediaAccessor ?? (_mediaAccessor = new MediaAccessor()); }
            set { _mediaAccessor = value; }
        }

        /// <summary>
        /// Gets or sets the mock content accessor.
        /// </summary>
        protected Mock<IContentAccessor> MockContentAccessor
        {
            get { return _mockContentAccessor ?? (_mockContentAccessor = new Mock<IContentAccessor>()); }
            set { _mockContentAccessor = value; }
        }

        /// <summary>
        /// Gets or sets the mock media accessor.
        /// </summary>
        protected Mock<IMediaAccessor> MockMediaAccessor
        {
            get { return _mockMediaAccessor ?? (_mockMediaAccessor = new Mock<IMediaAccessor>()); }
            set { _mockMediaAccessor = value; }
        }
    }
}
