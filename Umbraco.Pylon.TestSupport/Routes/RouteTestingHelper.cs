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
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Hosting;

namespace Umbraco.PylonLite.TestSupport.Routes
{
    public static class RouteTestingHelper
    {
        /// <summary>
        /// Routes the request.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="request">The request.</param>
        /// <returns>Inbformation about the route.</returns>
        public static RouteInfo RouteRequest(HttpConfiguration config, HttpRequestMessage request)
        {
            // create context
            var controllerContext = new HttpControllerContext(config, new FakeRouteData(), request);

            // get route data
            var routeData = config.Routes.GetRouteData(request);
            RemoveOptionalRoutingParameters(routeData.Values);

            request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            controllerContext.RouteData = routeData;

            // get controller type
            var controllerDescriptor = new DefaultHttpControllerSelector(config).SelectController(request);
            controllerContext.ControllerDescriptor = controllerDescriptor;

            // get action name
            var actionMapping = new ApiControllerActionSelector().SelectAction(controllerContext);

            var info = new RouteInfo(controllerDescriptor.ControllerType, actionMapping.ActionName);

            foreach (var param in actionMapping.GetParameters())
            {
                info.Parameters.Add(param.ParameterName);
            }

            return info;
        }

        #region | Extensions |

        /// <summary>
        /// Determines that a URL maps to a specified controller.
        /// </summary>
        /// <typeparam name="TController">The type of the controller.</typeparam>
        /// <param name="fullDummyUrl">The full dummy URL.</param>
        /// <param name="apiRoutesRegisterFunction">The API routes register function.</param>
        /// <param name="action">The action.</param>
        /// <param name="parameterNames">The parameter names.</param>
        /// <returns></returns>
        public static void ShouldMapTo<TController>(this string fullDummyUrl, Action<HttpConfiguration> apiRoutesRegisterFunction, string action, params string[] parameterNames)
        {
            ShouldMapTo<TController>(fullDummyUrl, apiRoutesRegisterFunction, action, HttpMethod.Get, parameterNames);
        }

        /// <summary>
        /// Determines that a URL maps to a specified controller.
        /// </summary>
        /// <typeparam name="TController">The type of the controller.</typeparam>
        /// <param name="fullDummyUrl">The full dummy URL.</param>
        /// <param name="apiRoutesRegisterFunction">The API routes register function.</param>
        /// <param name="action">The action.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="parameterNames">The parameter names.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// </exception>
        public static void ShouldMapTo<TController>(this string fullDummyUrl, Action<HttpConfiguration> apiRoutesRegisterFunction, string action, HttpMethod httpMethod, params string[] parameterNames)
        {
            var controllerName = typeof(TController).Name;
            ShouldMapTo(fullDummyUrl, apiRoutesRegisterFunction, controllerName, action, httpMethod, parameterNames);
        }

        /// <summary>
        /// Determines that a URL maps to a specified controller.
        /// </summary>
        /// <param name="fullDummyUrl">The full dummy URL.</param>
        /// <param name="apiRoutesRegisterFunction">The API routes register function.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="parameterNames">The parameter names.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// </exception>
        public static void ShouldMapTo(this string fullDummyUrl, Action<HttpConfiguration> apiRoutesRegisterFunction, string controllerName, string action, params string[] parameterNames)
        {
            ShouldMapTo(fullDummyUrl, apiRoutesRegisterFunction, controllerName, action, HttpMethod.Get, parameterNames);
        }

        /// <summary>
        /// Determines that a URL maps to a specified controller.
        /// </summary>
        /// <param name="fullDummyUrl">The full dummy URL.</param>
        /// <param name="apiRoutesRegisterFunction">The API routes register function.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="parameterNames">The parameter names.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// </exception>
        public static void ShouldMapTo(this string fullDummyUrl, Action<HttpConfiguration> apiRoutesRegisterFunction, string controllerName, string action, HttpMethod httpMethod, params string[] parameterNames)
        {
            var request = new HttpRequestMessage(httpMethod, fullDummyUrl);
            var config = new HttpConfiguration();
            apiRoutesRegisterFunction(config);

            RouteInfo route;

            try
            {
                route = RouteRequest(config, request);
            }
            catch (HttpResponseException hrex)
            {
                throw new Exception(string.Format("The specified route '{0}' generated an HTTP Response Error '{1}'", fullDummyUrl, hrex.Response.ReasonPhrase));
            }

            if (route.Controller.Name != controllerName)
                throw new Exception(string.Format("The specified route '{0}' does not match the expected controller '{1}'", fullDummyUrl, controllerName));

            if (route.Action.ToLowerInvariant() != action.ToLowerInvariant())
                throw new Exception(string.Format("The specified route '{0}' does not match the expected action '{1}'", fullDummyUrl, action));

            if (!parameterNames.Any()) return;

            if (route.Parameters.Count < parameterNames.Count())
                throw new Exception(
                    string.Format(
                        "The specified route '{0}' does not have enough parameters - expected '{1}' but was '{2}'",
                        fullDummyUrl, parameterNames.Count(), route.Parameters.Count));

            foreach (var param in parameterNames)
            {
                if (!route.Parameters.Contains(param))
                    throw new Exception(
                        string.Format("The specified route '{0}' does not contain the expected parameter '{1}'",
                            fullDummyUrl, param));
            }
        }

        #endregion

        #region | Private Methods |

        /// <summary>
        /// Removes the optional routing parameters.
        /// </summary>
        /// <param name="routeValues">The route values.</param>
        private static void RemoveOptionalRoutingParameters(IDictionary<string, object> routeValues)
        {
            var optionalParams = routeValues
                .Where(x => x.Value == RouteParameter.Optional)
                .Select(x => x.Key)
                .ToList();

            foreach (var key in optionalParams)
            {
                routeValues.Remove(key);
            }
        }

        #endregion
    }

    /// <summary>
    /// Route information
    /// </summary>
    public class RouteInfo
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteInfo"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        public RouteInfo(Type controller, string action)
        {
            Controller = controller;
            Action = action;
            Parameters = new List<string>();
        }

        #endregion

        public Type Controller { get; private set; }
        public string Action { get; private set; }
        public List<string> Parameters { get; private set; }
    }
}
