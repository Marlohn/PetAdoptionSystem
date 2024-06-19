using Bogus;
using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Tests.Faker.Models
{
    public class FakePet
    {
        private static readonly string[] MalePetNames = { "Max", "Buddy", "Charlie", "Rocky", "Toby" };
        private static readonly string[] FemalePetNames = { "Bella", "Lucy", "Daisy", "Luna", "Molly" };
        private static readonly string[] DogBreeds = { "Beagle", "Labrador", "Bulldog" };
        private static readonly string[] CatBreeds = { "Siamese", "Persian", "Maine Coon" };
        private static readonly string[] BirdBreeds = { "Parrot", "Canary", "Sparrow" };
        private static readonly string[] FishBreeds = { "Goldfish", "Betta", "Guppy" };

        private static readonly Faker<Pet> _faker = new Faker<Pet>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Type, f => f.PickRandom(new[] { "Dog", "Cat", "Bird", "Fish" }))
            .RuleFor(p => p.Sex, f => f.PickRandom(new[] { "Male", "Female" }))
            .RuleFor(p => p.Name, (f, p) =>
            {
                if (p.Sex == "Male")
                    return f.PickRandom(MalePetNames);
                else
                    return f.PickRandom(FemalePetNames);
            })
            .RuleFor(p => p.Breed, (f, p) =>
            {
                return p.Type switch
                {
                    "Dog" => f.PickRandom(DogBreeds),
                    "Cat" => f.PickRandom(CatBreeds),
                    "Bird" => f.PickRandom(BirdBreeds),
                    "Fish" => f.PickRandom(FishBreeds),
                    _ => "Unknown"
                };
            });

        public Pet Generate()
        {
            return _faker.Generate();
        }

        public List<Pet> Generate(int count)
        {
            return _faker.Generate(count);
        }
    }
}
