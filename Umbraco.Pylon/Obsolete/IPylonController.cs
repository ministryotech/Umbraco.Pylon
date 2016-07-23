// Copyright (c) 2015 Minotech Ltd.
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
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace Umbraco.Pylon
{
    /// <summary>
    /// Definition for a custom umbraco controller AND the controller that sits inside it.
    /// </summary>
    /// <remarks>
    /// The main controller and the inner controller should share the same interfaces with methods passed through. This enables testing the inner controllers without pain.
    /// </remarks>
    [Obsolete("Pylon controllers are no longer supported - Use a thin controller and a testable service passed in through DI as a recommended pattern")]
    public interface IPylonController<TPublishedContentRepository> :
        IActionFilter, IAuthorizationFilter, IDisposable, IExceptionFilter, IResultFilter, IAsyncController, IAsyncManagerContainer
        where TPublishedContentRepository : IPublishedContentRepository
    {
        /// <summary>
        /// Gets or sets a value indicating whether file checking is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if file checking is enabled; otherwise, <c>false</c>.
        /// </value>
        bool EnableFileCheck { get; set; }

        /// <summary>
        /// Gets or sets the content repository.
        /// </summary>
        TPublishedContentRepository ContentRepo { get; set; }

        /// <summary>
        /// Gets or sets the view string renderer.
        /// </summary>
        IViewStringRenderer ViewStringRenderer { get; set; }

        /// <summary>
        /// Checks to make sure the physical view file exists on disk
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        bool EnsurePhsyicalViewExists(string template);

        /// <summary>
        /// Sets the ControllerContext to a defaults state when no existing ControllerContext is present
        /// </summary>
        ControllerContext SetDefaultContext();
    }
}
