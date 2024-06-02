using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;
using Newtonsoft.Json;
using System.Text;
namespace GameWorldClassLibrary.Repositories
{
    public class MarketBuyItemRepositoryHttp : IMarketBuyItemRepository
    {
        private HttpClient httpClient;
        public MarketBuyItemRepositoryHttp(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<MarketBuyItem>> GetAllMarketBuyItemsAsync()
        {
            try
            {
                var response = await httpClient.GetAsync($"{Apis.MARKET_BUY_ITEM}");
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();
                var resources = JsonConvert.DeserializeObject<List<MarketBuyItem>>(responseContent) ?? throw new Exception("Response content from getting all market bought item from the backend is invalid: ");
                return resources;
            }
            catch (Exception exception)
            {
                throw new Exception("Error on getting all market items from the Server: " + exception.Message);
            }
        }

        public async Task<MarketBuyItem> GetMarketBuyItemByItemIdAsync(Guid itemId)
        {
            try
            {
                var response = await httpClient.GetAsync($"{Apis.MARKET_BUY_ITEM}/{itemId}");
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var resources = JsonConvert.DeserializeObject<MarketBuyItem>(responseContent) ?? throw new Exception("Response content from getting market by ID from the backend is invalid: ");
                return resources;
            }
            catch (Exception exception)
            {
                throw new Exception("Error on getting market item bought by ID from the Server: " + exception.Message);
            }
        }

        public async Task AddMarketBuyItemAsync(MarketBuyItem marketBuyItem)
        {
            try
            {
                var sentContent = JsonConvert.SerializeObject(marketBuyItem);
                var content = new StringContent(sentContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{Apis.MARKET_BUY_ITEM}", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception exception)
            {
                throw new Exception("Error on sending bought market item to the Server: " + exception.Message);
            }
        }

        public async Task UpdateMarketBuyItemAsync(MarketBuyItem marketBuyItem)
        {
            try
            {
                var putContent = JsonConvert.SerializeObject(marketBuyItem);
                var content = new StringContent(putContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync($"{Apis.MARKET_BUY_ITEM}/{marketBuyItem.Id}", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception exception)
            {
                throw new Exception("Error on putting market bought item to the Server: " + exception.Message);
            }
        }

        public async Task DeleteMarketBuyItemAsync(Guid marketBuyItemId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"{Apis.MARKET_BUY_ITEM}/{marketBuyItemId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception exception)
            {
                throw new Exception("Error on DELETE market bought item to the Server: " + exception.Message);
            }
        }
    }
}
