using PetAdoptionSystem.Application.Dtos;
using PetAdoptionSystem.Application.Interfaces;
using PetAdoptionSystem.Domain.Interfaces;
using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<UserResponseDto> Register(UserRequestDto userDto)
        {
            const string DefaultRole = "user";

            var user = new User()
            {
                Username = userDto.Username,
                Password = userDto.Password,
                Role = DefaultRole
            };

            user.Id = await _userRepository.AddAsync(user);

            return UserResponseDto.Map(user);
        }

        public async Task<UserResponseDto?> GetUserById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            return UserResponseDto.Map(user);
        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAndPasswordAsync(username, password);

            if (user == null)
            {
                return string.Empty;
            }

            return _jwtService.GenerateToken(username, user.Role);
        }
    }
}