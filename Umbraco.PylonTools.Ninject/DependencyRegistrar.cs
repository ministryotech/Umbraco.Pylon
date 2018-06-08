using Ninject;
using Ninject.Web.Common;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace Umbraco.PylonTools.Ninject
{
    /// <summary>
    /// Fluent extensions / Binding methods to bind key Umbraco contexts and services.
    /// </summary>
    public static class DependencyRegistrar
    {
        /// <summary>
        /// Binds all of the dependencies to support Umbraco Pylon based apps.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <returns>The kernel, for fluent bindings.</returns>
        public static IKernel BindUmbraco(this IKernel kernel)
            => kernel
                .BindContexts()
                .BindUmbracoServices()
                .BindPylon();

        /// <summary>
        /// Binds the default content repository.
        /// </summary>
        /// <remarks>
        /// Use this binding if you don't intend to add any custom methods to your own repository implementation.
        /// We recommend that you inherit from the generic versions of the Pylon controllers so you can make use of strongly typed site content.
        /// </remarks>
        /// <param name="kernel">The kernel.</param>
        /// <returns>The kernel, for fluent bindings.</returns>
        public static IKernel BindDefaultContentRepository(this IKernel kernel)
        {
            kernel.Bind<IPublishedContentRepository>().To<PublishedContentRepository>();
            return kernel;
        }

        #region | Private Methods |

        /// <summary>
        /// Binds the key Umbraco contexts used in the application code.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <returns>The kernel, for fluent bindings.</returns>
        private static IKernel BindContexts(this IKernel kernel)
        {
            kernel.Bind<UmbracoContext>().ToMethod(context => UmbracoContext.Current).InRequestScope();
            kernel.Bind<UmbracoHelper>().ToMethod(context => new UmbracoHelper(UmbracoContext.Current)).InRequestScope();
            return kernel;
        }

        /// <summary>
        /// Binds the key Umbraco services used in the application code.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <returns>The kernel, for fluent bindings.</returns>
        private static IKernel BindUmbracoServices(this IKernel kernel)
        {
            kernel.Bind<IApplicationTreeService>().ToMethod(context => ApplicationContext.Current.Services.ApplicationTreeService).InRequestScope();
            kernel.Bind<IAuditService>().ToMethod(context => ApplicationContext.Current.Services.AuditService).InRequestScope();
            kernel.Bind<IConsentService>().ToMethod(context => ApplicationContext.Current.Services.ConsentService).InRequestScope();
            kernel.Bind<IContentService>().ToMethod(context => ApplicationContext.Current.Services.ContentService).InRequestScope();
            kernel.Bind<IContentTypeService>().ToMethod(context => ApplicationContext.Current.Services.ContentTypeService).InRequestScope();
            kernel.Bind<IDataTypeService>().ToMethod(context => ApplicationContext.Current.Services.DataTypeService).InRequestScope();
            kernel.Bind<IDomainService>().ToMethod(context => ApplicationContext.Current.Services.DomainService).InRequestScope();
            kernel.Bind<IEntityService>().ToMethod(context => ApplicationContext.Current.Services.EntityService).InRequestScope();
            kernel.Bind<IExternalLoginService>().ToMethod(context => ApplicationContext.Current.Services.ExternalLoginService).InRequestScope();
            kernel.Bind<IFileService>().ToMethod(context => ApplicationContext.Current.Services.FileService).InRequestScope();
            kernel.Bind<ILocalizationService>().ToMethod(context => ApplicationContext.Current.Services.LocalizationService).InRequestScope();
            kernel.Bind<IMacroService>().ToMethod(context => ApplicationContext.Current.Services.MacroService).InRequestScope();
            kernel.Bind<IMediaService>().ToMethod(context => ApplicationContext.Current.Services.MediaService).InRequestScope();
            kernel.Bind<IMemberService>().ToMethod(context => ApplicationContext.Current.Services.MemberService).InRequestScope();
            kernel.Bind<IMemberTypeService>().ToMethod(context => ApplicationContext.Current.Services.MemberTypeService).InRequestScope();
            kernel.Bind<IMigrationEntryService>().ToMethod(context => ApplicationContext.Current.Services.MigrationEntryService).InRequestScope();
            kernel.Bind<INotificationService>().ToMethod(context => ApplicationContext.Current.Services.NotificationService).InRequestScope();
            kernel.Bind<IPackagingService>().ToMethod(context => ApplicationContext.Current.Services.PackagingService).InRequestScope();
            kernel.Bind<IPublicAccessService>().ToMethod(context => ApplicationContext.Current.Services.PublicAccessService).InRequestScope();
            kernel.Bind<IRedirectUrlService>().ToMethod(context => ApplicationContext.Current.Services.RedirectUrlService).InRequestScope();
            kernel.Bind<IRelationService>().ToMethod(context => ApplicationContext.Current.Services.RelationService).InRequestScope();
            kernel.Bind<ISectionService>().ToMethod(context => ApplicationContext.Current.Services.SectionService).InRequestScope();
            kernel.Bind<IServerRegistrationService>().ToMethod(context => ApplicationContext.Current.Services.ServerRegistrationService).InRequestScope();
            kernel.Bind<ITagService>().ToMethod(context => ApplicationContext.Current.Services.TagService).InRequestScope();
            kernel.Bind<ITaskService>().ToMethod(context => ApplicationContext.Current.Services.TaskService).InRequestScope();
            kernel.Bind<ILocalizedTextService>().ToMethod(context => ApplicationContext.Current.Services.TextService).InRequestScope();
            kernel.Bind<IUserService>().ToMethod(context => ApplicationContext.Current.Services.UserService).InRequestScope();
            return kernel;
        }

        /// <summary>
        /// Binds the provided Umbraco pylon implementations.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <returns>The kernel, for fluent bindings.</returns>
        private static IKernel BindPylon(this IKernel kernel)
        {
            kernel.Bind<IFormsAuthWrapper>().To<FormsAuthWrapper>();
            kernel.Bind<IViewStringRenderer>().To<ViewStringRenderer>();
            return kernel;
        }

        #endregion
    }
}
