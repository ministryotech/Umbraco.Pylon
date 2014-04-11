using System.Web;
using Ministry.Abstracts;

namespace Umbraco.Pylon.Sample
{
    /// <summary>
    /// Wrapper for Session state
    /// </summary>
    public class WebSession : WebSessionBase
    {
        public WebSession()
            : base(new HttpContextWrapper(HttpContext.Current))
        { }
    }
}