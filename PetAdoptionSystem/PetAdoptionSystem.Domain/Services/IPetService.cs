using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Domain.Services
{
    public interface IPetService
    {
        Task<List<Pet>> GetAllPetsAsync();
    }
}