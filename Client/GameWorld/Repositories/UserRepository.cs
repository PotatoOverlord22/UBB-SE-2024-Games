using System.Net.Http;
using System.Net.Http.Json;
using GameWorldClassLibrary.Models;
using GameWorld.Resources.Utils;
using Newtonsoft.Json;
using GameWorldClassLibrary.Repositories.Interfaces;

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

        public async Task UpdateUserChipsAsync(Guid userId, int chips)
        {
            var content = JsonContent.Create(chips);

            HttpResponseMessage response;
            try
            {
                response = await httpClient.PutAsync($"{Apis.USERS_BASE_URL}/{userId}/chips", content);
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Request error: " + e.Message, e);
            }

            // Check the response
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("User chips updated successfully.");
            }
            else
            {
                // Optionally read the response content for more detailed error messages
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}, {responseContent}");
            }
        }

        public async Task UpdateUserStreak(Guid id, int streak)
        {
            User user = await GetUserByIdAsync(id);
            user.UserStreak = streak;
            await UpdateUserAsync(user);
        }

        public async Task UpdateUserLastLogin(Guid id, DateTime lastLogin)
        {
            User user = await GetUserByIdAsync(id);
            user.UserLastLogin = lastLogin;
            await UpdateUserAsync(user);
        }

        public async Task UpdateUserStack(Guid id, int stack)
        {
            User user = await GetUserByIdAsync(id);
            user.UserStack = stack;
            await UpdateUserAsync(user);
        }

        public async Task<List<User>> GetPokerLeaderboard()
        {
            var response = await httpClient.GetAsync(Apis.POKER_LEADERBOARD_URL);
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<User>? leaderboard = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                return leaderboard;
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
    }
}
