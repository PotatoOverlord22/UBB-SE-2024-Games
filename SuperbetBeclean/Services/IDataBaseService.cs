using SuperbetBeclean.Models;

namespace SuperbetBeclean.Services
{
    public interface IDataBaseService
    {
        void OpenConnection();
        void CloseConnection();
        void UpdateUser(int id, string username, int currentFont, int currentTitle, int currentIcon, int currentTable, int chips, int stack, int streak, int handsPlayed, int level, DateTime lastLogin);
        void UpdateUserFont(int id, int font);
        void UpdateUserTitle(int id, int title);
        void UpdateUserIcon(int id, int icon);
        void UpdateUserChips(int id, int chips);
        void UpdateUserStack(int id, int stack);
        void UpdateUserStreak(int id, int streak);
        void UpdateUserHandsPlayed(int id, int handsPlayed);
        void UpdateUserLevel(int id, int level);
        void UpdateUserLastLogin(int id, DateTime lastLogin);
        void UpdateChallenge(int challengeId, string description, string rule, int amount, int reward);
        void UpdateFont(int fontId, string fontName, int fontPrice, string fontType);
        void UpdateIcon(int iconId, string iconName, int iconPrice, string iconPath);
        void UpdateTitle(int titleId, string titleName, int titlePrice, string titleContent);
        string GetIconPath(int iconId);
        List<string> GetLeaderboard();
        List<ShopItem> GetShopItems();
        List<ShopItem> GetAllUserIconsByUserId(int userId);
        void CreateUserIcon(int userId, int iconId);
        int GetIconIDByIconName(string iconName);
        void SetCurrentIcon(int userId, int iconId);
        List<string> GetAllRequestsByToUserID(int toUser);
        List<Tuple<int, int>> GetAllRequestsByToUserIDSimplified(int toUser);
        void CreateRequest(int fromUser, int toUser);
        string GetUserNameByUserId(int userId);
        int GetUserIdByUserName(string username);
        int GetChipsByUserId(int userId);
        void DeleteRequestsByUserId(int userId);
    }
}
