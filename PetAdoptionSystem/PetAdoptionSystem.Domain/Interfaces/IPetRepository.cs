using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Domain.Interfaces
{
    public interface IPetRepository
    {
        Task CreateAsync(Pet pet);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Pet>> GetAllAsync();
        Task<Pet> GetByIdAsync(Guid id);
        Task UpdateAsync(Pet pet);
    }
}