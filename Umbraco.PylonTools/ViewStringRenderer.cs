using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ministry;

namespace Umbraco.PylonTools
{
    #region | Interface |

    /// <summary>
    /// Converts Views to strings.
    /// </summary>
    public interface IViewStringRenderer
    {
        /// <summary>
        /// Renders the specified view to a string.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="model">The model.</param>
        /// <param name="caller">The caller.</param>
        /// <returns></returns>
        string Render(string viewName, object model, Controller caller);
    }

    #endregion

    /// <summary>
    /// Converts Views to strings.
    /// </summary>
    public class ViewStringRenderer : IViewStringRenderer
    {
        /// <summary>
        /// Renders the specified view to a string.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="model">The model.</param>
        /// <param name="caller">The calling controller.</param>
        /// <returns></returns>
        public string Render(string viewName, object model, Controller caller)
        {
            caller.ViewData.Model = model.ThrowIfNull(nameof(model));
            SetDefaultContext(caller.ThrowIfNull(nameof(caller)));

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(caller.ControllerContext, viewName.ThrowIfNullOrEmpty(nameof(viewName)));
                var viewContext = new ViewContext(caller.ControllerContext, viewResult.View, caller.ViewData,
                    caller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(caller.ControllerContext, viewResult.View);

                return sw.GetStringBuilder().ToString();
            }
        }

        #region | Private Methods |

        /// <summary>
        /// Sets a default context if none is present on the controller.
        /// </summary>
        /// <param name="controller">The controller.</param>
        private void SetDefaultContext(ControllerBase controller)
        {
            if (controller.ControllerContext != null)
                return;

            HttpContextBase wrapper = null;
            if (HttpContext.Current != null)
                wrapper = new HttpContextWrapper(HttpContext.Current);

            var routeData = new RouteData();

            if (!routeData.Values.ContainsKey("controller") && !routeData.Values.ContainsKey("Controller"))
                routeData.Values.Add("controller", controller.GetType().Name
                                                            .ToLower()
                                                            .Replace("controller", ""));

            controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);
        }

        #endregion
    }
}
