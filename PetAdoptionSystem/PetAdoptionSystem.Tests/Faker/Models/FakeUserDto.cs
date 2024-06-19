using Bogus;
using PetAdoptionSystem.Application.Dtos;

namespace PetAdoptionSystem.Tests.Faker.Models
{
    public class FakeUserDto
    {
        private static readonly Faker<UserResponseDto> _faker = new Faker<UserResponseDto>()
            .RuleFor(u => u.Id, f => Guid.NewGuid())
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Password, (f, u) => f.Internet.Password());

        public UserResponseDto Generate()
        {
            return _faker.Generate();
        }

        public List<UserResponseDto> Generate(int count)
        {
            return _faker.Generate(count);
        }
    }
}