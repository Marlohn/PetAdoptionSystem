using Bogus;
using PetAdoptionSystem.Application.Dtos;

namespace PetAdoptionSystem.Tests.Faker.Models
{
    public class FakeUserResponseDto : BaseUserFake<UserResponseDto>
    {
        public FakeUserResponseDto() : base()
        {
            Faker.RuleFor(p => p.Id, f => Guid.NewGuid());
        }
    }
}