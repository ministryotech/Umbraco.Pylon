using Moq;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;

namespace Umbraco.PylonLite.TestSupport.Mocks
{
    /// <summary>
    /// A context structural mock.
    /// </summary>
    public class MockHttpRequest : Mock<HttpRequestBase>
    {
        private HttpVerbs _httpVerb;
        private string _url;
        private NameValueCollection _serverVariables;

        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="MockHttpRequest"/> class.
        /// </summary>
        public MockHttpRequest()
        {
            SetupAllProperties();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockHttpRequest"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="verb">The verb.</param>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Justification = "Not valid on Http Mocking")]
        public MockHttpRequest(string url, HttpVerbs verb = HttpVerbs.Get)
        {
            SetupAllProperties();
            Url = url;
            Verb = verb;
        }

        #endregion

        /// <summary>
        /// Gets or sets the HttpMethod.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        public HttpVerbs Verb
        {
            get { return _httpVerb; }
            set
            {
                _httpVerb = value;
                Setup(th => th.HttpMethod).Returns(value.ToString());
            }
        }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        /// <value>
        /// The url.
        /// </value>
        public string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                Setup(th => th.AppRelativeCurrentExecutionFilePath).Returns(value);
            }
        }

        /// <summary>
        /// Gets or sets the route data.
        /// </summary>
        public NameValueCollection ServerVariables
        {
            get
            {
                if (_serverVariables != null) return _serverVariables;

                _serverVariables = new NameValueCollection();
                Setup(r => r.ServerVariables).Returns(_serverVariables);
                return _serverVariables;
            }
            set
            {
                _serverVariables = value;
                Setup(r => r.ServerVariables).Returns(_serverVariables);
            }
        }
    }
}
