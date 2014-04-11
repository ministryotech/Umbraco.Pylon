using System.Web.Routing;
using System;
using System.Web.Mvc;
using Umbraco.Web;

namespace PylonSampleWeb

{
    /// <summary>
    /// Custom Wiring for dependency injections.
    /// </summary>
    public class PylonSampleApplication : UmbracoApplication
    {
        protected override void OnApplicationStarted(object sender, EventArgs e)
        {
            base.OnApplicationStarted(sender, e);

            DependencyResolver.SetResolver(IocConfig.Resolver);
            RouteConfig.RegisterCustomRoutes(RouteTable.Routes);
        }
    }
}