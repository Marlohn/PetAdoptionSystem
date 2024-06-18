using Microsoft.AspNetCore.Mvc;
using PetAdoptionSystem.Application.Dtos;
using PetAdoptionSystem.Application.Interfaces;

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
            var pets = await _petService.GetAllPetsAsync();
            return Ok(pets);
        }

        [HttpGet("{id}")]
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
        public async Task<IActionResult> CreatePet([FromBody] PetDto petDto)
        {
            await _petService.AddPetAsync(petDto);
            return CreatedAtAction(nameof(GetPetById), new { id = petDto.Id }, petDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePet(Guid id, [FromBody] PetDto petDto)
        {
            if (id != petDto.Id)
            {
                return BadRequest();
            }

            await _petService.UpdatePetAsync(petDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(Guid id)
        {
            await _petService.DeletePetAsync(id);

            return NoContent();
        }
    }
}