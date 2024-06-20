using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Tests.Faker.Models
{
    public class FakePet : BasePetFake<Pet>
    {
        public FakePet() : base()
        {
            Faker.RuleFor(p => p.Id, f => Guid.NewGuid());
        }
    }
}
