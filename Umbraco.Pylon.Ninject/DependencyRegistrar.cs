using System.Reflection;
using System.Web.Mvc;
using Ninject;
using Ninject.Web.Common;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace Umbraco.Pylon.Ninject
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
        /// Binds a custom content repository.
        /// </summary>
        /// <typeparam name="TRepoInterface">The type of the repo interface.</typeparam>
        /// <typeparam name="TSiteContentRepository">The type of the site content repository.</typeparam>
        /// <param name="kernel">The kernel.</param>
        /// <returns>
        /// The kernel, for fluent bindings.
        /// </returns>
        /// <remarks>
        /// Use this binding if you implement your own content repository (recommended).
        /// </remarks>
        public static IKernel BindCustomContentRepository<TRepoInterface, TSiteContentRepository>(this IKernel kernel)
            where TRepoInterface : IPublishedContentRepository
            where TSiteContentRepository : PublishedContentRepository, TRepoInterface, new()
        {
            kernel.Bind<TRepoInterface>().ToMethod(repo
                => new TSiteContentRepository
                {
                    Umbraco = DependencyResolver.Current.GetService<UmbracoHelper>(),
                    Context = DependencyResolver.Current.GetService<UmbracoContext>()
                }).InRequestScope();

            return kernel;
        }

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
            kernel.Bind<IPublishedContentRepository>().ToConstructor(ctorArgs
                => new PublishedContentRepository(ctorArgs.Inject<UmbracoHelper>())).InRequestScope();
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
            kernel.Bind<IApplicationTreeService>().ToMethod(srv => ApplicationContext.Current.Services.ApplicationTreeService).InRequestScope();
            kernel.Bind<IAuditService>().ToMethod(srv => ApplicationContext.Current.Services.AuditService).InRequestScope();
            kernel.Bind<IConsentService>().ToMethod(srv => ApplicationContext.Current.Services.ConsentService).InRequestScope();
            kernel.Bind<IContentService>().ToMethod(srv => ApplicationContext.Current.Services.ContentService).InRequestScope();
            kernel.Bind<IContentTypeService>().ToMethod(srv => ApplicationContext.Current.Services.ContentTypeService).InRequestScope();
            kernel.Bind<IDataTypeService>().ToMethod(srv => ApplicationContext.Current.Services.DataTypeService).InRequestScope();
            kernel.Bind<IDomainService>().ToMethod(srv => ApplicationContext.Current.Services.DomainService).InRequestScope();
            kernel.Bind<IEntityService>().ToMethod(srv => ApplicationContext.Current.Services.EntityService).InRequestScope();
            kernel.Bind<IExternalLoginService>().ToMethod(srv => ApplicationContext.Current.Services.ExternalLoginService).InRequestScope();
            kernel.Bind<IFileService>().ToMethod(srv => ApplicationContext.Current.Services.FileService).InRequestScope();
            kernel.Bind<ILocalizationService>().ToMethod(srv => ApplicationContext.Current.Services.LocalizationService).InRequestScope();
            kernel.Bind<IMacroService>().ToMethod(srv => ApplicationContext.Current.Services.MacroService).InRequestScope();
            kernel.Bind<IMediaService>().ToMethod(srv => ApplicationContext.Current.Services.MediaService).InRequestScope();
            kernel.Bind<IMemberService>().ToMethod(srv => ApplicationContext.Current.Services.MemberService).InRequestScope();
            kernel.Bind<IMemberGroupService>().ToMethod(srv => ApplicationContext.Current.Services.MemberGroupService).InRequestScope();
            kernel.Bind<IMemberTypeService>().ToMethod(srv => ApplicationContext.Current.Services.MemberTypeService).InRequestScope();
            kernel.Bind<IMigrationEntryService>().ToMethod(srv => ApplicationContext.Current.Services.MigrationEntryService).InRequestScope();
            kernel.Bind<INotificationService>().ToMethod(srv => ApplicationContext.Current.Services.NotificationService).InRequestScope();
            kernel.Bind<IPackagingService>().ToMethod(srv => ApplicationContext.Current.Services.PackagingService).InRequestScope();
            kernel.Bind<IPublicAccessService>().ToMethod(srv => ApplicationContext.Current.Services.PublicAccessService).InRequestScope();
            kernel.Bind<IRedirectUrlService>().ToMethod(srv => ApplicationContext.Current.Services.RedirectUrlService).InRequestScope();
            kernel.Bind<IRelationService>().ToMethod(srv => ApplicationContext.Current.Services.RelationService).InRequestScope();
            kernel.Bind<ISectionService>().ToMethod(srv => ApplicationContext.Current.Services.SectionService).InRequestScope();
            kernel.Bind<IServerRegistrationService>().ToMethod(srv => ApplicationContext.Current.Services.ServerRegistrationService).InRequestScope();
            kernel.Bind<ITagService>().ToMethod(srv => ApplicationContext.Current.Services.TagService).InRequestScope();
            kernel.Bind<ITaskService>().ToMethod(srv => ApplicationContext.Current.Services.TaskService).InRequestScope();
            kernel.Bind<ILocalizedTextService>().ToMethod(srv => ApplicationContext.Current.Services.TextService).InRequestScope();
            kernel.Bind<IUserService>().ToMethod(srv => ApplicationContext.Current.Services.UserService).InRequestScope();
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
