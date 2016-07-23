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
    /// A class that asserts Web Api Routing.
    /// </summary>
    public class WebApiRouteAsserter
    {
        #region | Fields |

        private IAssertionFramework assert;

        #endregion

        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="MvcRouteAsserter"/> class.
        /// </summary>
        public WebApiRouteAsserter(IAssertionFramework assertionFramework)
        {
            assert = assertionFramework;
        }

        #endregion

        #region | Assertions |

        /// <summary>
        /// Asserts that an API route returns the expected mappings for a GET call.
        /// </summary>
        /// <param name="urlElement">The URL element (no host or http prefix) - '~/home/' as opposed to 'http://wibble/home/'.</param>
        /// <param name="apiRoutesRegisterFunction">The API routes register function.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        public void AssertRouteIsValid(string urlElement, Action<HttpConfiguration> apiRoutesRegisterFunction, string controller, string action)
        {
            var fullDummyUrl = GetFullDummyUrl(urlElement);
            var maps = true;
            try
            {
                fullDummyUrl.ShouldMapTo(apiRoutesRegisterFunction, controller, action);
            }
            catch
            {
                maps = false;
            }

            assert.IsTrue(maps, "Route not valid");
        }

        /// <summary>
        /// Asserts that an API route returns the expected mappings.
        /// </summary>
        /// <param name="urlElement">The URL element (no host or http prefix) - '~/home/' as opposed to 'http://wibble/home/'.</param>
        /// <param name="apiRoutesRegisterFunction">The API routes register function.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="verb">The verb.</param>
        public void AssertRouteIsValid(string urlElement, Action<HttpConfiguration> apiRoutesRegisterFunction, string controller, string action, HttpMethod verb)
        {
            var fullDummyUrl = GetFullDummyUrl(urlElement);
            var maps = true;
            try
            {
                fullDummyUrl.ShouldMapTo(apiRoutesRegisterFunction, controller, action, verb);
            }
            catch
            {
                maps = false;
            }

            assert.IsTrue(maps, "Route not valid");
        }

        /// <summary>
        /// Asserts that an API route is not returned.
        /// </summary>
        /// <param name="urlElement">The URL element (no host or http prefix) - '~/home/' as opposed to 'http://wibble/home/'.</param>
        /// <param name="apiRoutesRegisterFunction">The API routes register function.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="verb">The verb.</param>
        public void AssertRouteIsInvalid(string urlElement, Action<HttpConfiguration> apiRoutesRegisterFunction, string controller, string action, HttpMethod verb)
        {
            var fullDummyUrl = GetFullDummyUrl(urlElement);
            var maps = true;
            try
            {
                fullDummyUrl.ShouldMapTo(apiRoutesRegisterFunction, controller, action, verb);
            }
            catch
            {
                maps = false;
            }

            assert.IsFalse(maps, "Route is valid and should not be");
        }

        #endregion

        #region | Private Methods |

        /// <summary>
        /// Gets the full dummy URL.
        /// </summary>
        /// <param name="urlElement">The URL element.</param>
        /// <returns></returns>
        private static string GetFullDummyUrl(string urlElement)
        {
            var partialUrl = urlElement;

            if (partialUrl.StartsWith("~"))
                partialUrl = partialUrl.Substring(1, partialUrl.Length - 1);

            if (partialUrl.StartsWith("/"))
                partialUrl = partialUrl.Substring(1, partialUrl.Length - 1);

            return "http://my.site.local/" + partialUrl;
        }

        #endregion
    }
}
