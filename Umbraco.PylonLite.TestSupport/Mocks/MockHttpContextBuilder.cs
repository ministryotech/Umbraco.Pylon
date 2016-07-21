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
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;
using Moq;

namespace Umbraco.PylonLite.TestSupport.Mocks
{
    /// <summary>
    /// Builder for contexts
    /// </summary>
    [Obsolete("The MockHttpContext, MockHttpRequest and MockHttpResponse objects are best used directly through their constructors.")]
    internal static class MockHttpContextBuilder
    {
        #region | Mock Return Methods |

        /// <summary>
        /// Creates an empty mock context.
        /// </summary>
        /// <returns>
        /// A Mock context.
        /// </returns>
        public static Mock<HttpContextBase> MockHttpContext()
        {
            return new MockHttpContext();
        }
        /// <summary>
        /// Creates a mock context for a given URL.
        /// </summary>
        /// <param name="url">The URL to mock the context for.</param>
        /// <param name="verb">The verb.</param>
        /// <returns>
        /// A Mock context.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Justification = "Not valid on Http Mocking")]
        public static MockHttpContext MockHttpContext(string url, HttpVerbs verb = HttpVerbs.Get)
        {
            return new MockHttpContext(MockHttpRequest(url, verb));
        }

        /// <summary>
        /// Creates a mock request for a given URL.
        /// </summary>
        /// <param name="url">The URL to mock the request for.</param>
        /// <param name="verb">The verb.</param>
        /// <returns>
        /// A Mock request.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Justification = "Not valid on Http Mocking")]
        public static MockHttpRequest MockHttpRequest(string url, HttpVerbs verb = HttpVerbs.Get)
        {
            return new MockHttpRequest(url, verb);
        }

        /// <summary>
        /// Creates a mock response.
        /// </summary>
        /// <returns>A Mock response.</returns>
        public static MockHttpResponse MockHttpResponse()
        {
            return new MockHttpResponse();
        }

        #endregion

        #region | Object Return Methods |

        /// <summary>
        /// Creates an empty mock context and returns an object for it.
        /// </summary>
        /// <returns>
        /// A context.
        /// </returns>
        public static HttpContextBase CreateHttpContext()
        {
            return MockHttpContext().Object;
        }

        /// <summary>
        /// Creates a mock context for a given URL and returns an object for it.
        /// </summary>
        /// <param name="url">The URL to mock the context for.</param>
        /// <param name="verb">The verb.</param>
        /// <returns>
        /// A context.
        /// </returns>
        public static HttpContextBase CreateHttpContext(string url, HttpVerbs verb = HttpVerbs.Get)
        {
            return MockHttpContext(url, verb).Object;
        }

        #endregion
    }
}
