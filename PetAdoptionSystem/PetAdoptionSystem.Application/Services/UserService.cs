using PetAdoptionSystem.Application.Dtos;
using PetAdoptionSystem.Application.Interfaces;
using PetAdoptionSystem.Domain.Interfaces;
using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponseDto> Register(UserRequestDto userDto)
        {
            var user = new User()
            {
                Username = userDto.Username,
                Password = userDto.Password
            };

            var id = await _userRepository.AddAsync(user);

            return new UserResponseDto()
            {
                Id = id,
                Username = userDto.Username,
                Password = userDto.Password
            };
        }

        public async Task<UserResponseDto?> GetUserById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            return new UserResponseDto()
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password
            };
        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAndPasswordAsync(username, password);

            if (user == null)
            {
                return string.Empty;
            }

            // Generate JWT token
            return "generated-jwt-token"; // Replace with actual token generation logic
        }


    }
}