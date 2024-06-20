using System.Data.SqlClient;
using Moq;
using PetAdoptionSystem.Domain.Models;
using PetAdoptionSystem.Infra.Repositories;
using PetAdoptionSystem.Tests.Faker;

namespace PetAdoptionSystem.Tests
{
    public class PetRepositoryTests
    {
        private readonly Mock<IDatabaseExecutor> _mockDatabaseExecutor;
        private readonly PetRepository _repository;

        public PetRepositoryTests()
        {
            _mockDatabaseExecutor = new Mock<IDatabaseExecutor>();
            _repository = new PetRepository(_mockDatabaseExecutor.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsListOfPets()
        {
            // Arrange
            var pets = FakeDataGenerator.Pet.Generate(3);
            _mockDatabaseExecutor.Setup(exec => exec.ExecuteQueryAsync<Pet>(It.IsAny<string>(), null)).ReturnsAsync(pets);

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pets.Count, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsPet()
        {
            // Arrange
            var pets = FakeDataGenerator.Pet.Generate(1);

            _mockDatabaseExecutor.Setup(exec => exec.ExecuteQueryAsync<Pet>(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).ReturnsAsync(pets);

            // Act
            var result = await _repository.GetByIdAsync(pets.Single().Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pets.Single().Id, result.Id);
        }

        [Fact]
        public async Task CreateAsync_ReturnsNewPetId()
        {
            // Arrange
            var pet = FakeDataGenerator.Pet.Generate();
            var newPetId = Guid.NewGuid();

            _mockDatabaseExecutor.Setup(exec => exec.ExecuteScalarAsync<Guid>(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).ReturnsAsync(newPetId);

            // Act
            var result = await _repository.CreateAsync(pet);

            // Assert
            Assert.Equal(newPetId, result);
        }

        [Fact]
        public async Task CreateAsync_ThrowsException_WhenInsertFails()
        {
            // Arrange
            var pet = FakeDataGenerator.Pet.Generate();

            _mockDatabaseExecutor.Setup(exec => exec.ExecuteScalarAsync<Guid>(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).ReturnsAsync(Guid.Empty);

            // Act
            var action = _repository.CreateAsync(pet);

            // Assert
            await Assert.ThrowsAsync<Exception>(() => action);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsTrue_WhenUpdateSucceeds()
        {
            // Arrange
            var pet = FakeDataGenerator.Pet.Generate();

            _mockDatabaseExecutor.Setup(exec => exec.ExecuteNonQueryAsync(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).ReturnsAsync(1);

            // Act
            var result = await _repository.UpdateAsync(pet);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalse_WhenUpdateFails()
        {
            // Arrange
            var pet = FakeDataGenerator.Pet.Generate();

            _mockDatabaseExecutor.Setup(exec => exec.ExecuteNonQueryAsync(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).ReturnsAsync(0);

            // Act
            var result = await _repository.UpdateAsync(pet);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue_WhenDeleteSucceeds()
        {
            // Arrange
            var petId = Guid.NewGuid();

            _mockDatabaseExecutor.Setup(exec => exec.ExecuteNonQueryAsync(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).ReturnsAsync(1);

            // Act
            var result = await _repository.DeleteAsync(petId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenDeleteFails()
        {
            // Arrange
            var petId = Guid.NewGuid();

            _mockDatabaseExecutor.Setup(exec => exec.ExecuteNonQueryAsync(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).ReturnsAsync(0);

            // Act
            var result = await _repository.DeleteAsync(petId);

            // Assert
            Assert.False(result);
        }
    }
}