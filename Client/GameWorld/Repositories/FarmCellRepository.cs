using System.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using GameWorld.Models;
using GameWorld.Resources.Utils;
namespace GameWorld.Repositories
{
    public class FarmCellRepository : IFarmCellRepository
    {
        public async Task<List<FarmCell>> GetUserFarmCellsAsync(Guid userId)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync($"{Apis.FARM_CELL}/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<FarmCell>>() ?? throw new Exception("Response content from getting all farm cells from the backend is invalid: ");
                }
                else
                {
                    throw new Exception($"Error getting farm cells: {response.ReasonPhrase}");
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error getting the farm cells from backend" + exception.Message);
            }
        }

        public async Task<FarmCell> GetUserFarmCellByPositionAsync(Guid userId, int row, int column)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync($"{Apis.FARM_CELL}/userId={userId}&row={row}&column={column}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<FarmCell>() ?? throw new Exception("Response content from getting all farm cells by user from the backend is invalid: ");
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

        public async Task AddFarmCellAsync(FarmCell farmCell)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.PostAsJsonAsync(Apis.FARM_CELL, farmCell);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error adding farm cell: {response.ReasonPhrase}");
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Exception adding farm cell: {exception.Message}");
            }
        }

        public async Task UpdateFarmCellAsync(FarmCell farmCell)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.PutAsJsonAsync($"{Apis.FARM_CELL}/{farmCell.Id}", farmCell);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error updating farm cell: {response.ReasonPhrase}");
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Exception updating farm cell: {exception.Message}");
            }
        }

        public async Task DeleteFarmCellAsync(Guid farmCellId)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.DeleteAsync($"{Apis.FARM_CELL}/{farmCellId}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.Error.WriteLine($"Error deleting farm cell: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Exception deleting farm cell: {ex.Message}");
            }
        }
    }
}
