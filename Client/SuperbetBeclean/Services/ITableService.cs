using System.Data.SqlClient;
using SuperbetBeclean.Windows;

namespace SuperbetBeclean.Services
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