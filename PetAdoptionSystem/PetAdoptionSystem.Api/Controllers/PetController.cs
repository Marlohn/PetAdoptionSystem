using Microsoft.AspNetCore.Mvc;
using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Api.Controllers
{
    public class PetController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAllPets()
        {
            return Ok(new List<Pet>());
        }
    }
}
