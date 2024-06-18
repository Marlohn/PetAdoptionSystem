using Bogus;
using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Tests.Faker.Models
{
    public class FakeUser
    {
        private static readonly Faker<User> _faker = new Faker<User>()
            .RuleFor(u => u.Id, f => Guid.NewGuid())
            .RuleFor(u => u.Name, f => f.Name.FullName())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email());

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