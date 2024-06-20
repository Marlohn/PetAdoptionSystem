using PetAdoptionSystem.Application.Dtos;

namespace PetAdoptionSystem.Tests.Faker.Models
{
    public class FakePetResponseDto : BasePetFake<PetResponseDto>
    {
        public FakePetResponseDto() : base()
        {
            Faker.RuleFor(p => p.Id, f => Guid.NewGuid());
        }
    }
}