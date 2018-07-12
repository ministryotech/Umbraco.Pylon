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
        /// <returns>The builder, for fluent bindings.</returns>
        public static ContainerBuilder RegisterUmbraco(this ContainerBuilder builder)
            => builder
                .RegisterControllers()
                .RegisterContexts()
                .RegisterUmbracoServices()
                .RegisterPylon();

        /// <summary>
        /// Registers bindings for a custom content repository.
        /// </summary>
        /// <remarks>
        /// Use this binding if you implement your own content repository (recommended).
        /// </remarks>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder, for fluent bindings.</returns>
        public static ContainerBuilder RegisterCustomContentRepository<TSiteContentRepository>(this ContainerBuilder builder)
            where TSiteContentRepository : IPublishedContentRepository, new()
        {
            builder.Register(repo => new TSiteContentRepository
            {
                Umbraco = DependencyResolver.Current.GetService<UmbracoHelper>()
            }).As<IPublishedContentRepository>().InstancePerHttpRequest();

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
            builder.RegisterType<PublishedContentRepository>().As<IPublishedContentRepository>().InstancePerHttpRequest();
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
            builder.Register(context => UmbracoContext.Current).As<UmbracoContext>().InstancePerHttpRequest();
            builder.Register(context => new UmbracoHelper(UmbracoContext.Current)).As<UmbracoHelper>().InstancePerHttpRequest();
            return builder;
        }

        /// <summary>
        /// Registers bindings for the controllers.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder, for fluent bindings.</returns>
        private static ContainerBuilder RegisterControllers(this ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(UmbracoApplication).Assembly);
            builder.RegisterApiControllers(typeof(UmbracoApplication).Assembly);
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);
            builder.RegisterApiControllers(typeof(DependencyRegistrar).Assembly);
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
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
            builder.Register(srv => ApplicationContext.Current.Services.ApplicationTreeService).As<IApplicationTreeService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.AuditService).As<IAuditService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.ConsentService).As<IConsentService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.ContentService).As<IContentService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.ContentTypeService).As<IContentTypeService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.DataTypeService).As<IDataTypeService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.DomainService).As<IDomainService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.EntityService).As<IEntityService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.ExternalLoginService).As<IExternalLoginService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.FileService).As<IFileService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.LocalizationService).As<ILocalizationService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.MacroService).As<IMacroService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.MediaService).As<IMediaService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.MemberService).As<IMemberService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.MemberGroupService).As<IMemberGroupService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.MemberTypeService).As<IMemberTypeService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.MigrationEntryService).As<IMigrationEntryService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.NotificationService).As<INotificationService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.PackagingService).As<IPackagingService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.PublicAccessService).As<IPublicAccessService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.RedirectUrlService).As<IRedirectUrlService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.RelationService).As<IRelationService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.SectionService).As<ISectionService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.ServerRegistrationService).As<IServerRegistrationService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.TagService).As<ITagService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.TaskService).As<ITaskService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.TextService).As<ILocalizedTextService>().InstancePerHttpRequest();
            builder.Register(srv => ApplicationContext.Current.Services.UserService).As<IUserService>().InstancePerHttpRequest();
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
