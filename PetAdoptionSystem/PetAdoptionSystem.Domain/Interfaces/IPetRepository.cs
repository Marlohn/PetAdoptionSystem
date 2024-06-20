using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Domain.Interfaces
{
    public interface IPetRepository
    {
        Task<List<Pet>> GetAllAsync();
        Task<Pet?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(Pet pet);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateAsync(Pet pet);
    }
}