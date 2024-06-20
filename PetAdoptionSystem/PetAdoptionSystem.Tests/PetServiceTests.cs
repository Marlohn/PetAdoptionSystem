using Moq;
using PetAdoptionSystem.Application.Services;
using PetAdoptionSystem.Domain.Interfaces;
using PetAdoptionSystem.Domain.Models;
using PetAdoptionSystem.Tests.Faker;

namespace PetAdoptionSystem.Tests.Services
{
    public class PetServiceTests
    {
        private readonly PetService _petService;
        private readonly Mock<IPetRepository> _petRepositoryMock;

        public PetServiceTests()
        {
            _petRepositoryMock = new Mock<IPetRepository>();
            _petService = new PetService(_petRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllPetsAsync_ReturnsListOfPets()
        {
            // Arrange
            var pets = FakeDataGenerator.Pet.Generate(3);
            _petRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(pets);

            // Act
            var result = await _petService.GetAllPetsAsync();

            // Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetPetByIdAsync_ReturnsPet_WhenPetExists()
        {
            // Arrange
            var pet = FakeDataGenerator.Pet.Generate();
            _petRepositoryMock.Setup(repo => repo.GetByIdAsync(pet.Id)).ReturnsAsync(pet);

            // Act
            var result = await _petService.GetPetByIdAsync(pet.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pet.Id, result.Id);
        }

        [Fact]
        public async Task GetPetByIdAsync_ReturnsNull_WhenPetDoesNotExist()
        {
            // Arrange
            var petId = Guid.NewGuid();
            _petRepositoryMock.Setup(repo => repo.GetByIdAsync(petId)).ReturnsAsync((Pet?)null);

            // Act
            var result = await _petService.GetPetByIdAsync(petId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddPetAsync_ReturnsCreatedPet()
        {
            // Arrange
            var petRequestDto = FakeDataGenerator.PetRequestDto.Generate();

            var petId = Guid.NewGuid();
            _petRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Pet>())).ReturnsAsync(petId);

            // Act
            var result = await _petService.AddPetAsync(petRequestDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(petId, result.Id);
        }

        [Fact]
        public async Task UpdatePetAsync_ReturnsUpdatedPet()
        {
            // Arrange
            var petRequestDto = FakeDataGenerator.PetRequestDto.Generate();
            var pet = FakeDataGenerator.Pet.Generate();
            _petRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Pet>())).ReturnsAsync(true);

            // Act
            var result = await _petService.UpdatePetAsync(pet.Id, petRequestDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pet.Id, result.Id);
        }

        [Fact]
        public async Task UpdatePetAsync_ReturnsNull_WhenPetDoesNotExist()
        {
            // Arrange
            var petRequestDto = FakeDataGenerator.PetRequestDto.Generate();
            var petId = Guid.NewGuid();
            _petRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Pet>())).ReturnsAsync(false);

            // Act
            var result = await _petService.UpdatePetAsync(petId, petRequestDto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeletePetAsync_ReturnsTrue_WhenPetExists()
        {
            // Arrange
            var petId = Guid.NewGuid();
            _petRepositoryMock.Setup(repo => repo.DeleteAsync(petId)).ReturnsAsync(true);

            // Act
            var result = await _petService.DeletePetAsync(petId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeletePetAsync_ReturnsFalse_WhenPetDoesNotExist()
        {
            // Arrange
            var petId = Guid.NewGuid();
            _petRepositoryMock.Setup(repo => repo.DeleteAsync(petId)).ReturnsAsync(false);

            // Act
            var result = await _petService.DeletePetAsync(petId);

            // Assert
            Assert.False(result);
        }
    }
}
