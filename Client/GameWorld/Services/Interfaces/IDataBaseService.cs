using GameWorld.Models;

namespace GameWorld.Services
{
    public interface IDataBaseService
    {
        List<string> GetLeaderboard();
        List<string> GetAllRequestsByToUserID(Guid toUser);
        List<Tuple<Guid, Guid>> GetAllRequestsByToUserIDSimplified(Guid toUser);
        void CreateRequest(Guid fromUser, Guid toUser);
        string GetUserNameByUserId(Guid userId);
        Guid GetUserIdByUserName(string username);
        int GetChipsByUserId(Guid userId);
        void DeleteRequestsByUserId(Guid userId);
    }
}
