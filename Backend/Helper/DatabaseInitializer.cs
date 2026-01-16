using Backend.Interfaces;
using Microsoft.Data.Sqlite;

namespace Backend.Helper
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        // Connection String To Connect To The Database
        private readonly string? connectionString;

        public DatabaseInitializer(IConfiguration configuration)
        {
            // Get The Connection String From Configuration
            connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("No Connection String Found"); // Throw Exception If No Connection String Found
        }
        public void InitializeDatabase()
        {
            // Create A New SQLite Connection Using The Connection String
            // 'using var' Ensures The Connection Is Properly Disposed After Use
            using var connection = new SqliteConnection(connectionString);
            // Open The Connection
            connection.Open();

            // Create A Command To Execute SQL Statements
            // 'using var' Ensures The Command Is Properly Disposed After Use
            using var command = connection.CreateCommand();
            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Tasks (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT NOT NULL,
                IsCompleted BOOLEAN NOT NULL DEFAULT 0,
                CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
            );";

            // Execute The Command To Create The Table If It Doesn't Exist
            command.ExecuteNonQuery();
        }
    }
}
