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

using System;
using System.Net.Http;
using System.Web.Http;

namespace Umbraco.PylonLite.TestSupport.Routes
{
    /// <summary>
    /// A base class to enable easy testing of Web API Routes
    /// </summary>
    public abstract class WebApiRouteTestBase
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
        /// Gets or sets the test support factory.
        /// </summary>
        /// <value>
        /// The test support factory.
        /// </value>
        protected virtual ISupportFactoryForRoutes TestSupportFactory { get; set; }

        #endregion

        /// <summary>
        /// APIs the routes register function.
        /// </summary>
        /// <returns></returns>
        protected abstract Action<HttpConfiguration> ApiRoutesRegisterFunction { get; }

        #region | Assertions |

        /// <summary>
        /// Asserts that an MVC route returns the expected mappings.
        /// </summary>
        /// <param name="urlElement">The URL element.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        protected void AssertRouteIsValid(string urlElement, string controller, string action)
        {
            TestSupportFactory.ApiRouteAssert.AssertRouteIsValid(urlElement, ApiRoutesRegisterFunction, controller, action);
        }

        /// <summary>
        /// Asserts that an MVC route returns the expected mappings.
        /// </summary>
        /// <param name="urlElement">The URL element.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="verb">The verb.</param>
        protected void AssertRouteIsValid(string urlElement, string controller, string action, HttpMethod verb)
        {
            TestSupportFactory.ApiRouteAssert.AssertRouteIsValid(urlElement, ApiRoutesRegisterFunction, controller, action, verb);
        }

        /// <summary>
        /// Asserts that an MVC route returns the expected mappings.
        /// </summary>
        /// <param name="urlElement">The URL element.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="verb">The verb.</param>
        protected void AssertRouteIsInvalid(string urlElement, string controller, string action, HttpMethod verb)
        {
            TestSupportFactory.ApiRouteAssert.AssertRouteIsInvalid(urlElement, ApiRoutesRegisterFunction, controller, action, verb);
        }

        #endregion
    }
}