﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using PetAdoptionSystem.Api.Controllers;
using PetAdoptionSystem.Application.Dtos;
using PetAdoptionSystem.Application.Interfaces;
using PetAdoptionSystem.Domain.Interfaces;
using PetAdoptionSystem.Tests.Faker;

namespace PetAdoptionSystem.Tests
{
    public class PetControllerTests
    {
        private readonly PetsController _petController;
        private readonly Mock<IPetService> _petServiceMock;
        private readonly Mock<ICacheService> _cacheServiceMock;
        private const string CacheKey = "GetPets";

        public PetControllerTests()
        {
            _petServiceMock = new Mock<IPetService>();
            _cacheServiceMock = new Mock<ICacheService>();
            _petController = new PetsController(_petServiceMock.Object, _cacheServiceMock.Object);
        }

        [Fact]
        public async Task GetAllPets_ReturnsOkResult_WithListOfPets_FromCache()
        {
            // Arrange
            var pets = FakeDataGenerator.PetResponseDto.Generate(3);
            _cacheServiceMock.Setup(cache => cache.Get<List<PetResponseDto>>(CacheKey)).Returns(pets);

            // Act
            var result = await _petController.GetAllPets();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<PetResponseDto>>(okResult.Value);
            Assert.Equal(3, returnValue.Count);
            _cacheServiceMock.Verify(cache => cache.Get<List<PetResponseDto>>(CacheKey), Times.Once);
            _petServiceMock.Verify(service => service.GetAllPetsAsync(), Times.Never);
        }

        [Fact]
        public async Task GetAllPets_ReturnsOkResult_WithListOfPets_FromService_WhenCacheIsEmpty()
        {
            // Arrange
            var pets = FakeDataGenerator.PetResponseDto.Generate(3);
            _cacheServiceMock.Setup(cache => cache.Get<List<PetResponseDto>>(CacheKey)).Returns((List<PetResponseDto>?)null);
            _petServiceMock.Setup(service => service.GetAllPetsAsync()).ReturnsAsync(pets);

            // Act
            var result = await _petController.GetAllPets();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<PetResponseDto>>(okResult.Value);
            Assert.Equal(3, returnValue.Count);
            _cacheServiceMock.Verify(cache => cache.Get<List<PetResponseDto>>(CacheKey), Times.Once);
            _petServiceMock.Verify(service => service.GetAllPetsAsync(), Times.Once);
            _cacheServiceMock.Verify(cache => cache.Set(CacheKey, pets), Times.Once);
        }

        [Fact]
        public async Task GetPetById_ReturnsOkResult_WithPet()
        {
            // Arrange
            var pet = FakeDataGenerator.PetResponseDto.Generate();
            var petId = pet.Id;
            _petServiceMock.Setup(service => service.GetPetByIdAsync(petId)).ReturnsAsync(pet);

            // Act
            var result = await _petController.GetPetById(petId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PetResponseDto>(okResult.Value);
            Assert.Equal(pet.Id, returnValue.Id);
        }

        [Fact]
        public async Task GetPetById_ReturnsNotFoundResult_WhenPetIsNull()
        {
            // Arrange
            var petId = Guid.NewGuid();
            _petServiceMock.Setup(service => service.GetPetByIdAsync(petId)).ReturnsAsync((PetResponseDto?)null);

            // Act
            var result = await _petController.GetPetById(petId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreatePet_ReturnsCreatedAtActionResult_WithPet()
        {
            // Arrange
            var petRequestDto = FakeDataGenerator.PetRequestDto.Generate();
            var petResponseDto = FakeDataGenerator.PetResponseDto.Generate();
            _petServiceMock.Setup(service => service.AddPetAsync(It.IsAny<PetRequestDto>())).ReturnsAsync(petResponseDto);

            // Act
            var result = await _petController.CreatePet(petRequestDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<PetResponseDto>(createdAtActionResult.Value);
            Assert.Equal(petResponseDto.Id, returnValue.Id);
            _cacheServiceMock.Verify(cache => cache.Remove(CacheKey), Times.Once);
        }

        [Fact]
        public async Task UpdatePet_ReturnsOkResult_WithUpdatedPet()
        {
            // Arrange
            var petRequestDto = FakeDataGenerator.PetRequestDto.Generate();
            var petResponseDto = FakeDataGenerator.PetResponseDto.Generate();
            var petId = petResponseDto.Id;

            _petServiceMock.Setup(service => service.UpdatePetAsync(petId, It.IsAny<PetRequestDto>())).ReturnsAsync(petResponseDto);

            // Act
            var result = await _petController.UpdatePet(petId, petRequestDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PetResponseDto>(okResult.Value);
            Assert.Equal(petResponseDto.Id, returnValue.Id);
            _cacheServiceMock.Verify(cache => cache.Remove(CacheKey), Times.Once);
        }

        [Fact]
        public async Task UpdatePet_ReturnsNotFoundResult_WhenPetIsNull()
        {
            // Arrange
            var petRequestDto = FakeDataGenerator.PetRequestDto.Generate();
            var petId = Guid.NewGuid();
            _petServiceMock.Setup(service => service.UpdatePetAsync(petId, It.IsAny<PetRequestDto>())).ReturnsAsync((PetResponseDto?)null);

            // Act
            var result = await _petController.UpdatePet(petId, petRequestDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeletePet_ReturnsNoContentResult()
        {
            // Arrange
            var petId = Guid.NewGuid();
            _petServiceMock.Setup(service => service.DeletePetAsync(petId)).ReturnsAsync(true);

            // Act
            var result = await _petController.DeletePet(petId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _cacheServiceMock.Verify(cache => cache.Remove(CacheKey), Times.Once);
        }

        [Fact]
        public async Task DeletePet_ReturnsNotFoundResult_WhenPetDoesNotExist()
        {
            // Arrange
            var petId = Guid.NewGuid();
            _petServiceMock.Setup(service => service.DeletePetAsync(petId)).ReturnsAsync(false);

            // Act
            var result = await _petController.DeletePet(petId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}