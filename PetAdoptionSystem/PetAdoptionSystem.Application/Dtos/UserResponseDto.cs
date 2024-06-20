using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Application.Dtos
{
    public class UserResponseDto : UserRequestDto
    {
        public Guid Id { get; set; }
        public string Role { get; set; }

        public static UserResponseDto Map(User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Role = user.Role,
                Username = user.Username,
                Password = user.Password
            };
        }
    }
}