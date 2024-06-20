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

            return pets.Select(p => new PetResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Type = p.Type,
                Breed = p.Breed,
                Sex = p.Sex
            }).ToList();
        }

        public async Task<PetResponseDto?> GetPetByIdAsync(Guid id)
        {
            var pet = await _petRepository.GetByIdAsync(id);

            if (pet == null)
            {
                return null;
            }

            return new PetResponseDto
            {
                Id = pet.Id,
                Name = pet.Name,
                Type = pet.Type,
                Breed = pet.Breed,
                Sex = pet.Sex
            };
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

            var id = await _petRepository.CreateAsync(pet);

            return new PetResponseDto
            {
                Id = id,
                Name = pet.Name,
                Type = pet.Type,
                Breed = pet.Breed,
                Sex = pet.Sex
            };
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

            return new PetResponseDto
            {
                Id = pet.Id,
                Name = pet.Name,
                Type = pet.Type,
                Breed = pet.Breed,
                Sex = pet.Sex
            };
        }

        public async Task<bool> DeletePetAsync(Guid id)
        {
            return await _petRepository.DeleteAsync(id);
        }
    }
}