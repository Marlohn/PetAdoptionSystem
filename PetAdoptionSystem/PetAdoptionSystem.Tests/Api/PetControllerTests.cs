using Microsoft.AspNetCore.Mvc;
using Moq;
using PetAdoptionSystem.Api.Controllers;
using PetAdoptionSystem.Domain.Models;
using PetAdoptionSystem.Domain.Services;

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
            var pets = new List<Pet>
            {
                new Pet { Id = Guid.NewGuid(), Name = "Max", Type = "Dog", Sex = "Male", Breed = "Beagle" },
                new Pet { Id = Guid.NewGuid(), Name = "Luna", Type = "Dog", Sex = "Female", Breed = "Golden Retriever" },
                new Pet { Id = Guid.NewGuid(), Name = "Toby", Type = "Dog", Sex = "Male", Breed = "N/A" },
                new Pet { Id = Guid.NewGuid(), Name = "Lola", Type = "Cat", Sex = "Female", Breed = "N/A" },
            };

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