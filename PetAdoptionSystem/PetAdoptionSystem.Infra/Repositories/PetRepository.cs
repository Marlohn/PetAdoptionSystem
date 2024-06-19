using PetAdoptionSystem.Domain.Interfaces;
using PetAdoptionSystem.Domain.Models;
using System.Data;
using System.Data.SqlClient;

namespace PetAdoptionSystem.Infra.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly string _connectionString;

        public PetRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Pet>> GetAllAsync()
        {
            var pets = new List<Pet>();
            const string query = "SELECT * FROM Pets";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            pets.Add(new Pet
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Type = reader.GetString(reader.GetOrdinal("Type")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                Sex = reader.GetString(reader.GetOrdinal("Sex"))
                            });
                        }
                    }
                }
            }

            return pets;
        }

        public async Task<Pet?> GetByIdAsync(Guid id)
        {
            Pet? pet = null;
            const string query = "SELECT * FROM Pets WHERE Id = @Id";

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
                            pet = new Pet
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Type = reader.GetString(reader.GetOrdinal("Type")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                Sex = reader.GetString(reader.GetOrdinal("Sex"))
                            };
                        }
                    }
                }
            }

            return pet;
        }

        public async Task CreateAsync(Pet pet)
        {
            const string query = "INSERT INTO Pets (Id, Name, Type, Breed, Sex) VALUES (@Id, @Name, @Type, @Breed, @Sex)";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = pet.Id });
                    command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = pet.Name });
                    command.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar, 50) { Value = pet.Type });
                    command.Parameters.Add(new SqlParameter("@Breed", SqlDbType.NVarChar, 50) { Value = pet.Breed });
                    command.Parameters.Add(new SqlParameter("@Sex", SqlDbType.NVarChar, 10) { Value = pet.Sex });
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Pet pet)
        {
            const string query = "UPDATE Pets SET Name = @Name, Type = @Type, Breed = @Breed, Sex = @Sex WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = pet.Id });
                    command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = pet.Name });
                    command.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar, 50) { Value = pet.Type });
                    command.Parameters.Add(new SqlParameter("@Breed", SqlDbType.NVarChar, 50) { Value = pet.Breed });
                    command.Parameters.Add(new SqlParameter("@Sex", SqlDbType.NVarChar, 10) { Value = pet.Sex });
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            const string query = "DELETE FROM Pets WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id });
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}