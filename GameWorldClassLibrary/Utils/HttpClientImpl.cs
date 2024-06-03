using System.Net.Http.Json;

namespace GameWorldClassLibrary.Utils
{
    public class HttpClientImpl : IRequestClient
    {
        private readonly HttpClient httpClient;

        public HttpClientImpl()
        {
            this.httpClient = new HttpClient();
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return httpClient.GetAsync(requestUri);
        }

        public Task<HttpResponseMessage> PostAsync<T>(string requestUri, T content)
        {
            return httpClient.PostAsJsonAsync(requestUri, content);
        }

        public Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return httpClient.DeleteAsync(requestUri);
        }

        public Task<HttpResponseMessage> PutAsync<T>(string requestUri, T content)
        {
            return httpClient.PutAsJsonAsync(requestUri, content);
        }
    }
}
