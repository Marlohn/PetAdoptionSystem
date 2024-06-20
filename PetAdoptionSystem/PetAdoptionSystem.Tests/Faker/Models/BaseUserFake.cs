using Bogus;

namespace PetAdoptionSystem.Tests.Faker.Models
{
    public abstract class BaseUserFake<T> where T : class
    {
        protected Faker<T> Faker { get; }

        protected BaseUserFake()
        {
            Faker = new Faker<T>()
                .RuleFor("Username", f => f.Internet.UserName())
                .RuleFor("Password", f => f.Internet.Password());
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
