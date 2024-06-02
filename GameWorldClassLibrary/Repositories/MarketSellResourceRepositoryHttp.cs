using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;
using Newtonsoft.Json;
using System.Text;

namespace GameWorldClassLibrary.Repositories
{
    public class MarketSellResourceRepositoryHttp : IMarketSellResourceRepository
    {
        private HttpClient httpClient;
        public MarketSellResourceRepositoryHttp(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<MarketSellResource>> GetAllSellResourcesAsync()
        {
            try
            {
                var response = await httpClient.GetAsync($"{Apis.MARKET_SELL_RESOURCE}");
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();
                var resources = JsonConvert.DeserializeObject<List<MarketSellResource>>(responseContent) ?? throw new Exception("Response content from getting all market sell resources from the backend is invalid: ");
                return resources;
            }
            catch (Exception exception)
            {
                throw new Exception("Error on getting all market sell resources from the Server: " + exception.Message);
            }
        }

        public async Task<MarketSellResource> GetMarketSellResourceByResourceIdAsync(Guid resourceId)
        {
            try
            {
                var response = await httpClient.GetAsync($"{Apis.MARKET_SELL_RESOURCE}/{resourceId}");
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var resources = JsonConvert.DeserializeObject<MarketSellResource>(responseContent) ?? throw new Exception("Response content from getting market sell resource by ID from the backend is invalid: ");
                return resources;
            }
            catch (Exception exception)
            {
                throw new Exception("Error on getting market sell resource by ID from the Server: " + exception.Message);
            }
        }

        public async Task AddMarketSellResourceAsync(MarketSellResource marketSellResource)
        {
            try
            {
                var sentContent = JsonConvert.SerializeObject(marketSellResource);
                var content = new StringContent(sentContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{Apis.MARKET_SELL_RESOURCE}", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception exception)
            {
                throw new Exception("Error on sending market sell resource to the Server: " + exception.Message);
            }
        }

        public async Task UpdateMarketSellResourceAsync(MarketSellResource marketSellResource)
        {
            try
            {
                var putContent = JsonConvert.SerializeObject(marketSellResource);
                var content = new StringContent(putContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync($"{Apis.MARKET_SELL_RESOURCE}/{marketSellResource.Id}", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception exception)
            {
                throw new Exception("Error on putting market sell resource to the Server: " + exception.Message);
            }
        }

        public async Task DeleteMarketSellResourceAsync(Guid marketSellResourceId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"{Apis.MARKET_SELL_RESOURCE}/{marketSellResourceId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception exception)
            {
                throw new Exception("Error on DELETE market sell resource to the Server: " + exception.Message);
            }
        }
    }
}
