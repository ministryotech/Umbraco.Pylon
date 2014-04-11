using Umbraco.Pylon.Sample.Repositories;

namespace Umbraco.Pylon.Sample.Controllers
{
    /// <summary>
    /// Base Controller class.
    /// </summary>
    public abstract class MinistryWebControllerBase : UmbracoPylonControllerBase<ISamplePublishedContentRepository>
    {
        private ISamplePublishedContentRepository contentRepo;

        /// <summary>
        /// Gets or sets the content repository.
        /// </summary>
        public override ISamplePublishedContentRepository ContentRepo
        {
            get 
            { 
                if (contentRepo != null) 
                    return contentRepo;

                return UmbracoContext != null ? new SamplePublishedContentRepository(UmbracoContext) : new SamplePublishedContentRepository();
            }
            set { contentRepo = value; }
        }
    }
}