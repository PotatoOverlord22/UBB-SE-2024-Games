using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace GameWorldClassLibrary.Repositories
{
    public class ItemRepositoryHttp : IItemRepository
    {
        private HttpClient httpClient;
        public ItemRepositoryHttp(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<Item>> GetAllItemsAsync()
        {
            List<Item> items = new List<Item>();

            var response = await httpClient.GetAsync(Apis.ITEMS_BASE_URL);
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                items = JsonConvert.DeserializeObject<List<Item>>(apiResponse);
                return items;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No items found");
                return null;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
            return items;
        }

        public async Task<Item> GetItemByIdAsync(Guid itemId)
        {
            var response = await httpClient.GetAsync($"{Apis.ITEMS_BASE_URL}/{itemId}");
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Item item = JsonConvert.DeserializeObject<Item>(apiResponse);
                return item;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"No item with id: {itemId} was found");
                return null;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task<Item> GetItemByTypeAsync(ItemType itemType)
        {
            var response = await httpClient.GetAsync($"{Apis.ITEMS_BASE_URL}/{itemType}");
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Item item = JsonConvert.DeserializeObject<Item>(apiResponse);
                return item;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"No item with type: {itemType} was found");
                return null;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task CreateItemAsync(Item item)
        {
            var response = await httpClient.PostAsync(Apis.ITEMS_BASE_URL, JsonContent.Create(item));
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Item added successfully.");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task UpdateItemAsync(Item item)
        {
            string jsonSerialized = JsonConvert.SerializeObject(item);
            var content = JsonContent.Create(jsonSerialized);
            string endpoint = $"{Apis.ITEMS_BASE_URL}/{item}";

            var response = await httpClient.PutAsync(endpoint, content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Item updated successfully.");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task DeleteItemAsync(Guid itemId)
        {
            var response = await httpClient.DeleteAsync($"{Apis.ITEMS_BASE_URL}/{itemId}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Item deleted successfully.");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }
    }
}
