using System.Data;
using System.Data.SqlClient;
using PetAdoptionSystem.Domain.Interfaces;
using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseExecutorService _databaseExecutor;

        public UserRepository(IDatabaseExecutorService databaseExecutor)
        {
            _databaseExecutor = databaseExecutor;
        }

        public async Task<Guid> AddAsync(User user)
        {
            const string query = "INSERT INTO Users (Username, Password, Role) OUTPUT INSERTED.Id VALUES (@Username, @Password, @Role)";
            var parameters = new[]
            {
                new SqlParameter("@Username", SqlDbType.NVarChar, 50) { Value = user.Username },
                new SqlParameter("@Password", SqlDbType.NVarChar, 50) { Value = user.Password },
                new SqlParameter("@Role", SqlDbType.NVarChar, 50) { Value = user.Role }
            };

            var result = await _databaseExecutor.ExecuteScalarAsync<Guid>(query, parameters);
            if (result == Guid.Empty)
            {
                throw new Exception("Failed to insert the user and retrieve the ID.");
            }
            return result;
        }

        public async Task<User?> GetByUsernameAndPasswordAsync(string username, string password)
        {
            const string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
            var parameters = new[]
            {
                new SqlParameter("@Username", SqlDbType.NVarChar, 50) { Value = username },
                new SqlParameter("@Password", SqlDbType.NVarChar, 50) { Value = password }
            };

            var result = await _databaseExecutor.ExecuteQueryAsync<User>(query, parameters);
            return result.FirstOrDefault();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            const string query = "SELECT * FROM Users WHERE Id = @Id";
            var parameters = new[]
            {
                new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id }
            };

            var result = await _databaseExecutor.ExecuteQueryAsync<User>(query, parameters);
            return result.FirstOrDefault();
        }
    }
}