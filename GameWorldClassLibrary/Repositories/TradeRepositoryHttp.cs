using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace GameWorldClassLibrary.Repositories
{
    public class TradeRepositoryHttp : ITradeRepository
    {
        private HttpClient httpClient;
        public TradeRepositoryHttp(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<Trade>> GetAllTradesAsync()
        {
            var response = await httpClient.GetAsync(Apis.TRADES_BASE_URL);
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<Trade> trades = JsonConvert.DeserializeObject<List<Trade>>(apiResponse);
                return trades;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No trades found");
                return new List<Trade>();
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task<List<Trade>> GetAllTradesExceptCreatedByUser(Guid userId)
        {
            var response = await httpClient.GetAsync($"{Apis.TRADES_BASE_URL}/except/{userId}");
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<Trade> trades = JsonConvert.DeserializeObject<List<Trade>>(apiResponse);
                return trades;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No trades found");
                return new List<Trade>();
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task<Trade> GetTradeByIdAsync(Guid tradeId)
        {
            var response = await httpClient.GetAsync($"{Apis.TRADES_BASE_URL}/{tradeId}");
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Trade trade = JsonConvert.DeserializeObject<Trade>(apiResponse);
                return trade;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception($"No trade with id {tradeId} found");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task<Trade> GetUserTradeAsync(Guid userId)
        {
            var response = await httpClient.GetAsync($"{Apis.TRADES_BASE_URL}/user/{userId}");
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Trade trade = JsonConvert.DeserializeObject<Trade>(apiResponse);
                return trade;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception($"No trade found for user with id {userId}");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task CreateTradeAsync(Trade trade)
        {
            var response = await httpClient.PostAsync(Apis.TRADES_BASE_URL, JsonContent.Create(trade));
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Trade created successfully.");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task UpdateTradeAsync(Trade trade)
        {
            string jsonSerialized = JsonConvert.SerializeObject(trade);
            var content = JsonContent.Create(jsonSerialized);
            string endpoint = $"{Apis.TRADES_BASE_URL}/{trade.Id}";

            var response = await httpClient.PutAsync(endpoint, content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Trade updated successfully.");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task DeleteTradeAsync(Guid tradeId)
        {
            var response = await httpClient.DeleteAsync($"{Apis.TRADES_BASE_URL}/{tradeId}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Trade deleted successfully.");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }
    }
}
