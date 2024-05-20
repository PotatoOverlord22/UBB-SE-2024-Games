using System.IO;

namespace GameWorld.Utils
{
    public class DatabaseHelper
    {
        public static string GetDatabaseFilePath()
        {
            // Get the base directory of the current application domain
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Navigate three folders back from the base directory
            string projectRootDirectory = Path.Combine(
                Directory.GetParent(baseDirectory).Parent.Parent.Parent.FullName);

            // Construct the full path to the database file
            string databaseFilePath = Path.Combine(
                projectRootDirectory, "Database", "Database.mdf");

            // Check if the database file exists
            if (File.Exists(databaseFilePath))
            {
                // return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\PROJECTS\C#\UBB-SE-2024-Random-Entities\HarvestHaven\Repository\Database\Database.mdf;Integrated Security=True;Connect Timeout=30";
                return @$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Integrated Security=True;Connect Timeout=30";
            }
            else
            {
                /*throw new FileNotFoundException("Database file not found at the specified location.");*/
                return null;
            }
        }
    }
}
