using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Application.Dtos
{
    public class PetResponseDto : PetRequestDto
    {
        public Guid Id { get; set; }

        public static PetResponseDto Map(Pet pet)
        {
            return new PetResponseDto
            {
                Id = pet.Id,
                Name = pet.Name,
                Type = pet.Type,
                Breed = pet.Breed,
                Sex = pet.Sex
            };
        }
    }
}