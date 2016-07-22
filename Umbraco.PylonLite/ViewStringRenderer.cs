using System.IO;
using System.Web.Mvc;

namespace Umbraco.PylonLite
{
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
            caller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(caller.ControllerContext, viewName);
                var viewContext = new ViewContext(caller.ControllerContext, viewResult.View, caller.ViewData,
                    caller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(caller.ControllerContext, viewResult.View);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
