using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.WebApi;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Web;
using Autofac.Integration.Mvc;

namespace Umbraco.Pylon.Autofac
{
    /// <summary>
    /// Fluent extensions / Registering methods to bind key Umbraco contexts and services.
    /// </summary>
    public static class DependencyRegistrar
    {
        /// <summary>
        /// Registers bindings for all of the dependencies to support Umbraco Pylon based apps.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="controllersAssembly">The controllers assembly to bind the local controllers.</param>
        /// <returns>
        /// The builder, for fluent bindings.
        /// </returns>
        public static ContainerBuilder RegisterUmbraco(this ContainerBuilder builder, Assembly controllersAssembly = null)
            => builder
                .RegisterAllControllers(controllersAssembly)
                .RegisterContexts()
                .RegisterUmbracoServices()
                .RegisterPylon();

        /// <summary>
        /// Registers bindings for a custom content repository.
        /// </summary>
        /// <typeparam name="TSiteContentRepository">The type of the site content repository.</typeparam>
        /// <typeparam name="TRepoInterface">The type of the repo interface.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder, for fluent bindings.
        /// </returns>
        /// <remarks>
        /// Use this binding if you implement your own content repository (recommended).
        /// </remarks>
        public static ContainerBuilder RegisterCustomContentRepository<TSiteContentRepository, TRepoInterface>(this ContainerBuilder builder)
            where TRepoInterface : IPublishedContentRepository
            where TSiteContentRepository : PublishedContentRepository, TRepoInterface, new()
        {
            builder.Register(repo => new TSiteContentRepository
            {
                Umbraco = DependencyResolver.Current.GetService<UmbracoHelper>(),
                Context = DependencyResolver.Current.GetService<UmbracoContext>()
            }).As<TRepoInterface>().InstancePerRequest();

            return builder;
        }

        /// <summary>
        /// Registers bindings for the default content repository.
        /// </summary>
        /// <remarks>
        /// Use this binding if you don't intend to add any custom methods to your own repository implementation.
        /// We recommend that you inherit from the generic versions of the Pylon controllers so you can make use of strongly typed site content.
        /// </remarks>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder, for fluent bindings.</returns>
        public static ContainerBuilder RegisterDefaultContentRepository(this ContainerBuilder builder)
        {
            builder.RegisterType<PublishedContentRepository>().As<IPublishedContentRepository>().InstancePerRequest();
            return builder;
        }

        #region | Private Methods |

        /// <summary>
        /// Registers bindings for the key Umbraco contexts used in the application code.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder, for fluent bindings.</returns>
        private static ContainerBuilder RegisterContexts(this ContainerBuilder builder)
        {
            builder.Register(context => UmbracoContext.Current).As<UmbracoContext>().InstancePerRequest();
            builder.Register(context => new UmbracoHelper(UmbracoContext.Current)).As<UmbracoHelper>().InstancePerRequest();
            return builder;
        }

        /// <summary>
        /// Registers bindings for the controllers.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="controllersAssembly">The controllers assembly.</param>
        /// <returns>
        /// The builder, for fluent bindings.
        /// </returns>
        private static ContainerBuilder RegisterAllControllers(this ContainerBuilder builder, Assembly controllersAssembly = null)
        {
            builder.RegisterControllers(typeof(UmbracoApplication).Assembly);
            builder.RegisterApiControllers(typeof(UmbracoApplication).Assembly);
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);
            builder.RegisterApiControllers(typeof(DependencyRegistrar).Assembly);
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            if (controllersAssembly != null)
            {
                builder.RegisterControllers(controllersAssembly);
                builder.RegisterApiControllers(controllersAssembly);
            }
            builder.RegisterModule(new AutofacWebTypesModule());
            return builder;
        }

        /// <summary>
        /// Registers bindings for the key Umbraco services used in the application code.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder, for fluent bindings.</returns>
        private static ContainerBuilder RegisterUmbracoServices(this ContainerBuilder builder)
        {
            builder.Register(srv => ApplicationContext.Current.Services.ApplicationTreeService).As<IApplicationTreeService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.AuditService).As<IAuditService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.ConsentService).As<IConsentService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.ContentService).As<IContentService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.ContentTypeService).As<IContentTypeService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.DataTypeService).As<IDataTypeService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.DomainService).As<IDomainService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.EntityService).As<IEntityService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.ExternalLoginService).As<IExternalLoginService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.FileService).As<IFileService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.LocalizationService).As<ILocalizationService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.MacroService).As<IMacroService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.MediaService).As<IMediaService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.MemberService).As<IMemberService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.MemberGroupService).As<IMemberGroupService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.MemberTypeService).As<IMemberTypeService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.MigrationEntryService).As<IMigrationEntryService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.NotificationService).As<INotificationService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.PackagingService).As<IPackagingService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.PublicAccessService).As<IPublicAccessService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.RedirectUrlService).As<IRedirectUrlService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.RelationService).As<IRelationService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.SectionService).As<ISectionService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.ServerRegistrationService).As<IServerRegistrationService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.TagService).As<ITagService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.TaskService).As<ITaskService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.TextService).As<ILocalizedTextService>().InstancePerRequest();
            builder.Register(srv => ApplicationContext.Current.Services.UserService).As<IUserService>().InstancePerRequest();
            return builder;
        }

        /// <summary>
        /// Registers bindings for the provided Umbraco pylon implementations.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder, for fluent bindings.</returns>
        private static ContainerBuilder RegisterPylon(this ContainerBuilder builder)
        {
            builder.RegisterType<FormsAuthWrapper>().As<IFormsAuthWrapper>();
            builder.RegisterType<ViewStringRenderer>().As<IViewStringRenderer>();
            return builder;
        }

        #endregion
    }
}
