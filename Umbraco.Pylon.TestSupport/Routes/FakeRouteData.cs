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

using System.Collections.Generic;
using System.Web.Http.Routing;

namespace Umbraco.PylonLite.TestSupport.Routes
{
    /// <summary>
    /// A fake passing object.
    /// </summary>
    internal class FakeRouteData : IHttpRouteData
    {
        /// <summary>
        /// Gets the object that represents the route.
        /// </summary>
        /// <returns>The object that represents the route.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IHttpRoute Route
        {
            get { return null; }
        }

        /// <summary>
        /// Gets a collection of URL parameter values and default values for the route.
        /// </summary>
        /// <returns>The values that are parsed from the URL and from default values.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IDictionary<string, object> Values
        {
            get { return new Dictionary<string, object>(); }
        }
    }
}
