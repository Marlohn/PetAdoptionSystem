using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Tests.Faker.Models
{
    public class FakeUser : BaseUserFake<User>
    {
        public FakeUser() : base()
        {
            Faker.RuleFor(p => p.Id, f => Guid.NewGuid());
        }
    }
}