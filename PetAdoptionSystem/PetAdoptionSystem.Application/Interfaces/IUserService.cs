using PetAdoptionSystem.Application.Dtos;

namespace PetAdoptionSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> Register(UserRequestDto user);
        Task<UserResponseDto?> GetUserById(Guid id);
        Task<string> Login(string username, string password);
    }
}