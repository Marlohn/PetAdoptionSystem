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

        public async Task<List<PetDto>> GetAllPetsAsync()
        {
            var pets = await _petRepository.GetAllAsync();

            return pets.Select(p => new PetDto
            {
                Id = p.Id,
                Name = p.Name,
                Type = p.Type,
                Breed = p.Breed,
                Sex = p.Sex
            }).ToList();
        }

        public async Task<PetDto?> GetPetByIdAsync(Guid id)
        {
            var pet = await _petRepository.GetByIdAsync(id);

            if (pet == null)
            {
                return null;
            }

            return new PetDto
            {
                Id = pet.Id,
                Name = pet.Name,
                Type = pet.Type,
                Breed = pet.Breed,
                Sex = pet.Sex
            };
        }

        public async Task AddPetAsync(PetDto petDto)
        {
            var pet = new Pet
            {
                Id = petDto.Id,
                Name = petDto.Name,
                Type = petDto.Type,
                Breed = petDto.Breed,
                Sex = petDto.Sex
            };

            await _petRepository.CreateAsync(pet);
        }

        public async Task UpdatePetAsync(PetDto petDto)
        {
            var pet = new Pet
            {
                Id = petDto.Id,
                Name = petDto.Name,
                Type = petDto.Type,
                Breed = petDto.Breed,
                Sex = petDto.Sex
            };

            await _petRepository.UpdateAsync(pet);
        }

        public async Task DeletePetAsync(Guid id)
        {
            await _petRepository.DeleteAsync(id);
        }
    }
}