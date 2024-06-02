using Server.API.Models;

namespace Server.API.Repositories
{
    public interface IPlayingCardRepository
    {
        Task<PlayingCard> GetPlayingCardByIdAsync(Guid id);
        Task<List<PlayingCard>> GetPlayingCardAsync();
        Task AddPlayingCardAsync(PlayingCard playingCard);
        Task DeletePlayingCardAsync(Guid id);
        Task UpdatePlayingCardAsync(Guid id, PlayingCard playingCard);
    }
}