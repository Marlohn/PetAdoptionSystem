using PetAdoptionSystem.Application.Dtos;

namespace PetAdoptionSystem.Application.Interfaces
{
    public interface IPetService
    {
        Task<List<PetResponseDto>> GetAllPetsAsync();
        Task<PetResponseDto?> GetPetByIdAsync(Guid id);
        Task AddPetAsync(PetRequestDto petDto);
        Task UpdatePetAsync(Guid id, PetRequestDto petDto);
        Task DeletePetAsync(Guid id);
    }
}