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
        /// <param name="caller">The caller.</param>
        /// <returns></returns>
        string Render(string viewName, object model, Controller caller);
    }
}
