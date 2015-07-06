using System.Web.Mvc;
using Umbraco.Pylon.Sample.Repositories;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Umbraco.Pylon.Sample.Controllers
{
    /// <summary>
    /// Custom Controller interface.
    /// </summary>
    /// <remarks>
    /// The main controller and the inner controller should share the same interfaces with methods passed through. This enables testing the inner controllers without pain.
    /// </remarks>
    public interface ISampleController : IPylonController<ISamplePublishedContentRepository>
    {
        /// <summary>
        /// Default controller view rendering call.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The Team Member view.</returns>
        ActionResult Index(RenderModel model);
    }

    /// <summary>
    /// Base Controller class.
    /// </summary>
    public class SampleController : PylonLayeredController<ISamplePublishedContentRepository, SampleInnerController>, ISampleController
    {
        public SampleController(SampleInnerController innerController, UmbracoContext umbracoContext)
            : base(innerController, umbracoContext)
        { }

        /// <summary>
        /// Default controller view rendering call.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The Team Member view.</returns>
        public override ActionResult Index(RenderModel model)
        {
            return InnerController.Index(model);
        }
    }

    /// <summary>
    /// Inner controller
    /// </summary>
    public class SampleInnerController : PylonInnerController<ISamplePublishedContentRepository>, ISampleController
    {
        public SampleInnerController(ISamplePublishedContentRepository contentRepo, IContentAccessor contentAccessor, IMediaAccessor mediaAccessor) 
            : base(contentRepo, contentAccessor, mediaAccessor)
        { }

        /// <summary>
        /// Default controller view rendering call.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The Team Member view.</returns>
        public ActionResult Index(RenderModel model)
        {
            return CurrentTemplate(model);
        }
    }
}