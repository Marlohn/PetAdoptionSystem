using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetAdoptionSystem.Application.Dtos;
using PetAdoptionSystem.Application.Interfaces;

namespace PetAdoptionSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : Controller
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllPets()
        {
            var pets = await _petService.GetAllPetsAsync();

            return Ok(pets);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetPetById(Guid id)
        {
            var pet = await _petService.GetPetByIdAsync(id);

            if (pet == null)
            {
                return NotFound();
            }

            return Ok(pet);
        }

        [HttpPost]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> CreatePet([FromBody] PetRequestDto petDto)
        {
            var createdPet = await _petService.AddPetAsync(petDto);

            return CreatedAtAction(nameof(GetPetById), new { id = createdPet.Id }, createdPet);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> UpdatePet(Guid id, [FromBody] PetRequestDto petDto)
        {
            var updatedPet = await _petService.UpdatePetAsync(id, petDto);
            if (updatedPet == null)
            {
                return NotFound();
            }

            return Ok(updatedPet);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> DeletePet(Guid id)
        {
            var deleted = await _petService.DeletePetAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}