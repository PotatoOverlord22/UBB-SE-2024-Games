using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Repositories
{
    public interface IChallengeRepository
    {
        Task<Challenge> GetChallengeByIdAsync(Guid id);
        Task<List<Challenge>> GetChallengeAsync();
        Task AddChallengeAsync(Challenge challenge);
        Task DeleteChallengeAsync(Guid id);
        Task UpdateChallengeAsync(Guid id, Challenge challenge);
    }
}