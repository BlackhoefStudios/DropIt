using BlackhoefStudios.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace BlackhoefStudios.Common.Services
{
    /// <summary>
    /// Provides base methods for accessing a remote REST based API service.
    /// </summary>
    public abstract class BaseApiService
    {
        private const string ApiPrefix = "api";

        private string Host { get; set; }
        private string Controller { get; set; }

        //object used for authenticating requests
        private IApiAuthenticator Authenticator { get; set; }

        /// <summary>
        /// Creates a new HttpClient for making REST calls.
        /// </summary>
        /// <returns>A new HttpClient instance that is authenticated using the class supplied authenticator.</returns>
        private async Task<HttpClient> CreateClientAsync()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(string.Format("{0}/{1}/{2}", 
                Host, ApiPrefix, Controller));

            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            await AuthenticateAsync(client);

            return client;
        }

        /// <summary>
        /// Authenticates a request using a specified authenticator.
        /// This method only uses the Token Bearer authentication method.
        /// </summary>
        /// <param name="client">The client you want to authenticate.</param>
        /// <returns>A task that represents the authenticators asynchronous operations.</returns>
        private async Task AuthenticateAsync(HttpClient client)
        {
            //get the token used for authenticating a request.
            var authToken = await Authenticator.GetToken();

            //add the authentication to the request.
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", "Bearer " + authToken);
        }

        /// <summary>
        /// Converts a REST response from a message to an ApiResult wrapper object.
        /// If an error occurred, it contains the response message and code.
        /// </summary>
        /// <param name="response">The response to inspect and parse.</param>
        /// <returns>A result that contains the status and code of the specified response.</returns>
        private ApiResult HandleResponse(HttpResponseMessage response)
        {
            ApiResult result = new ApiResult();
            result.Message = response.ReasonPhrase;
            result.ResponseCode = (int)response.StatusCode;
            return result;
        }

        /// <summary>
        /// Converts a REST response from a message to an ApiResult wrapper object.
        /// If an error occurred, it contains the response message and code. Otherwise, it will
        /// contain the JSON parsed return value from the server.
        /// </summary>
        /// <typeparam name="T">The return type expected from the server.</typeparam>
        /// <param name="response">The response to parse and inspect.</param>
        /// <returns>A Task that represents the parsing of the response.</returns>
        private async Task<ApiResult<T>> HandleResponseAsync<T>(HttpResponseMessage response) where T : class
        {
            ApiResult<T> result = new ApiResult<T>();

            if (response.IsSuccessStatusCode)
            {
                //success
                result.Message = string.Empty;
                result.ResponseCode = (int) response.StatusCode;

                var jsonData = await response.Content.ReadAsStringAsync();

                result.Data = await Task.Factory.StartNew(() =>
                {
                    return JsonConvert.DeserializeObject<T>(jsonData);
                });
            }
            else
            {
                //error
                result.Message = response.ReasonPhrase;
                result.ResponseCode = (int) response.StatusCode;
            }
            return result;
        }

        /// <summary>
        /// Provides a way for accessing REST api's in a simple and cross-platform way.
        /// </summary>
        /// <param name="auth">The object to use for authenticating a request.</param>
        /// <param name="host">The server host that the service will be calling, such as http://microsoft.com.</param>
        /// <param name="controller">The controller name being accessed. This is excluding the /api/ prefix.</param>
        protected BaseApiService(IApiAuthenticator auth, string host, string controller)
        {
            Host = host.EndsWith("/") ? host.Substring(host.Length - 1, 1) : host;
            Controller = controller.Replace("/", string.Empty);
            Authenticator = auth;
        }

        /// <summary>
        /// Performs a GET request and returns data from the server.
        /// </summary>
        /// <typeparam name="T">The data type expected from the server.</typeparam>
        /// <param name="action">The action name to call for the GET request.</param>
        /// <returns>The return data wrapped in a standard ApiResult object.</returns>
        public async Task<ApiResult<T>> GetAsync<T>(string action)
            where T : class
        {
            action = action.Replace('/', '\0');
            using(var client = await CreateClientAsync())
            {
                var response = await client.GetAsync(action);
                var returnData = await HandleResponseAsync<T>(response);
                return returnData;
            }
        }

        /// <summary>
        /// Performs a GET request for a specific id and returns data from the server.
        /// </summary>
        /// <typeparam name="T">The data type expected from the server.</typeparam>
        /// <param name="action">The action name to call for the GET request.</param>
        /// <param name="id">The id parameter to append to the action url (like /api/controller/myaction/123</param>
        /// <returns>The return data wrapped in a standard ApiResult object.</returns>
        public async Task<ApiResult<T>> GetAsync<T>(string action, string id)
            where T : class
        {
            action =  string.Format("{0}/{1}", action.Replace('/', '\0'), id);
            using (var client = await CreateClientAsync())
            {
                var response = await client.GetAsync(action);
                var returnData = await HandleResponseAsync<T>(response);
                return returnData;
            }
        }

        /// <summary>
        /// Performs a POST (create) request and returns data from the server.
        /// </summary>
        /// <typeparam name="T">The data type expected from the server.</typeparam>
        /// <param name="action">The action name to call for the POST request.</param>
        /// <param name="dataToSend">The data to send to the server in the body of the request as JSON.</param>
        /// <returns>The return data wrapped in a standard ApiResult object.</returns>
        public async Task<ApiResult<T>> PostAsync<T>(string action, object dataToSend)
            where T : class
        {
            action = action.Replace('/', '\0');
            using (var client = await CreateClientAsync())
            {
                var response = await client.PostAsJsonAsync(action, dataToSend);
                var returnData = await HandleResponseAsync<T>(response);
                return returnData;
            }
        }

        /// <summary>
        /// Performs a PUT (update) request and returns data from the server.
        /// </summary>
        /// <typeparam name="T">The data type expected from the server.</typeparam>
        /// <param name="action">The action name to call for the PUT request.</param>
        /// <param name="dataToSend">The data to send to the server in the body of the request as JSON.</param>
        /// <returns>The return data wrapped in a standard ApiResult object.</returns>
        public async Task<ApiResult<T>> PutAsync<T>(string action, object dataToSend)
            where T : class
        {
            action = action.Replace('/', '\0');
            using (var client = await CreateClientAsync())
            {
                var response = await client.PutAsJsonAsync(action, dataToSend);
                var returnData = await HandleResponseAsync<T>(response);
                return returnData;
            }
        }

        /// <summary>
        /// Performs a DELETE request for a specific id.
        /// </summary>
        /// <param name="action">The action name to call for the DELETE request.</param>
        /// <param name="id">The id parameter to append to the action url (like /api/controller/myaction/123</param>
        /// <returns>The result wrapped in a standard ApiResult object.</returns>
        public async Task<ApiResult> DeleteAsync(string action, string id)
        {
            action = string.Format("{0}/{1}", action.Replace('/', '\0'), id);
            using (var client = await CreateClientAsync())
            {
                var response = await client.DeleteAsync(action);
                var returnData = HandleResponse(response);
                return returnData;
            }
        }
    }
}
