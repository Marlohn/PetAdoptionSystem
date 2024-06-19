using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Domain.Interfaces
{
    public interface IPetRepository
    {
        Task<List<Pet>> GetAllAsync();
        Task<Pet?> GetByIdAsync(Guid id);
        Task CreateAsync(Pet pet);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Pet pet);
    }
}