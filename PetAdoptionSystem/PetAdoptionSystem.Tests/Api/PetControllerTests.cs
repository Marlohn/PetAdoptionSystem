using Microsoft.AspNetCore.Mvc;
using Moq;
using PetAdoptionSystem.Api.Controllers;
using PetAdoptionSystem.Domain.Models;
using PetAdoptionSystem.Domain.Services;
using PetAdoptionSystem.Tests.Faker;

namespace PetAdoptionSystem.Tests.Api
{
    public class PetControllerTests
    {
        private readonly PetController _petController;
        private readonly Mock<IPetService> _petServiceMock;

        public PetControllerTests()
        {
            _petServiceMock = new Mock<IPetService>();
            _petController = new PetController(_petServiceMock.Object);
        }

        [Fact]
        public async Task GetAllPets_ReturnsOkResult_WithListOfPets()
        {
            // Arrange
            List<Pet> pets = FakeDataGenerator.Pets.Generate(5);
            _petServiceMock.Setup(service => service.GetAllPetsAsync()).ReturnsAsync(pets);

            // Act
            var result = await _petController.GetAllPets();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Pet>>(okResult.Value);
            Assert.Equal(pets, returnValue);
        }
    }
}