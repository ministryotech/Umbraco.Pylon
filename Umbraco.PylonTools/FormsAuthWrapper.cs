using System.Web.Security;

namespace Umbraco.PylonTools
{
    #region | Interface |

    /// <summary>
    /// Wraps basic Forms Authentication functions.
    /// </summary>
    public interface IFormsAuthWrapper
    {
        /// <summary>
        /// Sets the authentication cookie.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="createPersistentCookie">if set to <c>true</c> [create persistent cookie].</param>
        void SetAuthCookie(string username, bool createPersistentCookie);


        /// <summary>
        /// Signs out.
        /// </summary>
        void SignOut();
    }

    #endregion

    /// <summary>
    /// Wraps basic Forms Authentication functions.
    /// </summary>
    public class FormsAuthWrapper : IFormsAuthWrapper
    {
        /// <summary>
        /// Sets the authentication cookie.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="createPersistentCookie">if set to <c>true</c> [create persistent cookie].</param>
        public void SetAuthCookie(string username, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(username, createPersistentCookie);
        }


        /// <summary>
        /// Signs out.
        /// </summary>
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}