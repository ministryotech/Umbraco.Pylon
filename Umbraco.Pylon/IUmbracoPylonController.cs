using System;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using Umbraco.Web;

namespace Umbraco.Pylon
{
    /// <summary>
    /// Definition for a custom umbraco controller AND the controller that sits inside it.
    /// </summary>
    /// <remarks>
    /// The main controller and the inner controller should share the same interfaces with methods passed through. This enables testing the inner controllers without pain.
    /// </remarks>
    public interface IUmbracoPylonController<TPublishedContentRepository> :
        IActionFilter, IAuthorizationFilter, IDisposable, IExceptionFilter, IResultFilter, IAsyncController, IAsyncManagerContainer
        where TPublishedContentRepository : IPublishedContentRepository
    {
        /// <summary>
        /// Gets or Sets the umbraco context.
        /// </summary>
        /// <remarks>
        /// Avoid using this wherever possible for your own sanity.
        /// </remarks>
        UmbracoContext UmbracoContext { get; set; }

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
        /// Returns an ActionResult based on the template name found in the route values and the given model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <remarks>
        /// If the template found in the route values doesn't physically exist, then an empty ContentResult will be returned.
        /// </remarks>
        ActionResult CurrentTemplate<TModel>(TModel model);

        /// <summary>
        /// Sets the ControllerContext to a defaults state when no existing ControllerContext is present       
        /// </summary>
        ControllerContext SetDefaultContext();
    }
}
