using Autofac;
using Autofac.Integration.Mvc;
using Ministry.StrongTyped;
using Umbraco.Pylon.Sample;
using Umbraco.Pylon.Sample.Repositories;

namespace PylonSampleWeb
{
    public static class IocConfig
    {
        private static AutofacDependencyResolver resolver;

        /// <summary>
        /// Gets the IoC Dependency Resolver.
        /// </summary>
        public static AutofacDependencyResolver Resolver
        {
            get { return resolver ?? (resolver = new AutofacDependencyResolver(BuildContainer())); }
        }

        #region | Private Methods |

        /// <summary>
        /// Builds the IoC container.
        /// </summary>
        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            //register all controllers found in the Ministry.Ministryweb assembly
            builder.RegisterControllers(typeof(SampleSite).Assembly);

            //add custom class to the container as Transient instance
            builder.RegisterType<ConfigReader>().As<IConfigReader>();
            builder.RegisterType<WebSession>().As<IWebSession>();
            builder.RegisterType<SamplePublishedContentRepository>().As<ISamplePublishedContentRepository>();
            builder.RegisterModule(new AutofacWebTypesModule());

            return builder.Build();
        }

        #endregion
    }
}