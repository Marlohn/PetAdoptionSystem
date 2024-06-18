using Microsoft.AspNetCore.Mvc;
using PetAdoptionSystem.Api.Controllers;
using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Tests.Api
{
    public class PetControllerTests
    {
        private readonly PetController _petController;

        public PetControllerTests()
        {
            _petController = new PetController();
        }

        [Fact]
        public async Task GetAllPets_ReturnsOkResult_WithListOfPets()
        {
            // Arrange


            // Act
            var result = await _petController.GetAllPets();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

        }
    }
}