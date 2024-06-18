using PetAdoptionSystem.Application.Dtos;

namespace PetAdoptionSystem.Application.Interfaces
{
    public interface IPetService
    {
        Task<List<PetDto>> GetAllPetsAsync();
        Task<PetDto?> GetPetByIdAsync(Guid id);
        Task AddPetAsync(PetDto petDto);
        Task UpdatePetAsync(PetDto petDto);
        Task DeletePetAsync(Guid id);
    }
}