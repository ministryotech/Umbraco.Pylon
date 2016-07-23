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

using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Pylon.TestSupport.Mocks;

namespace Umbraco.Pylon.TestSupport.Routes
{
    /// <summary>
    /// A class that asserts MVC Routing.
    /// </summary>
    public class MvcRouteAsserter
    {
        #region | Fields |

        private IAssertionFramework assert;

        #endregion

        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="MvcRouteAsserter" /> class.
        /// </summary>
        /// <param name="assertionFramework">The assertion framework.</param>
        public MvcRouteAsserter(IAssertionFramework assertionFramework)
        {
            assert = assertionFramework;
        }

        #endregion

        #region | Assertions |

        /// <summary>
        /// Asserts that an MVC route returns the expected mappings.
        /// </summary>
        /// <param name="urlElement">The URL element (no host or http prefix) - '~/home/' as opposed to 'http://wibble/home/'.</param>
        /// <param name="routes">The route collection.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="area">The area.</param>
        /// <param name="verb">The verb.</param>
        public void AssertRouteIsValid(string urlElement, RouteCollection routes, string controller, string action, string area = "", HttpVerbs verb = HttpVerbs.Get)
        {
            var httpContextMock = new MockHttpContext(new MockHttpRequest(urlElement, verb));

            RouteData routeData = routes.GetRouteData(httpContextMock.Object);
            assert.IsNotNull(routeData, "Should have found the route");
            assert.AreCaseInsensitiveEqual(controller, routeData.Values["controller"], "Expected a different controller");
            assert.AreCaseInsensitiveEqual(action, routeData.Values["action"], "Expected a different action");
            if (area != string.Empty) assert.AreCaseInsensitiveEqual(area, routeData.Values["area"], "Expected a different area");
        }

        /// <summary>
        /// Asserts that an MVC route returns the expected mappings.
        /// </summary>
        /// <param name="urlElement">The URL element (no host or http prefix) - '~/home/' as opposed to 'http://wibble/home/'.</param>
        /// <param name="routes">The route collection.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="area">The area.</param>
        /// <param name="verb">The verb.</param>
        /// <param name="routeProperties">The route properties to assert.</param>
        public void AssertRouteIsValid(string urlElement, RouteCollection routes, string controller, string action, string area, HttpVerbs verb, object routeProperties)
        {
            var httpContextMock = new MockHttpContext(new MockHttpRequest(urlElement, verb));

            RouteData routeData = routes.GetRouteData(httpContextMock.Object);
            assert.IsNotNull(routeData, "Should have found the route");
            assert.AreCaseInsensitiveEqual(controller, routeData.Values["controller"], "Expected a different controller");
            assert.AreCaseInsensitiveEqual(action, routeData.Values["action"], "Expected a different action");
            if (area != string.Empty) assert.AreCaseInsensitiveEqual(area, routeData.Values["area"], "Expected a different area");

            if (routeProperties != null)
            {
                PropertyInfo[] propInfo = routeProperties.GetType().GetProperties();
                foreach (var pi in propInfo)
                {
                    if (!(routeData.Values.ContainsKey(pi.Name) && routeData.Values[pi.Name].IsCaseInsensitiveEqualTo(pi.GetValue(routeProperties, null))))
                    {
                        var propertyError = "The property '" + pi.Name + "' is not equal to the expected value or is not present";
                        assert.AreCaseInsensitiveEqual(pi.GetValue(routeProperties, null), routeData.Values[pi.Name], propertyError);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Asserts that an MVC route is not returned.
        /// </summary>
        /// <param name="urlElement">The URL element (no host or http prefix) - '~/home/' as opposed to 'http://wibble/home/'.</param>
        /// <param name="routes">The route collection.</param>
        public void AssertRouteIsInvalid(string urlElement, RouteCollection routes)
        {
            var httpContextMock = new MockHttpContext(new MockHttpRequest(urlElement));
            var resultString = string.Empty;

            RouteData routeData = routes.GetRouteData(httpContextMock.Object);
            if (routeData != null)
            {
                var result = new StringBuilder("Should not have found the route, but found...");
                foreach (var i in routeData.Values)
                {
                    result.Append("\n");
                    result.Append(i.Key.ToUpper());
                    result.Append(": ");
                    result.Append(i.Value);
                }

                resultString = result.ToString();
            }

            assert.IsNull(routeData,  resultString);
        }

        /// <summary>
        /// Asserts that an MVC route generates the expected URL output.
        /// </summary>
        /// <param name="expectedUrl">The expected URL.</param>
        /// <param name="routes">The route collection.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeProperties">The route properties.</param>
        public void AssertOutgoingRouteUrlGeneration(string expectedUrl, RouteCollection routes, string controller, string action, string routeName = null, object routeProperties = null)
        {
            var context = new RequestContext(new MockHttpContext().Object, new RouteData());
            RouteValueDictionary routeValues = null;
            
            if (routeProperties != null) routeValues = new RouteValueDictionary(routeProperties);

            string result = UrlHelper.GenerateUrl(routeName, action, controller, routeValues, routes, context, true);

            assert.AreCaseInsensitiveEqual(expectedUrl, result);
        }

        #endregion
    }
}
