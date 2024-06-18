using PetAdoptionSystem.Domain.Interfaces;
using PetAdoptionSystem.Domain.Models;
using System.Data.SqlClient;

namespace PetAdoptionSystem.Infra.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly string _connectionString;

        public PetRepository()
        {
            _connectionString = "";
        }

        //public PetRepository(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

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

        public Task<Pet?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Pet pet)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Pet pet)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}