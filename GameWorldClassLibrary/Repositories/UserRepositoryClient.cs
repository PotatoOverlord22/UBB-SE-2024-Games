using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace GameWorldClassLibrary.Repositories
{
    public class UserRepositoryClient : IUserRepository
    {
        private readonly IRequestClient requestClient;

        public UserRepositoryClient(IRequestClient requestClient)
        {
            this.requestClient = requestClient;
        }

        public async Task AddUserAsync(User user)
        {
            var response = await requestClient.PostAsync(Apis.USERS_BASE_URL, JsonContent.Create(user));
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
            var response = await requestClient.DeleteAsync($"{Apis.USERS_BASE_URL}/{userId}");
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
            var response = await requestClient.GetAsync(Apis.USERS_BASE_URL);
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
            var response = await requestClient.GetAsync($"{Apis.USERS_BASE_URL}/{userId}");
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

            var response = await requestClient.PutAsync(endpoint, content);
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
                response = await requestClient.PutAsync($"{Apis.USERS_BASE_URL}/{userId}/chips", content);
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

        public async Task UpdateUserLastLogin(Guid id, DateTime lastLogin)
        {
            User user = await GetUserByIdAsync(id);
            user.UserLastLogin = lastLogin;
            await UpdateUserAsync(user);
        }

        public async Task<List<User>> GetPokerLeaderboard()
        {
            var response = await requestClient.GetAsync(Apis.POKER_LEADERBOARD_URL);
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

        public async Task<User> GetUserByUsername(string username)
        {
            var response = await requestClient.GetAsync($"{Apis.USERS_USERNAME_URL}/{username}");
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                User user = JsonConvert.DeserializeObject<User>(apiResponse);
                return user;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No users found");
                throw new Exception("No users found with the name " + username);
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }
    }
}
