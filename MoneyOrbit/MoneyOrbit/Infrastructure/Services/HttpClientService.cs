using MoneyOrbit.Application.Interfaces.IServices;

namespace MoneyOrbit.Infrastructure.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient httpClient;
        private readonly string accessToken;

        public HttpClientService()
        {
            //Get our HttpClients
            httpClient = GetClient();
        }
        public async Task<HttpResponseMessage> Request(HttpRequestMessage request)
        {
            return await httpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> PostFormUrlEnconded(string url, StringContent content)
        {
            return await httpClient.PostAsync(url, content);
        }

        private HttpClient GetClient()
        {
            return new HttpClient();
        }

    }
}





    

