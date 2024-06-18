
namespace PetAdoptionSystem.Domain.Models
{
    public class Pet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public string Sex { get; set; }
    }
}