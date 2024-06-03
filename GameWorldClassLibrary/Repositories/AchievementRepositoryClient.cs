using GameWorldClassLibrary.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Repositories
{
    public class AchievementRepositoryClient : IAchievementRepository
    {
        private IRequestClient requestClient;
        public AchievementRepositoryClient(IRequestClient requestClient)
        {
            this.requestClient = requestClient;
        }
        public async Task<Achievement> GetAchievementByIdAsync(Guid achievementId)
        {
            var response = await requestClient.GetAsync($"{Apis.ACHIEVEMENTS_BASE_URL}/{achievementId}");
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var achievement = JsonConvert.DeserializeObject<Achievement>(apiResponse);
                return achievement;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception($"No achievement with id {achievementId} found");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task<List<Achievement>> GetAllAchievementsAsync()
        {
            var response = await requestClient.GetAsync(Apis.ACHIEVEMENTS_BASE_URL);
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<Achievement>? achievements = JsonConvert.DeserializeObject<List<Achievement>>(apiResponse);
                return achievements;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No achievements found");
                return new List<Achievement>();
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }
        public async Task AddAchievementAsync(Achievement achievement)
        {
            var response = await requestClient.PostAsync(Apis.ACHIEVEMENTS_BASE_URL, JsonContent.Create(achievement));
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Achievement added successfully.");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task DeleteAchievementAsync(Guid achievementId)
        {
            var response = await requestClient.DeleteAsync($"{Apis.ACHIEVEMENTS_BASE_URL}/{achievementId}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Achievement deleted successfully.");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task UpdateAchievementAsync(Achievement achievement)
        {
            string jsonSerialized = JsonConvert.SerializeObject(achievement);
            var content = JsonContent.Create(jsonSerialized);
            string endpoint = $"{Apis.ACHIEVEMENTS_BASE_URL}/{achievement.Id}";

            var response = await requestClient.PutAsync(endpoint, content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Achievement updated successfully.");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }
    }
}
