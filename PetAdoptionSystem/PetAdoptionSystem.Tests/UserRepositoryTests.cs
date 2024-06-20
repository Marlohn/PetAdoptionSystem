using System.Data.SqlClient;
using Moq;
using PetAdoptionSystem.Domain.Interfaces;
using PetAdoptionSystem.Domain.Models;
using PetAdoptionSystem.Infra.Repositories;
using PetAdoptionSystem.Tests.Faker;

namespace PetAdoptionSystem.Tests
{
    public class UserRepositoryTests
    {
        private readonly Mock<IDatabaseExecutorService> _mockDatabaseExecutor;
        private readonly UserRepository _repository;

        public UserRepositoryTests()
        {
            _mockDatabaseExecutor = new Mock<IDatabaseExecutorService>();
            _repository = new UserRepository(_mockDatabaseExecutor.Object);
        }

        [Fact]
        public async Task AddAsync_ReturnsNewUserId()
        {
            // Arrange
            var user = FakeDataGenerator.User.Generate();
            var newUserId = Guid.NewGuid();

            _mockDatabaseExecutor.Setup(exec => exec.ExecuteScalarAsync<Guid>(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).ReturnsAsync(newUserId);

            // Act
            var result = await _repository.AddAsync(user);

            // Assert
            Assert.Equal(newUserId, result);
        }

        [Fact]
        public async Task AddAsync_ThrowsException_WhenInsertFails()
        {
            // Arrange
            var user = FakeDataGenerator.User.Generate();

            _mockDatabaseExecutor.Setup(exec => exec.ExecuteScalarAsync<Guid>(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).ReturnsAsync(Guid.Empty);

            // Act
            var action = _repository.AddAsync(user);

            // Assert
            await Assert.ThrowsAsync<Exception>(() => action);
        }

        [Fact]
        public async Task GetByUsernameAndPasswordAsync_ReturnsUser()
        {
            // Arrange
            var user = FakeDataGenerator.User.Generate();

            _mockDatabaseExecutor.Setup(exec => exec.ExecuteQueryAsync<User>(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).ReturnsAsync(new List<User> { user });

            // Act
            var result = await _repository.GetByUsernameAndPasswordAsync(user.Username, user.Password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
        }

        [Fact]
        public async Task GetByUsernameAndPasswordAsync_ReturnsNull_WhenUserNotFound()
        {
            // Arrange
            _mockDatabaseExecutor.Setup(exec => exec.ExecuteQueryAsync<User>(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).ReturnsAsync(new List<User>());

            // Act
            var result = await _repository.GetByUsernameAndPasswordAsync("nonexistentuser", "wrongpassword");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsUser()
        {
            // Arrange
            var user = FakeDataGenerator.User.Generate();

            _mockDatabaseExecutor.Setup(exec => exec.ExecuteQueryAsync<User>(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).ReturnsAsync(new List<User> { user });

            // Act
            var result = await _repository.GetByIdAsync(user.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenUserNotFound()
        {
            // Arrange
            _mockDatabaseExecutor.Setup(exec => exec.ExecuteQueryAsync<User>(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).ReturnsAsync(new List<User>());

            // Act
            var result = await _repository.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }
    }
}