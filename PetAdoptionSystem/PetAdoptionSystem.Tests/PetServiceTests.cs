using Moq;
using PetAdoptionSystem.Application.Services;
using PetAdoptionSystem.Domain.Interfaces;
using PetAdoptionSystem.Domain.Models;
using PetAdoptionSystem.Tests.Faker;

namespace PetAdoptionSystem.Tests
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
        public async Task GetAllPetsAsync_ReturnsListOfPetDtos()
        {
            // Arrange
            var pets = FakeDataGenerator.Pet.Generate(5);

            _petRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(pets);

            // Act
            var result = await _petService.GetAllPetsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pets.Count, result.Count());

            for (int i = 0; i < pets.Count; i++)
            {
                var expectedPet = pets[i];
                var actualPet = result.ElementAt(i);

                Assert.Equal(expectedPet.Id, actualPet.Id);
                Assert.Equal(expectedPet.Name, actualPet.Name);
                Assert.Equal(expectedPet.Type, actualPet.Type);
                Assert.Equal(expectedPet.Breed, actualPet.Breed);
                Assert.Equal(expectedPet.Sex, actualPet.Sex);
            }
        }

        [Fact]
        public async Task GetPetByIdAsync_ReturnsPetDto()
        {
            // Arrange
            var pet = FakeDataGenerator.Pet.Generate();

            _petRepositoryMock.Setup(repo => repo.GetByIdAsync(pet.Id)).ReturnsAsync(pet);

            // Act
            var result = await _petService.GetPetByIdAsync(pet.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pet.Id, result.Id);
            Assert.Equal(pet.Name, result.Name);
            Assert.Equal(pet.Type, result.Type);
            Assert.Equal(pet.Breed, result.Breed);
            Assert.Equal(pet.Sex, result.Sex);
        }

        [Fact]
        public async Task AddPetAsync_CallsRepositoryCreateAsync()
        {
            // Arrange
            var petDto = FakeDataGenerator.PetDto.Generate();

            var pet = new Pet
            {
                Id = petDto.Id,
                Name = petDto.Name,
                Type = petDto.Type,
                Breed = petDto.Breed,
                Sex = petDto.Sex
            };

            _petRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Pet>())).Returns(Task.CompletedTask);

            // Act
            await _petService.AddPetAsync(petDto);

            // Assert
            _petRepositoryMock.Verify(repo => repo.CreateAsync(It.Is<Pet>(p =>
                p.Id == petDto.Id &&
                p.Name == petDto.Name &&
                p.Type == petDto.Type &&
                p.Breed == petDto.Breed &&
                p.Sex == petDto.Sex)), Times.Once);
        }

        [Fact]
        public async Task UpdatePetAsync_CallsRepositoryUpdateAsync()
        {
            // Arrange
            var petDto = FakeDataGenerator.PetDto.Generate();

            var pet = new Pet
            {
                Id = petDto.Id,
                Name = petDto.Name,
                Type = petDto.Type,
                Breed = petDto.Breed,
                Sex = petDto.Sex
            };

            _petRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Pet>())).Returns(Task.CompletedTask);

            // Act
            await _petService.UpdatePetAsync(pet.Id, petDto);

            // Assert
            _petRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Pet>(p =>
                p.Id == petDto.Id &&
                p.Name == petDto.Name &&
                p.Type == petDto.Type &&
                p.Breed == petDto.Breed &&
                p.Sex == petDto.Sex)), Times.Once);
        }

        [Fact]
        public async Task DeletePetAsync_CallsRepositoryDeleteAsync()
        {
            // Arrange
            var petId = Guid.NewGuid();
            _petRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);

            // Act
            await _petService.DeletePetAsync(petId);

            // Assert
            _petRepositoryMock.Verify(repo => repo.DeleteAsync(It.Is<Guid>(id => id == petId)), Times.Once);
        }
    }
}