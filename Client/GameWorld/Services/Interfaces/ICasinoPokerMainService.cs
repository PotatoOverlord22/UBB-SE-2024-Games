using GameWorld.Models;
using GameWorld.Views;

namespace GameWorld.Services
{
    public interface ICasinoPokerMainService
    {
        int OccupiedIntern();
        int OccupiedJunior();
        int OccupiedSenior();
        void NewUserLogin(User newUser);
        void AddWindow(string username);
        void DisconnectUser(MenuWindow window);
        int JoinInternTable(MenuWindow window);
        int JoinJuniorTable(MenuWindow window);
        int JoinSeniorTable(MenuWindow window);
        /*     User FetchUser(SqlConnection sqlConnection, string userName);
             User CreateUserFromReader(SqlDataReader reader);
             DateTime GetDateFromReader(SqlDataReader reader, string columnName);
             string GetStringFromReader(SqlDataReader reader, string columnName);
             int GetIntFromReader(SqlDataReader reader, string columnName);*/
    }
}