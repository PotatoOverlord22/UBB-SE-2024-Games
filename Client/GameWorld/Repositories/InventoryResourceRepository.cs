using System;
using System.Data;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Net.Http;
using GameWorldClassLibrary.Models;
using GameWorld.Resources.Utils;

namespace GameWorldClassLibrary.Repositories
{
    public class InventoryResourceRepository : IInventoryResourceRepository
    {
        public async Task<List<InventoryResource>> GetUserResourcesAsync(Guid userId)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync($"{Apis.INVENTORY_RESOURCES_BASE_URL}/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<InventoryResource>>() ?? throw new Exception("Response content from getting all inventory resources from the backend is invalid: ");
                }
                else
                {
                    throw new Exception($"Error getting user resources: {response.ReasonPhrase}");
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error getting the user resources from backend" + exception.Message);
            }
        }

        public async Task<InventoryResource> GetUserResourceByResourceIdAsync(Guid userId, Guid resourceId)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync($"{Apis.INVENTORY_RESOURCES_BASE_URL}/userId={userId}&resourceId={resourceId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<InventoryResource>() ?? throw new Exception("Response content from getting all resources by user from the backend is invalid: ");
                }
                else
                {
                    throw new Exception($"Error getting user resource by resource ID: {response.ReasonPhrase}");
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Exception getting user resource by resource ID: {exception.Message}");
            }
        }

        public async Task AddUserResourceAsync(InventoryResource userResource)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.PostAsJsonAsync(Apis.INVENTORY_RESOURCES_BASE_URL, userResource);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error adding user resource: {response.ReasonPhrase}");
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Exception adding user resource: {exception.Message}");
            }
        }

        public async Task UpdateUserResourceAsync(InventoryResource userResource)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.PutAsJsonAsync($"{Apis.INVENTORY_RESOURCES_BASE_URL}/{userResource.Id}", userResource);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error updating user resource: {response.ReasonPhrase}");
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Exception updating user resource: {exception.Message}");
            }
        }

        public async Task DeleteUserResourceAsync(Guid userResourceId)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.DeleteAsync($"{Apis.INVENTORY_RESOURCES_BASE_URL}/{userResourceId}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.Error.WriteLine($"Error deleting user resource: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Exception deleting user resource: {ex.Message}");
            }
        }
    }
}
