using Microsoft.Data.SqlClient;
using GameWorld.Resources.Utils;
using GameWorld.Models;

namespace GameWorld.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly string connectionString = DatabaseHelper.GetDatabaseFilePath();

        public async Task<List<Item>> GetAllItemsAsync()
        {
            List<Item> items = new List<Item>();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(Apis.ITEMS_BASE_URL);
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    List<Item>? items = JsonConvert.DeserializeObject<List<Item>>(apiResponse);
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
            }
            return items;
        }

        public async Task<Item> GetItemByIdAsync(Guid itemId)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{Apis.ITEMS_BASE_URL}/{itemId}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    <Item> item = JsonConvert.DeserializeObject<Item>(apiResponse);
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
        }

        public async Task<Item> GetItemByTypeAsync(ItemType itemType)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{Apis.ITEMS_BASE_URL}/{itemType}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    <Item> item = JsonConvert.DeserializeObject<Item>(apiResponse);
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
        }

        public async Task CreateItemAsync(Item item)
        {
            using (var httpClient = new HttpClient())
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
        }

        public async Task UpdateItemAsync(Item item)
        {
            using (var httpClient = new HttpClient())
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
        }

        public async Task DeleteItemAsync(Guid itemId)
        {
            using (var httpClient = new HttpClient())
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
}
