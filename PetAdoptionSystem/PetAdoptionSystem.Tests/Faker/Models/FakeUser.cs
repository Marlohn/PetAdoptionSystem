using Bogus;
using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Tests.Faker.Models
{
    public class FakeUser
    {
        private static readonly Faker<User> _faker = new Faker<User>()
            .RuleFor(u => u.Id, f => Guid.NewGuid())
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Password, (f, u) => f.Internet.Password());

        public User Generate()
        {
            return _faker.Generate();
        }

        public List<User> Generate(int count)
        {
            return _faker.Generate(count);
        }
    }
}
