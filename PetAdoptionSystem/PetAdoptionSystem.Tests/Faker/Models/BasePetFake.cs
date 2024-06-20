using Bogus;

namespace PetAdoptionSystem.Tests.Faker.Models
{
    public abstract class BasePetFake<T> where T : class
    {
        private static readonly string[] MalePetNames = { "Max", "Buddy", "Charlie", "Rocky", "Toby" };
        private static readonly string[] FemalePetNames = { "Bella", "Lucy", "Daisy", "Luna", "Molly" };
        private static readonly string[] DogBreeds = { "Beagle", "Labrador", "Bulldog" };
        private static readonly string[] CatBreeds = { "Siamese", "Persian", "Maine Coon" };
        private static readonly string[] BirdBreeds = { "Parrot", "Canary", "Sparrow" };
        private static readonly string[] FishBreeds = { "Goldfish", "Betta", "Guppy" };

        protected Faker<T> Faker { get; }

        protected BasePetFake()
        {
            Faker = new Faker<T>()
                .RuleFor("Type", f => f.PickRandom(new[] { "Dog", "Cat", "Bird", "Fish" }))
                .RuleFor("Sex", f => f.PickRandom(new[] { "Male", "Female" }))
                .RuleFor("Name", (f, p) =>
                {
                    var type = (string)p.GetType().GetProperty("Sex").GetValue(p, null);
                    if (type == "Male")
                        return f.PickRandom(MalePetNames);
                    else
                        return f.PickRandom(FemalePetNames);
                })
                .RuleFor("Breed", (f, p) =>
                {
                    var type = (string)p.GetType().GetProperty("Type").GetValue(p, null);
                    return type switch
                    {
                        "Dog" => f.PickRandom(DogBreeds),
                        "Cat" => f.PickRandom(CatBreeds),
                        "Bird" => f.PickRandom(BirdBreeds),
                        "Fish" => f.PickRandom(FishBreeds),
                        _ => "Unknown"
                    };
                });
        }

        public T Generate()
        {
            return Faker.Generate();
        }

        public List<T> Generate(int count)
        {
            return Faker.Generate(count);
        }
    }
}