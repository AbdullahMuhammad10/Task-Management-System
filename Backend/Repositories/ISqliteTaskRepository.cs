
using Microsoft.Data.Sqlite;

namespace Backend.Repositories
{
    public class SqliteTaskRepository : ITaskRepository
    {
        private readonly string connectionString;

        public SqliteTaskRepository(IConfiguration configuration)
        {
            // Get The Connection String From Configuration
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public IEnumerable<TaskItem> GetAll()
        {
            var Tasks = new List<TaskItem>();
            // Create and open a connection to the SQLite database
            using var Connection = GetOpenConnection();

            // Create a command to retrieve all tasks 
            var Command = CreateCommand(Connection,
                "SELECT Id, Title, CreatedAt, IsCompleted FROM Tasks");

            // Execute the command and read the results
            using var Reader = Command.ExecuteReader();
            while(Reader.Read())
            {
                // Map the data to TaskItem objects and add them to the list
                Tasks.Add(MapReaderToTaskItem(Reader));
            }
            return Tasks;
        }

        public TaskItem? GetById(int id)
        {
            // Create and open a connection to the SQLite database
            using var Connection = GetOpenConnection();
            // Create a command to retrieve a task by its ID With Parameterized Query
            var Command = CreateCommand(Connection,
                "SELECT Id, Title, CreatedAt, IsCompleted FROM Tasks WHERE Id = @id",
                ("@id", id));

            // Execute the command and read the result
            using var Reader = Command.ExecuteReader();
            // If a task is found, map it to a TaskItem object; otherwise, return null
            return Reader.Read() ? MapReaderToTaskItem(Reader) : null;
        }

        public void Add(TaskItem task)
        {
            // Create and open a connection to the SQLite database
            using var Connection = GetOpenConnection();

            // Create a command to insert a new task With Parameterized Query
            var Command = CreateCommand(Connection,
                "INSERT INTO Tasks (Title, CreatedAt, IsCompleted) VALUES (@title, @createdAt, @isCompleted)",
               ("@title", task.Title),("@createdAt", task.CreatedAt),("@isCompleted", task.IsCompleted));

            // Execute the command to insert the new task
            Command.ExecuteNonQuery();
        }

        public void Update(TaskItem task)
        {
            // Create and open a connection to the SQLite database

            using var Connection = GetOpenConnection();

            // Create a command to update an existing task With Parameterized Query
            var Command = CreateCommand(Connection,"UPDATE Tasks SET Title = @title, IsCompleted = @isCompleted WHERE Id = @id"
                ,("@id", task.Id),("@title", task.Title),("@createdAt", task.CreatedAt),("@isCompleted", task.IsCompleted));

            //
            Command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var Connection = GetOpenConnection();

            // Create a command to delete a task by its ID With Parameterized Query
            var Command = CreateCommand(Connection,"DELETE FROM Tasks WHERE Id = @id",
                ("@id", id));
            // Execute the command to delete the task
            Command.ExecuteNonQuery();
        }

        // Helper Methods
        // Map the data from the SqliteDataReader to a TaskItem object
        private TaskItem MapReaderToTaskItem(SqliteDataReader reader)
        {
            return new TaskItem
            {
                Id = reader.GetInt32(0), // Id At index 0
                Title = reader.GetString(1), // Title At index 1 
                CreatedAt = reader.GetDateTime(2), // CreatedAt At index 2
                IsCompleted = reader.GetBoolean(3) // IsCompleted At index 3
            };
        }

        // Create and open a new SqliteConnection
        private SqliteConnection GetOpenConnection()
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();
            return connection;
        }
        // Create a new SqliteCommand with the specified command text and parameters
        private SqliteCommand CreateCommand(SqliteConnection connection,string commandText,params (string name, object value)[] parameters)
        {
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            foreach(var param in parameters)
            {
                command.Parameters.AddWithValue(param.name,param.value);
            }
            return command;
        }

    }
}

