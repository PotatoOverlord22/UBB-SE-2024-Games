using System.Data;
using System.Net.Http.Json;
using System.Net.Http;
using GameWorldClassLibrary.Models;
using GameWorld.Resources.Utils;
using Newtonsoft.Json;

namespace GameWorld.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        public async Task CreateCommentAsync(Comment comment)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(Apis.COMMENTS_BASE_URL, JsonContent.Create(comment));
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Comment added successfully.");
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
                }
            }
        }

        public async Task<List<Comment>> GetUserCommentsAsync(Guid userId)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{Apis.COMMENTS_BASE_URL}?userId={userId}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    List<Comment>? comments = JsonConvert.DeserializeObject<List<Comment>>(apiResponse);
                    return comments;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No comments found for the user");
                    return null;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
                }
            }
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            using (var httpClient = new HttpClient())
            {
                string jsonSerialized = JsonConvert.SerializeObject(comment);
                var content = JsonContent.Create(jsonSerialized);
                string endpoint = $"{Apis.COMMENTS_BASE_URL}/{comment}";

                var response = await httpClient.PutAsync(endpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Comment updated successfully.");
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
                }
            }
        }

        public async Task DeleteCommentAsync(Guid commentId)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync($"{Apis.COMMENTS_BASE_URL}/{commentId}");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Comment deleted successfully.");
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
                }
            }
        }
    }
}
