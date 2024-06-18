using PetAdoptionSystem.Tests.Faker.Models;

namespace PetAdoptionSystem.Tests.Faker
{
    public static class FakeDataGenerator
    {
        public static FakePetDto PetDto { get; }
        public static FakeUserDto UserDto { get; }
        public static FakePet Pet { get; }
        public static FakeUser User { get; }

        static FakeDataGenerator()
        {
            PetDto = new FakePetDto();
            UserDto = new FakeUserDto();  
            Pet = new FakePet();
            User = new FakeUser();
        }
    }
}
