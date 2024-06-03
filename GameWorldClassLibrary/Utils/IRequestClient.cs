namespace GameWorldClassLibrary.Utils
{
    public interface IRequestClient
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);
        Task<HttpResponseMessage> PostAsync<T>(string requestUri, T content);
        Task<HttpResponseMessage> DeleteAsync(string requestUri);
        Task<HttpResponseMessage> PutAsync<T>(string requestUri, T content);
    }
}
