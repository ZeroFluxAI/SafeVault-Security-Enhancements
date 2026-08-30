using Microsoft.Data.Sqlite;
using BCrypt.Net;

namespace SafeVault.Data;

public static class DatabaseInitializer
{
    private const string ConnectionString = "Data Source=safevault.db";

    public static void Initialize()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        var tableCommand = connection.CreateCommand();
        tableCommand.CommandText = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT NOT NULL UNIQUE,
                PasswordHash TEXT NOT NULL,
                Role TEXT NOT NULL
            );";
        tableCommand.ExecuteNonQuery();

        // Seed default admin account if non-existent
        var checkAdminCommand = connection.CreateCommand();
        checkAdminCommand.CommandText = "SELECT COUNT(1) FROM Users WHERE Username = @username;";
        checkAdminCommand.Parameters.AddWithValue("@username", "admin");
        var count = Convert.ToInt32(checkAdminCommand.ExecuteScalar());

        if (count == 0)
        {
            var seedCommand = connection.CreateCommand();
            seedCommand.CommandText = @"
                INSERT INTO Users (Username, PasswordHash, Role)
                VALUES (@username, @hash, @role);";
            seedCommand.Parameters.AddWithValue("@username", "admin");
            seedCommand.Parameters.AddWithValue("@hash", BCrypt.Net.BCrypt.HashPassword("ChangeMe_Admin_123!"));
            seedCommand.Parameters.AddWithValue("@role", "Admin");
            seedCommand.ExecuteNonQuery();
        }
    }
}
