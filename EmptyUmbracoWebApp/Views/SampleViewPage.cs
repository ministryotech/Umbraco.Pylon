using System.Web.Mvc;
using EmptyUmbracoWebApp.Repositories;
using Umbraco.Pylon;

namespace EmptyUmbracoWebApp.Views
{
    /// <summary>
    /// An abstract base class for Sample views.
    /// </summary>
    public abstract class SampleViewPage : PylonViewPage<ISampleSite, ISamplePublishedContentRepository>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleViewPage"/> class.
        /// </summary>
        protected SampleViewPage()
            : base(DependencyResolver.Current.GetService<ISampleSite>())
        { }

        #endregion
    }

    /// <summary>
    /// An abstract base class for Sample views.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public abstract class SampleViewPage<TModel> : PylonViewPage<ISampleSite, ISamplePublishedContentRepository, TModel>
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleViewPage"/> class.
        /// </summary>
        protected SampleViewPage()
            : base(DependencyResolver.Current.GetService<ISampleSite>())
        { }

        #endregion
    }
}
