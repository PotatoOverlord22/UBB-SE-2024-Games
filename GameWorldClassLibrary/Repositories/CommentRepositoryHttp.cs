using System.Net.Http.Json;
using GameWorldClassLibrary.Models;
using Newtonsoft.Json;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Repositories
{
    public class CommentRepositoryHttp : ICommentRepository
    {
        private HttpClient httpClient;
        public CommentRepositoryHttp(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task AddCommentAsync(Comment comment)
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

        public async Task<List<Comment>> GetUserCommentsAsync(Guid userId)
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

        public async Task UpdateCommentAsync(Comment comment)
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

        public async Task DeleteCommentAsync(Guid commentId)
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

        public async Task<Comment> GetCommentByIdAsync(Guid id)
        {
            var response = await httpClient.GetAsync($"{Apis.COMMENTS_BASE_URL}/{id}");
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Comment? comment = JsonConvert.DeserializeObject<Comment>(apiResponse);
                return comment ?? throw new KeyNotFoundException("Comment not found");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException("Comment not found");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task<List<Comment>> GetCommentsAsync()
        {
            var response = await httpClient.GetAsync(Apis.COMMENTS_BASE_URL);
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<Comment>? comments = JsonConvert.DeserializeObject<List<Comment>>(apiResponse);
                return comments ?? new List<Comment>();
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }
    }
}
