using Umbraco.Pylon.Sample.Repositories;

namespace Umbraco.Pylon.Sample.Views
{
    /// <summary>
    /// An abstract base class for views.
    /// </summary>
    public abstract class SampleViewPage : UmbracoPylonViewPage<SampleSite, SamplePublishedContentRepository>
    { }

    /// <summary>
    /// An abstract base class for views.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public abstract class SampleViewPage<TModel> : UmbracoPylonViewPage<SampleSite, SamplePublishedContentRepository, TModel>
    { }
}