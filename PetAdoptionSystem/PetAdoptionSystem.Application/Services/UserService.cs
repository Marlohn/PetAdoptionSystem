using Microsoft.IdentityModel.Tokens;
using PetAdoptionSystem.Application.Dtos;
using PetAdoptionSystem.Application.Interfaces;
using PetAdoptionSystem.Domain.Interfaces;
using PetAdoptionSystem.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

            var id = await _userRepository.AddAsync(user);

            return new UserResponseDto()
            {
                Id = id,
                Role = DefaultRole,
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
                Password = user.Password,
                Role = user.Role
            };
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