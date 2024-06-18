using PetAdoptionSystem.Tests.Faker.Models;

namespace PetAdoptionSystem.Tests.Faker
{
    public static class FakeDataGenerator
    {
        public static FakePet Pets { get; }
        public static FakeUser Users { get; }

        static FakeDataGenerator()
        {
            Pets = new FakePet();
            Users = new FakeUser();
        }
    }
}
