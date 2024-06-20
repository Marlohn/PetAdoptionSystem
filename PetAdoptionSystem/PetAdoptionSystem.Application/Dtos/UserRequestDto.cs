using System.ComponentModel.DataAnnotations;

namespace PetAdoptionSystem.Application.Dtos
{
    public class UserRequestDto
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 50 characters.")]
        public string Password { get; set; }
    }
}