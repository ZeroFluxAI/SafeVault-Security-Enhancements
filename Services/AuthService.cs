using Microsoft.Data.Sqlite;
using SafeVault.Models;
using SafeVault.DTOs;
using BCrypt.Net;

namespace SafeVault.Services;

public class AuthService
{
    private const string ConnectionString = "Data Source=safevault.db";

    public bool Register(RegisterRequest request, out string errorMessage)
    {
        errorMessage = string.Empty;
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            errorMessage = "Username and password are required.";
            return false;
        }

        string hash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Users (Username, PasswordHash, Role)
            VALUES (@username, @hash, @role);";
        command.Parameters.AddWithValue("@username", request.Username);
        command.Parameters.AddWithValue("@hash", hash);
        command.Parameters.AddWithValue("@role", string.Equals(request.Role, "Admin", StringComparison.OrdinalIgnoreCase) ? "Admin" : "User");

        try
        {
            command.ExecuteNonQuery();
            return true;
        }
        catch (SqliteException ex) when (ex.SqliteErrorCode == 19)
        {
            errorMessage = "Username already exists.";
            return false;
        }
    }

    public User? ValidateUser(LoginRequest request)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Username, PasswordHash, Role FROM Users WHERE Username = @username;";
        command.Parameters.AddWithValue("@username", request.Username);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            string hash = reader.GetString(2);
            if (BCrypt.Net.BCrypt.Verify(request.Password, hash))
            {
                return new User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    PasswordHash = hash,
                    Role = reader.GetString(3)
                };
            }
        }
        return null;
    }
}
