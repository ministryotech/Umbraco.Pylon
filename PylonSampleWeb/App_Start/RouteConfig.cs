using System.Web.Mvc;
using System.Web.Routing;

namespace PylonSampleWeb
{
    public static class RouteConfig
    {
        /// <summary>
        /// Registers the custom routes for the app.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterCustomRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                null, "blog/page{page}",
                new { controller = "blog", action = "pageredirect", page = UrlParameter.Optional },
                new { page = @"\d+" });

            routes.MapRoute(
                null, "blog/rss.xml",
                new { controller = "blog", action = "feedredirect" });
        }
    }
}