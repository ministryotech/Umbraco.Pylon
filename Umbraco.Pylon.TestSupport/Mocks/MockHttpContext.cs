using Moq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Umbraco.Pylon.TestSupport.Mocks
{
    /// <summary>
    /// A context structural mock.
    /// </summary>
    public class MockHttpContext : Mock<HttpContextBase>
    {
        private RouteData _routeData;
        private Mock<IPrincipal> _mockUser;

        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="MockHttpContext"/> class.
        /// </summary>
        /// <param name="mockRequest">The mock request.</param>
        /// <param name="mockResponse">The mock response.</param>
        public MockHttpContext(MockHttpRequest mockRequest, MockHttpResponse mockResponse)
        {
            Request = mockRequest;
            Response = mockResponse;

            SetupAllProperties();
            Setup(th => th.Request).Returns(Request.Object);
            Setup(th => th.Response).Returns(Response.Object);

            User = new Mock<IPrincipal>();
            User.SetupAllProperties();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockHttpContext"/> class.
        /// </summary>
        /// <param name="mockRequest">The mock request.</param>
        public MockHttpContext(MockHttpRequest mockRequest)
            : this(mockRequest, new MockHttpResponse())
        { }


        /// <summary>
        /// Initializes a new instance of the <see cref="MockHttpContext"/> class.
        /// </summary>
        /// <param name="mockResponse">The mock response.</param>
        public MockHttpContext(MockHttpResponse mockResponse)
            : this(new MockHttpRequest(), mockResponse)
        { }


        /// <summary>
        /// Initializes a new instance of the <see cref="MockHttpContext"/> class.
        /// </summary>
        public MockHttpContext()
            : this(new MockHttpRequest(), new MockHttpResponse())
        { }

        #endregion

        /// <summary>
        /// Gets the request.
        /// </summary>
        public MockHttpRequest Request { get; private set; }

        /// <summary>
        /// Gets the response.
        /// </summary>
        public MockHttpResponse Response { get; private set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public Mock<IPrincipal> User
        {
            get { return _mockUser; }
            set
            {
                _mockUser = value;
                Setup(th => th.User).Returns(value.Object);
            }
        }

        /// <summary>
        /// Gets or sets the route data.
        /// </summary>
        public RouteData RouteData
        {
            get { return _routeData ?? (_routeData = new RouteData()); }
            set { _routeData = value; }
        }

        /// <summary>
        /// Applies the context to a given controller.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public void ApplyTo(ControllerBase controller)
        {
            controller.ControllerContext = new ControllerContext(Object, RouteData, controller);
        }

        /// <summary>
        /// Applies the context to a given controller with specific route data.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="routeData">The route data.</param>
        public void ApplyTo(ControllerBase controller, RouteData routeData)
        {
            RouteData = routeData;
            controller.ControllerContext = new ControllerContext(Object, RouteData, controller);
        }
    }
}
