using System.Net.Http;
using System.Threading.Tasks;
namespace MoneyOrbit.Application.Interfaces.IServices
{
    /// <summary>
    /// Defines interaction between system
    /// and internet resources.
    /// </summary>
    public interface IHttpClientService
    {
        /// <summary>
        /// This method sends a request with the given request message.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>HttpResponseMessage</returns>
        public Task<HttpResponseMessage> Request(HttpRequestMessage request);

        /// <summary>
        /// This method posts a http request of url encoded form data
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns>HttpResponseMessage</returns>
        public Task<HttpResponseMessage> PostFormUrlEnconded(string url, StringContent content);

    }
}



    

