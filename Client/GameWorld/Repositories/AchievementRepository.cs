using System.Net.Http;
using System.Net.Http.Json;
using GameWorld.Models;
using GameWorld.Resources.Utils;
using Newtonsoft.Json;

namespace GameWorld.Repositories
{
    public class AchievementRepository : IAchievementRepository
    {
        public async Task<Achievement> GetAchievementByIdAsync(Guid achievementId)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{Apis.ACHIEVEMENTS_BASE_URL}/{achievementId}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var achievement = JsonConvert.DeserializeObject<Achievement>(apiResponse);
                    return achievement;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"No achievement with id {achievementId} found");
                    return null;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
                }
            }
        }

        public async Task<List<Achievement>> GetAllAchievementsAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(Apis.ACHIEVEMENTS_BASE_URL);
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    List<Achievement>? achievements = JsonConvert.DeserializeObject<List<Achievement>>(apiResponse);
                    return achievements;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No achievements found");
                    return null;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
                }
            }
        }
        public async Task AddAchievementAsync(Achievement achievement)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(Apis.ACHIEVEMENTS_BASE_URL, JsonContent.Create(achievement));
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Achievement added successfully.");
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
                }
            }
        }

        public async Task DeleteAchievementAsync(Guid achievementId)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync($"{Apis.ACHIEVEMENTS_BASE_URL}/{achievementId}");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Achievement deleted successfully.");
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
                }
            }
        }

        public async Task UpdateAchievementAsync(Achievement achievement)
        {
            using (var httpClient = new HttpClient())
            {
                string jsonSerialized = JsonConvert.SerializeObject(achievement);
                var content = JsonContent.Create(jsonSerialized);
                string endpoint = $"{Apis.ACHIEVEMENTS_BASE_URL}/{achievement}";

                var response = await httpClient.PutAsync(endpoint, content);
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
}
