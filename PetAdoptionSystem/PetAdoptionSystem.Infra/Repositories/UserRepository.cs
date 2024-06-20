using PetAdoptionSystem.Domain.Interfaces;
using PetAdoptionSystem.Domain.Models;
using System.Data;
using System.Data.SqlClient;

namespace PetAdoptionSystem.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Guid> AddAsync(User user)
        {
            const string query = "INSERT INTO Users (Username, Password) OUTPUT INSERTED.Id VALUES (@Username, @Password)";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 50) { Value = user.Username });
                    command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 50) { Value = user.Password });

                    var result = await command.ExecuteScalarAsync();
                    if (result == null)
                    {
                        throw new Exception("Failed to insert the user and retrieve the ID.");
                    }

                    return (Guid)result;
                }
            }
        }

        public async Task<User?> GetByUsernameAndPasswordAsync(string username, string password)
        {
            User? user = null;
            const string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 50) { Value = username });
                    command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 50) { Value = password });

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = new User
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                Password = reader.GetString(reader.GetOrdinal("Password"))
                            };
                        }
                    }
                }
            }

            return user;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            User? user = null;
            const string query = "SELECT * FROM Users WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id });

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = new User
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                Password = reader.GetString(reader.GetOrdinal("Password"))
                            };
                        }
                    }
                }
            }

            return user;
        }
    }
}
