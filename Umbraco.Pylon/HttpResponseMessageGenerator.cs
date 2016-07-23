using System;
using System.Net;
using System.Net.Http;

namespace Umbraco.PylonLite
{
    /// <summary>
    /// Generates standard HttpResponseMessages depending on whether an item is null or not.
    /// </summary>
    public static class HttpResponseMessageGenerator
    {
        private const string NotFoundMessage = "An item was not found at this address.";

        /// <summary>
        /// Returns an HttpResponseMessage for a function.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <returns>
        /// OK, NotFound or Error as appropriate.
        /// </returns>
        public static HttpResponseMessage For(HttpRequestMessage request, Func<object> function)
        {
            try
            {
                var returnItem = function();
                return returnItem != null
                        ? request.CreateResponse(HttpStatusCode.OK, returnItem)
                        : request.CreateErrorResponse(HttpStatusCode.NotFound, NotFoundMessage);
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
