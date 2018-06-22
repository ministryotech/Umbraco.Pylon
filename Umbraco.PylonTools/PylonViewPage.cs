using System;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace Umbraco.PylonTools
{
    /// <summary>
    /// A Pylon based view page using a standard IPublishedContent model and a generic site repository.
    /// </summary>
    /// <seealso cref="UmbracoViewPage" />
    /// <remarks>
    /// Use this over <see cref="UmbracoViewPage" /> to access your IPublishedContentRepository implementation.
    /// </remarks>
    public abstract class PylonViewPage : PylonViewPage<DefaultUmbracoSite, PublishedContentRepository, IPublishedContent>
    { }

    /// <summary>
    /// A Pylon based view page using a custom model and a generic site repository.
    /// </summary>
    /// <seealso cref="UmbracoViewPage" />
    /// <remarks>
    /// Use this over <see cref="UmbracoViewPage" /> to access your IPublishedContentRepository implementation.
    /// </remarks>
    public abstract class PylonViewPage<TModel> : PylonViewPage<DefaultUmbracoSite, PublishedContentRepository, TModel>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonViewPage{TUmbracoSite, TSiteContentRepository, TModel}"/> class.
        /// </summary>
        protected PylonViewPage()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            UmbracoSite = new DefaultUmbracoSite(new PublishedContentRepository(Umbraco, UmbracoContext));
        }

        #endregion
    }

    /// <summary>
    /// A Pylon based view page using a custom model and a site specific content repository.
    /// </summary>
    /// <typeparam name="TUmbracoSite">The type of the umbraco site.</typeparam>
    /// <typeparam name="TSiteContentRepository">The type of the site content repository.</typeparam>
    /// <seealso cref="UmbracoViewPage" />
    /// <remarks>
    /// Use this over <see cref="UmbracoViewPage" /> to access your IPublishedContentRepository implementation.
    /// Of the three variants available, this is the preferred usage.
    /// </remarks>
    public abstract class PylonViewPage<TUmbracoSite, TSiteContentRepository> : PylonViewPage<TUmbracoSite, TSiteContentRepository, IPublishedContent>
    where TUmbracoSite : class, IUmbracoSite<TSiteContentRepository>
    where TSiteContentRepository : class, IPublishedContentRepository
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonViewPage{TUmbracoSite, TSiteContentRepository, TModel}"/> class.
        /// </summary>
        /// <remarks>
        /// For subclass initialisation.
        /// </remarks>
        internal PylonViewPage()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonViewPage{TUmbracoSite, TSiteContentRepository, TModel}"/> class.
        /// </summary>
        /// <param name="umbracoSite">The umbraco site.</param>
        protected PylonViewPage(TUmbracoSite umbracoSite)
            : base(umbracoSite)
        { }

        #endregion
    }

    /// <summary>
    /// A Pylon based view page using a custom model and a site specific content repository.
    /// </summary>
    /// <typeparam name="TUmbracoSite">The type of the umbraco site.</typeparam>
    /// <typeparam name="TSiteContentRepository">The type of the site content repository.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="UmbracoViewPage" />
    /// <remarks>
    /// Use this over <see cref="UmbracoViewPage" /> to access your IPublishedContentRepository implementation.
    /// Of the three variants available, this is the preferred usage.
    /// </remarks>
    public abstract class PylonViewPage<TUmbracoSite, TSiteContentRepository, TModel> : UmbracoViewPage<TModel>
        where TUmbracoSite : class, IUmbracoSite<TSiteContentRepository>
        where TSiteContentRepository :class,  IPublishedContentRepository
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonViewPage{TUmbracoSite, TSiteContentRepository, TModel}"/> class.
        /// </summary>
        /// <remarks>
        /// For subclass initialisation.
        /// </remarks>
        internal PylonViewPage()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonViewPage{TUmbracoSite, TSiteContentRepository, TModel}"/> class.
        /// </summary>
        /// <param name="umbracoSite">The umbraco site.</param>
        protected PylonViewPage(TUmbracoSite umbracoSite)
        {
            UmbracoSite = umbracoSite;
        }

        #endregion

        /// <summary>
        /// Entry point for specified site content.
        /// </summary>
        /// <value>
        /// The umbraco site.
        /// </value>
        /// <remarks>
        /// If the content repository has not yet been initialised then it will be done here.
        /// </remarks>
        public TUmbracoSite UmbracoSite { get; protected set; }

        #region | Obsolete Members |

        /// <summary>
        /// Gets the dynamic model.
        /// </summary>
        [Obsolete("Pylon is now fully committed to strongly typed modelling using tools such as Umbraco Models Builder. This property now returns 'Model' wrapped in a dynamic. Use 'Model' instead.")]
        public dynamic DynamicModel => Model;

        #endregion
    }
}
