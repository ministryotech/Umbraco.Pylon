using Umbraco.Pylon.Sample.Repositories;

namespace Umbraco.Pylon.Sample
{
    /// <summary>
    /// Elements for the site.
    /// </summary>
    public class SampleSite : UmbracoSite<ISamplePublishedContentRepository>
    {
        public SampleSite(ISamplePublishedContentRepository contentRepo)
            : base(contentRepo)
        { }

    }
}