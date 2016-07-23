using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Reflection;
using Umbraco.Web;

namespace Umbraco.PylonLite
{
    /// <summary>
    /// IOC Aides.
    /// </summary>
    public static class IocHelper
    {
        /// <summary>
        /// Builds the IoC container.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="registerDefaultSiteAndRepo">if set to <c>true</c> registers default site and repo.</param>
        /// <remarks>
        /// Standard usage would be to manually regster custom implementations of classes inherriting UmbracoSite and PublishedContentRepository.
        /// </remarks>
        public static void RegisterUmbracoPylon(this ContainerBuilder builder, bool registerDefaultSiteAndRepo = false)
        {
            // Register umbraco context, mvc controllers and api controllers
            builder.Register(c => UmbracoContext.Current).AsSelf();
            builder.RegisterControllers(typeof(UmbracoApplication).Assembly);
            builder.RegisterApiControllers(typeof(UmbracoApplication).Assembly);

            //Register core dependencies and repos
            builder.RegisterType<ContentAccessor>().As<IContentAccessor>().InstancePerHttpRequest()
                .OnActivated(e => e.Instance.GetContentFunc = new UmbracoHelper(UmbracoContext.Current).TypedContent);

            builder.RegisterType<MediaAccessor>().As<IMediaAccessor>().InstancePerHttpRequest()
                .OnActivated(e => e.Instance.GetContentFunc = new UmbracoHelper(UmbracoContext.Current).TypedMedia);

            if (registerDefaultSiteAndRepo)
            {
                builder.RegisterType<PublishedContentRepository>().As<IPublishedContentRepository>().InstancePerHttpRequest();
                builder.RegisterType<UmbracoSite>().As<IUmbracoSite>();
            }

            // Register executing assembly
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }
    }
}