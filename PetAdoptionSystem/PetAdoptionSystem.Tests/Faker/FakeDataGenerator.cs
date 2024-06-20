using PetAdoptionSystem.Tests.Faker.Models;

namespace PetAdoptionSystem.Tests.Faker
{
    public static class FakeDataGenerator
    {
        public static FakePetRequestDto PetRequestDto { get; } = new();
        public static FakePetResponseDto PetResponseDto { get; } = new();
        public static FakePet Pet { get; } = new();
        public static FakeUserRequestDto UserRequestDto { get; } = new();
        public static FakeUserResponseDto UserResponseDto { get; } = new();
        public static FakeUser User { get; } = new();
    }
}