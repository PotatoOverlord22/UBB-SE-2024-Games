using System.Data;
using Microsoft.Data.SqlClient;
namespace GameWorld.Resources.Utils
{
    public interface IDatabaseProvider
    {
        Task<IDataReader> ExecuteReaderAsync(string query, IDictionary<string, object> parameters);
    }
    public class DatabaseProvider : IDatabaseProvider
    {
        private readonly string connectionString;
        public DatabaseProvider()
        {
            connectionString = DatabaseHelper.GetDatabaseFilePath();
        }
        public async Task<IDataReader> ExecuteReaderAsync(string query, IDictionary<string, object> parameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            await connection.OpenAsync();
            SqlCommand command = new SqlCommand(query, connection);
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
            }
            SqlDataReader reader = await command.ExecuteReaderAsync();
            return reader;
        }
    }
}
