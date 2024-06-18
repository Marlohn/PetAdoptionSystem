using Microsoft.AspNetCore.Mvc;
using Moq;
using PetAdoptionSystem.Api.Controllers;
using PetAdoptionSystem.Application.Dtos;
using PetAdoptionSystem.Application.Interfaces;
using PetAdoptionSystem.Tests.Faker;

namespace PetAdoptionSystem.Tests
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
            var pets = FakeDataGenerator.PetDto.Generate(5);
            _petServiceMock.Setup(service => service.GetAllPetsAsync()).ReturnsAsync(pets);

            // Act
            var result = await _petController.GetAllPets();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<PetDto>>(okResult.Value);
            Assert.Equal(pets, returnValue);
        }

        [Fact]
        public async Task GetPetById_ReturnsOkResult_WithPet()
        {
            // Arrange
            var pet = FakeDataGenerator.PetDto.Generate();
            _petServiceMock.Setup(service => service.GetPetByIdAsync(pet.Id)).ReturnsAsync(pet);

            // Act
            var result = await _petController.GetPetById(pet.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PetDto>(okResult.Value);
            Assert.Equal(pet, returnValue);
        }

        [Fact]
        public async Task GetPetById_ReturnsNotFoundResult_WhenPetNotFound()
        {
            // Arrange
            var petId = Guid.NewGuid();
            _petServiceMock.Setup(service => service.GetPetByIdAsync(petId)).ReturnsAsync((PetDto?)null);

            // Act
            var result = await _petController.GetPetById(petId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreatePet_ReturnsCreatedAtActionResult_WithPet()
        {
            // Arrange
            var pet = FakeDataGenerator.PetDto.Generate();
            _petServiceMock.Setup(service => service.AddPetAsync(It.IsAny<PetDto>())).Returns(Task.CompletedTask);

            // Act
            var result = await _petController.CreatePet(pet);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<PetDto>(createdAtActionResult.Value);
            Assert.Equal(pet.Id, returnValue.Id);
        }


        [Fact]
        public async Task UpdatePet_ReturnsNoContentResult()
        {
            // Arrange
            var pet = FakeDataGenerator.PetDto.Generate();
            _petServiceMock.Setup(service => service.UpdatePetAsync(It.IsAny<PetDto>())).Returns(Task.CompletedTask);

            // Act
            var result = await _petController.UpdatePet(pet.Id, pet);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdatePet_ReturnsBadRequestResult_WhenIdsDoNotMatch()
        {
            // Arrange
            var pet = FakeDataGenerator.PetDto.Generate();
            var differentPetId = Guid.NewGuid();

            // Act
            var result = await _petController.UpdatePet(differentPetId, pet);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeletePet_ReturnsNoContentResult()
        {
            // Arrange
            var petId = Guid.NewGuid();
            _petServiceMock.Setup(service => service.DeletePetAsync(petId)).Returns(Task.CompletedTask);

            // Act
            var result = await _petController.DeletePet(petId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}