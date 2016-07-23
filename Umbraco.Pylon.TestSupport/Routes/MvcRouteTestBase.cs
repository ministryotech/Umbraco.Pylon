// Copyright (c) 2014 Minotech Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files
// (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, 
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do 
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Web.Mvc;
using System.Web.Routing;

namespace Umbraco.Pylon.TestSupport.Routes
{
    /// <summary>
    /// A base class to enable easy testing of MVC Routes
    /// </summary>
    public abstract class MvcRouteTestBase
    {
        #region | Setup & TearDown |

        /// <summary>
        /// In an inherrited class, Sets up the test fixture routes.
        /// </summary>
        /// <example>
        /// This would normally take the form of something like wthis when implemented:
        ///    Routes = new RouteCollection();
        ///    MvcApplication app = new MvcApplication();
        ///    app.RegisterAllRoutes(Routes);
        /// </example>
        public abstract void SetUpFixture();

        #endregion

        #region | Properties |

        /// <summary>
        /// Gets the route collection.
        /// </summary>
        protected RouteCollection Routes { get; set; }

        /// <summary>
        /// Gets or sets the test support factory.
        /// </summary>
        /// <value>
        /// The test support factory.
        /// </value>
        protected ISupportFactoryForRoutes TestSupportFactory { get; set; }

        #endregion

        #region | Assertions |

        /// <summary>
        /// Asserts that an MVC route returns the expected mappings.
        /// </summary>
        /// <param name="urlElement">The URL element.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="area">The area.</param>
        /// <param name="verb">The verb.</param>
        protected void AssertRouteIsValid(string urlElement, string controller, string action, string area = "", HttpVerbs verb = HttpVerbs.Get)
        {
            TestSupportFactory.RouteAssert.AssertRouteIsValid(urlElement, Routes, controller, action, area, verb);
        }

        /// <summary>
        /// Asserts that an MVC route returns the expected mappings.
        /// </summary>
        /// <param name="urlElement">The URL element.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="area">The area.</param>
        /// <param name="verb">The verb.</param>
        /// <param name="routeProperties">The route properties to assert.</param>
        public void AssertRouteIsValid(string urlElement, string controller, string action, string area, HttpVerbs verb, object routeProperties)
        {
            TestSupportFactory.RouteAssert.AssertRouteIsValid(urlElement, Routes, controller, action, area, verb, routeProperties);
        }

        /// <summary>
        /// Asserts that an MVC route returns the expected mappings.
        /// </summary>
        /// <param name="urlElement">The URL element.</param>
        protected void AssertRouteIsInvalid(string urlElement)
        {
            TestSupportFactory.RouteAssert.AssertRouteIsInvalid(urlElement, Routes);
        }

        /// <summary>
        /// Asserts that an MVC route generates the expected URL output.
        /// </summary>
        /// <param name="expectedUrl">The expected URL.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeProperties">The route properties.</param>
        public void AssertOutgoingRouteUrlGeneration(string expectedUrl, string controller, string action, string routeName = null, object routeProperties = null)
        {
            TestSupportFactory.RouteAssert.AssertOutgoingRouteUrlGeneration(expectedUrl, Routes, controller, action, routeName, routeProperties);
        }

        #endregion
    }
}