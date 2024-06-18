using Bogus;
using PetAdoptionSystem.Application.Dtos;

namespace PetAdoptionSystem.Tests.Faker.Models
{
    public class FakeUser
    {
        private static readonly Faker<UserDto> _faker = new Faker<UserDto>()
            .RuleFor(u => u.Id, f => Guid.NewGuid())
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Password, (f, u) => f.Internet.Password());

        public UserDto Generate()
        {
            return _faker.Generate();
        }

        public List<UserDto> Generate(int count)
        {
            return _faker.Generate(count);
        }
    }
}