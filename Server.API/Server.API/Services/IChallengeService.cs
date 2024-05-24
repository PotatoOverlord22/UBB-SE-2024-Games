using Server.API.Models;

namespace Server.API.Services
{
    public interface IChallengeService
    {
        Task<Challenge> GetChallengeByIdAsync(Guid id);
        Task<List<Challenge>> GetChallengeAsync();
        Task AddChallengeAsync(Challenge challenge);
        Task DeleteChallengeAsync(Guid id);
        Task UpdateChallengeAsync(Guid id, Challenge challenge);
    }
}