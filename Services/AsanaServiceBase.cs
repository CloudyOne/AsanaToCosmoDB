using PortableRest;
using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace AsanaToCosmoDB.Services
{
    public class AsanaServiceBase : RestClient
    {
        #region Properties
        /// <summary>
        /// This value allows the derivative classes to set the url segment that immediately follows the base url:
        /// https://api.asana.com/ + v1 + / (data API url segement)
        /// https://api.asana.com/ + v1 + /stats (stats API url segment)
        /// </summary>
        public string ServiceKey { get; private set; }

        /// <summary>
        /// The api key credential that allows remote API access
        /// </summary>
        public string ApiKey { get; private set; }

        /// <summary>
        /// The version of the API to access. This version is typically embedded into the URL of the request, 
        /// although occasionally you can specify it in the header. We support both options.
        /// </summary>
        /// <remarks>
        /// You can change the logic on the getter to automatically update the BaseUrl if the ApiVersion changes.
        /// </remarks>
        public string ApiVersion { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiKey">The api key to authorize access to the service</param>
        /// <param name="apiVersion">The api version key (ie. v1, v2)</param>
        /// <param name="serviceKey">The url segment that indicates the API service (data, stats, etc)</param>
        public AsanaServiceBase(string apiKey, string apiVersion, string serviceKey)
        {
            ServiceKey = serviceKey;
            ApiKey = apiKey;
            ApiVersion = apiVersion;

            // PCL-friendly way to get current version
            var thisAssembly = typeof(AsanaServiceBase).Assembly;
            var thisAssemblyName = new AssemblyName(thisAssembly.FullName);
            var thisVersion = thisAssemblyName.Version;

            var portableRestAssembly = typeof(RestRequest).Assembly;
            var portableRestAssemblyName = new AssemblyName(portableRestAssembly.FullName);
            var portableRestVersion = portableRestAssemblyName.Version;

            UserAgent = string.Format("Asana {0} Client for .NET {1} (PortableRest {2})", ApiVersion, thisVersion, portableRestVersion);

            BaseUrl = $"https://app.asana.com/api/{ApiVersion}/{ServiceKey}";
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceKey">The url segment that indicates the API service (data, stats, etc)</param>
        /// <param name="apiKey">The api key to authorize access to the service</param>
        public AsanaServiceBase(string serviceKey, string apiKey) : this(apiKey, "1.0", serviceKey)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiKey">The api key to authorize access to the service</param>
        public AsanaServiceBase(string apiKey) : this(apiKey, "1.0", string.Empty)
        {
        }
        #endregion

        #region Request Methods
        protected Task DeleteRequest(string path, params object[] args)
        {
            // TODO: Implement this method, as soon as PortableRest releases PUT/PUT/DELETE support
            throw new NotImplementedException();
        }

        protected Task PutRequest<T, T1>(T item, string path, params object[] args)
        {
            // TODO: Implement this method, as soon as PortableRest releases PUT/PUT/DELETE support
            throw new NotImplementedException();
        }

        protected async Task<T> PostRequest<T>(T item, string path, params object[] args) where T: class
        {
            var request = new RestRequest(string.Format(path, args), HttpMethod.Post) { ContentType = ContentTypes.Json };
            SetAuthorization(request);
            return await ExecuteAsync<T>(request);
        }

        protected async Task<T> GetRequest<T>(string path, params object[] args) where T : class
        {
            var response = default(T);
            var request = new RestRequest(string.Format(path, args), HttpMethod.Get) { ContentType = ContentTypes.Json };
            SetAuthorization(request);
            request.Headers.Add("Accept", "application/json");
            try
            {
                response = await ExecuteAsync<T>(request);
            }
            catch (Exception ex)
            {
            }
            await Task.Delay(400); // Rate Limit Protection: 150 request per minute
            return response;
        }
        #endregion

        #region Helper Methods
        protected void SetAuthorization(RestRequest request)
        {
            request.Headers.Add("Authorization", $"Bearer {ApiKey}");
        }
        #endregion
    }
}
