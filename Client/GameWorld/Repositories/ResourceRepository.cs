using System.Net.Http;
using System.Text;
using GameWorldClassLibrary.Models;
using GameWorld.Resources.Utils;
using Newtonsoft.Json;

namespace GameWorld.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly HttpClient httpClient;
        private readonly string base_URL;

        public ResourceRepository()
        {
            this.httpClient = new HttpClient();
            this.base_URL = Apis.RESOURCES_BASE_URL;
        }

        public async Task<List<Resource>> GetAllResourcesAsync()
        {
            try
            {
                // Send a GET request to the base URL (localhost:3000 ?)
                var response = await httpClient.GetAsync($"{base_URL}");
                // Make sure the response is good otherwise throw error
                response.EnsureSuccessStatusCode();
                // Turn the HTTPContent into a json string
                string responseContent = await response.Content.ReadAsStringAsync();
                // Turn string to List
                // Aici arunc o eroare da se poate returna si o lista goala. Cum crezi ca ii mai bine
                var resources = JsonConvert.DeserializeObject<List<Resource>>(responseContent) ?? throw new Exception("Response content from getting all resources from the backend is invalid: ");
                return resources;
            }
            catch (Exception exception)
            {
                throw new Exception("Error on getting all resources from the Server: " + exception.Message);
            }
        }

        public async Task<Resource> GetResourceByIdAsync(Guid resourceId)
        {
            try
            {
                // Aici se poate face si POST daca se considera ca nu e okay sa expui IDu in URI
                var response = await httpClient.GetAsync($"{base_URL}/{resourceId}");
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var resources = JsonConvert.DeserializeObject<Resource>(responseContent) ?? throw new Exception("Response content from getting resource by ID from the backend is invalid: ");
                return resources;
            }
            catch (Exception exception)
            {
                throw new Exception("Error on getting resource by ID from the Server: " + exception.Message);
            }
        }

        public async Task<Resource> GetResourceByTypeAsync(ResourceType resourceType)
        {
            try
            {
                Console.WriteLine($"{base_URL}/type/{(int)resourceType}");
                var response = await httpClient.GetAsync($"{base_URL}/type/{resourceType}");
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var resources = JsonConvert.DeserializeObject<Resource>(responseContent) ?? throw new Exception("Response content from getting resource by Type from the backend is invalid: ");
                return resources;
            }
            catch (Exception exception)
            {
                throw new Exception("Error on getting resource by Type from the Server: " + exception.Message);
            }
        }

        public async Task AddResourceAsync(Resource resource)
        {
            try
            {
                var sentContent = JsonConvert.SerializeObject(resource);
                var content = new StringContent(sentContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{base_URL}", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception exception)
            {
                throw new Exception("Error on sending resource to the Server: " + exception.Message);
            }
        }

        public async Task UpdateResourceAsync(Resource resource)
        {
            try
            {
                var putContent = JsonConvert.SerializeObject(resource);
                var content = new StringContent(putContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync($"{base_URL}/{resource.Id}", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception exception)
            {
                throw new Exception("Error on putting resource to the Server: " + exception.Message);
            }
        }

        public async Task DeleteResourceAsync(Guid resourceId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"{base_URL}/{resourceId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception exception)
            {
                throw new Exception("Error on DELETE resource to the Server: " + exception.Message);
            }
        }
    }
}
