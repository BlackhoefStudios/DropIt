using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackhoefStudios.Common.Services
{
    /// <summary>
    /// Represents a wrapper for an API call that also returns some data (other than just status messages).
    /// </summary>
    /// <typeparam name="T">The type of data returned from a server request.</typeparam>
    public class ApiResult<T> : ApiResult where T : class
    {
        /// <summary>
        /// The data returned from a server request. Examples would be a list of items, a single Guid, or String.
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    /// Provides a standard wrapper for an Api call to a server.
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// The message recieved from the request.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The response status code, for example: 404, 400, 500.
        /// </summary>
        public int ResponseCode { get; set; }
    }
}
