using System.IO;
using System.Web.Mvc;

namespace Umbraco.Pylon
{
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
        /// <param name="controller">The controller.</param>
        /// <returns></returns>
        string Render(string viewName, object model, Controller controller);
    }

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
        /// <param name="controller">The controller.</param>
        /// <returns></returns>
        public string Render(string viewName, object model, Controller controller)
        {
            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData,
                    controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
