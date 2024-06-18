using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Domain.Services
{
    public class PetService : IPetService
    {
        public async Task<List<Pet>> GetAllPetsAsync()
        {
            var petList = new List<Pet>();

            return petList;
        }
    }
}