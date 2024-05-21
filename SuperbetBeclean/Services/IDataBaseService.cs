using SuperbetBeclean.Models;

namespace SuperbetBeclean.Services
{
    public interface IDataBaseService
    {
        void OpenConnection();
        void CloseConnection();
        void UpdateUser(Guid id, string username, int currentFont, int currentTitle, int currentIcon, int currentTable, int chips, int stack, int streak, int handsPlayed, int level, DateTime lastLogin);
        void UpdateUserFont(Guid id, int font);
        void UpdateUserTitle(Guid id, int title);
        void UpdateUserIcon(Guid id, int icon);
        void UpdateUserChips(Guid id, int chips);
        void UpdateUserStack(Guid id, int stack);
        void UpdateUserStreak(Guid id, int streak);
        void UpdateUserHandsPlayed(Guid id, int handsPlayed);
        void UpdateUserLevel(Guid id, int level);
        void UpdateUserLastLogin(Guid id, DateTime lastLogin);
        void UpdateChallenge(int challengeId, string description, string rule, int amount, int reward);
        void UpdateFont(int fontId, string fontName, int fontPrice, string fontType);
        void UpdateIcon(int iconId, string iconName, int iconPrice, string iconPath);
        void UpdateTitle(int titleId, string titleName, int titlePrice, string titleContent);
        string GetIconPath(int iconId);
        List<string> GetLeaderboard();
        List<ShopItem> GetShopItems();
        List<ShopItem> GetAllUserIconsByUserId(Guid userId);
        void CreateUserIcon(Guid userId, int iconId);
        int GetIconIDByIconName(string iconName);
        void SetCurrentIcon(Guid userId, int iconId);
        List<string> GetAllRequestsByToUserID(Guid toUser);
        List<Tuple<int, int>> GetAllRequestsByToUserIDSimplified(Guid toUser);
        void CreateRequest(Guid fromUser, Guid toUser);
        string GetUserNameByUserId(Guid userId);
        Guid GetUserIdByUserName(string username);
        int GetChipsByUserId(Guid userId);
        void DeleteRequestsByUserId(Guid userId);
    }
}
