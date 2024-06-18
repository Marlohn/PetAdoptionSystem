namespace PetAdoptionSystem.Application.Dtos
{
    public class PetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public string Sex { get; set; }
        //public bool IsAdopted { get; set; }
    }
}