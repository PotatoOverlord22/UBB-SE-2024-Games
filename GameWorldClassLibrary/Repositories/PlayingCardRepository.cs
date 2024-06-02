using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;
using GameWorldClassLibrary.Utils;
using Microsoft.EntityFrameworkCore;

public class PlayingCardRepository : IPlayingCardRepository
{
    private readonly GamesContext context;

    public PlayingCardRepository(GamesContext context)
    {
        this.context = context;
    }

    public async Task<List<PlayingCard>> GetPlayingCardAsync()
    {
        return await context.PlayingCards.ToListAsync();
    }

    public async Task<PlayingCard> GetPlayingCardByIdAsync(Guid id)
    {
        var playingCard = await context.PlayingCards.FindAsync(id);

        if (playingCard == null)
        {
            throw new KeyNotFoundException("PlayingCard not found");
        }

        return playingCard;
    }
    public async Task AddPlayingCardAsync(PlayingCard playingCard)
    {
        context.PlayingCards.Add(playingCard);
        await context.SaveChangesAsync();
    }
    public async Task DeletePlayingCardAsync(Guid id)
    {
        var playingCard = context.PlayingCards.Find(id);
        if (playingCard == null)
        {
            throw new KeyNotFoundException("PlayingCard not found");
        }
        context.PlayingCards.Remove(playingCard);
        await context.SaveChangesAsync();
    }
    public async Task UpdatePlayingCardAsync(Guid id, PlayingCard playingCard)
    {
        if (context.PlayingCards.Find(id) == null)
        {
            throw new KeyNotFoundException("PlayingCard not found");
        }
        context.PlayingCards.Update(playingCard);
        await context.SaveChangesAsync();
    }
}
