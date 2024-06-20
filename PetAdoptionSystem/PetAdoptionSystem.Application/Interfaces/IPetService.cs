using PetAdoptionSystem.Application.Dtos;

namespace PetAdoptionSystem.Application.Interfaces
{
    public interface IPetService
    {
        Task<List<PetResponseDto>> GetAllPetsAsync();
        Task<PetResponseDto?> GetPetByIdAsync(Guid id);
        Task <PetResponseDto> AddPetAsync(PetRequestDto petDto);
        Task <PetResponseDto?> UpdatePetAsync(Guid id, PetRequestDto petDto);
        Task<bool> DeletePetAsync(Guid id);
    }
}