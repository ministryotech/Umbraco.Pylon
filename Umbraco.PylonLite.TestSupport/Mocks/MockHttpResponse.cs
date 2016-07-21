using Moq;
using System.Web;

namespace Umbraco.PylonLite.TestSupport.Mocks
{
    /// <summary>
    /// A context structural mock.
    /// </summary>
    public class MockHttpResponse : Mock<HttpResponseBase>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="MockHttpResponse"/> class.
        /// </summary>
        public MockHttpResponse()
        {
            Setup(x => x.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(x => x);
            SetupAllProperties();
        }

        #endregion
    }
}
