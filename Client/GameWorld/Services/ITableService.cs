using GameWorld.Views;
using Microsoft.Data.SqlClient;

namespace GameWorld.Services
{
    public interface ITableService
    {
        void RunTable();
        void DisconnectUser(MenuWindow window);
        int JoinTable(MenuWindow window, ref SqlConnection sqlConnection);
        bool IsFull();
        bool IsEmpty();
        int Occupied();
    }
}