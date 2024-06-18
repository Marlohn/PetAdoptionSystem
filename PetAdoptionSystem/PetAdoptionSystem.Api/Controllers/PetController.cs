using Microsoft.AspNetCore.Mvc;
using PetAdoptionSystem.Domain.Models;
using PetAdoptionSystem.Domain.Services;

namespace PetAdoptionSystem.Api.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPets()
        {
            List<Pet> result = await _petService.GetAllPetsAsync();

            return Ok(result);
        }
    }
}