using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Repositories
{
    public interface IPlayerRepository
    {
        Player? GetPlayerById(Guid id);
        bool AddPlayer(Player player);
        bool RemoveAddressById(Guid id);
        bool UpdatePlayer(Player player);
        bool RemovePlayerWhereNameEqualsTestOrUpdated();
    }
}