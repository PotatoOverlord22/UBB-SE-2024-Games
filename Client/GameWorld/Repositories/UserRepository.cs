using System.Net.Http;
using System.Net.Http.Json;
using GameWorld.Models;
using GameWorld.Resources.Utils;
using GameWorld.Services;
using Newtonsoft.Json;

namespace GameWorld.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HttpClient httpClient;

        public UserRepository()
        {
            this.httpClient = new HttpClient();
        }

        public async Task AddUserAsync(User user)
        {
            var response = await httpClient.PostAsync(Apis.USERS_BASE_URL, JsonContent.Create(user));
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("User added successfully.");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task DeleteUserByIdAsync(Guid userId)
        {
            var response = await httpClient.DeleteAsync($"{Apis.USERS_BASE_URL}/{userId}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("User deleted successfully.");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var response = await httpClient.GetAsync(Apis.USERS_BASE_URL);
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<User>? users = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                return users;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No users found");
                return new List<User>();
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            var response = await httpClient.GetAsync($"{Apis.USERS_BASE_URL}/{userId}");
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var user = JsonConvert.DeserializeObject<User>(apiResponse);
                return user;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception($"No User with id {userId} found");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            string jsonSerialized = JsonConvert.SerializeObject(user);
            var content = JsonContent.Create(jsonSerialized);
            string endpoint = $"{Apis.USERS_BASE_URL}/{user.Id}";

            var response = await httpClient.PutAsync(endpoint, content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("User updated successfully.");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }
    }
}
