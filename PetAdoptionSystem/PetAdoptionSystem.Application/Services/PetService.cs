using PetAdoptionSystem.Application.Dtos;
using PetAdoptionSystem.Application.Interfaces;
using PetAdoptionSystem.Domain.Interfaces;
using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Application.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;

        public PetService(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task<List<PetResponseDto>> GetAllPetsAsync()
        {
            var pets = await _petRepository.GetAllAsync();

            return pets.Select(PetResponseDto.Map).ToList();
        }

        public async Task<PetResponseDto?> GetPetByIdAsync(Guid id)
        {
            var pet = await _petRepository.GetByIdAsync(id);

            if (pet == null)
            {
                return null;
            }

            return PetResponseDto.Map(pet);
        }

        public async Task<PetResponseDto> AddPetAsync(PetRequestDto petDto)
        {
            var pet = new Pet
            {
                Name = petDto.Name,
                Type = petDto.Type,
                Breed = petDto.Breed,
                Sex = petDto.Sex
            };

            pet.Id = await _petRepository.CreateAsync(pet);

            return PetResponseDto.Map(pet);
        }

        public async Task<PetResponseDto?> UpdatePetAsync(Guid id, PetRequestDto petDto)
        {
            var pet = new Pet
            {
                Id = id,
                Name = petDto.Name,
                Type = petDto.Type,
                Breed = petDto.Breed,
                Sex = petDto.Sex
            };

            var updated = await _petRepository.UpdateAsync(pet);
            if (!updated)
            {
                return null;
            }

            return PetResponseDto.Map(pet);
        }

        public async Task<bool> DeletePetAsync(Guid id)
        {
            return await _petRepository.DeleteAsync(id);
        }
    }
}