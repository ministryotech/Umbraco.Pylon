using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

namespace Umbraco.Pylon.Autofac
{
    /// <summary>
    /// Core resolvers and elements to support Autofac implementation.
    /// </summary>
    /// <remarks>
    /// Inherit from this class and instantiate it's subclass in 'OnApplicationStarted'
    /// </remarks>
    public abstract class PylonIocConfig
    {
        private IContainer iocContainerBuilder ;
        private AutofacWebApiDependencyResolver apiResolver;
        private AutofacDependencyResolver mvcResolver;

        /// <summary>
        /// Gets the IoC Dependency Resolver for APIs.
        /// </summary>
        public AutofacWebApiDependencyResolver ApiResolver 
            => apiResolver ?? (apiResolver = new AutofacWebApiDependencyResolver(ContainerBuilder));

        /// <summary>
        /// Gets the IoC Dependency Resolver fro MVC.
        /// </summary>
        public AutofacDependencyResolver MvcResolver 
            => mvcResolver ?? (mvcResolver = new AutofacDependencyResolver(ContainerBuilder));

        #region | Protected Methods |

        /// <summary>
        /// Gets the container builder.
        /// </summary>
        /// <value>
        /// The container builder.
        /// </value>
        protected IContainer ContainerBuilder 
            => iocContainerBuilder ?? (iocContainerBuilder = BuildContainer());

        /// <summary>
        /// Builds the IoC container.
        /// </summary>
        /// <remarks>
        /// Override this to implement your own build.
        /// </remarks>>
        protected abstract IContainer BuildContainer();

        /// <summary>
        /// Builds the default IoC container to bind key Umbraco and Pylon elements if you have no custom ones to add.
        /// </summary>
        /// <returns>A container.</returns>
        protected IContainer BuildDefaultContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterUmbraco();
            return builder.Build();
        }

        #endregion
    }
}