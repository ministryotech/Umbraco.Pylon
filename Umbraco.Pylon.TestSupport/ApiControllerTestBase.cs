namespace Umbraco.Pylon.TestSupport
{
    /// <summary>
    /// Base test file
    /// </summary>
    /// <typeparam name="TObjUt">The type of the object under test.</typeparam>
    /// <typeparam name="TContentRepo"></typeparam>
    public abstract class ApiControllerTestBase<TObjUt, TContentRepo> : ControllerTestBase<TObjUt, TContentRepo>
        where TContentRepo : class, IPublishedContentRepository
    { }
}
