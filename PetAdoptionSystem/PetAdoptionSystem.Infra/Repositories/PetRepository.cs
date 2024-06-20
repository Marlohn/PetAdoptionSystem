using System.Data;
using System.Data.SqlClient;
using PetAdoptionSystem.Domain.Interfaces;
using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Infra.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly IDatabaseExecutorService _databaseExecutor;

        public PetRepository(IDatabaseExecutorService databaseExecutor)
        {
            _databaseExecutor = databaseExecutor;
        }

        public async Task<List<Pet>> GetAllAsync()
        {
            const string query = "SELECT * FROM Pets";

            return await _databaseExecutor.ExecuteQueryAsync<Pet>(query);
        }

        public async Task<Pet?> GetByIdAsync(Guid id)
        {
            const string query = "SELECT * FROM Pets WHERE Id = @Id";

            var parameters = new[]
            {
                new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id }
            };

            var result = await _databaseExecutor.ExecuteQueryAsync<Pet>(query, parameters);

            return result.FirstOrDefault();
        }

        public async Task<Guid> CreateAsync(Pet pet)
        {
            const string query = @"
                INSERT INTO Pets (Name, Type, Breed, Sex)
                OUTPUT INSERTED.Id
                VALUES (@Name, @Type, @Breed, @Sex)";

            var parameters = new[]
            {
                new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = pet.Name },
                new SqlParameter("@Type", SqlDbType.NVarChar, 50) { Value = pet.Type },
                new SqlParameter("@Breed", SqlDbType.NVarChar, 50) { Value = pet.Breed },
                new SqlParameter("@Sex", SqlDbType.NVarChar, 10) { Value = pet.Sex }
            };

            var result = await _databaseExecutor.ExecuteScalarAsync<Guid>(query, parameters);

            if (result == Guid.Empty)
            {
                throw new Exception("Failed to insert the pet and retrieve the ID.");
            }

            return result;
        }

        public async Task<bool> UpdateAsync(Pet pet)
        {
            const string query = "UPDATE Pets SET Name = @Name, Type = @Type, Breed = @Breed, Sex = @Sex WHERE Id = @Id";

            var parameters = new[]
            {
                new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = pet.Id },
                new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = pet.Name },
                new SqlParameter("@Type", SqlDbType.NVarChar, 50) { Value = pet.Type },
                new SqlParameter("@Breed", SqlDbType.NVarChar, 50) { Value = pet.Breed },
                new SqlParameter("@Sex", SqlDbType.NVarChar, 10) { Value = pet.Sex }
            };

            var rowsAffected = await _databaseExecutor.ExecuteNonQueryAsync(query, parameters);

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            const string query = "DELETE FROM Pets WHERE Id = @Id";

            var parameters = new[]
            {
                new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id }
            };

            var rowsAffected = await _databaseExecutor.ExecuteNonQueryAsync(query, parameters);

            return rowsAffected > 0;
        }
    }
}