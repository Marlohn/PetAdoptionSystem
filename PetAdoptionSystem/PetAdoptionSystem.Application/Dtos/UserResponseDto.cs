namespace PetAdoptionSystem.Application.Dtos
{
    public class UserResponseDto : UserRequestDto
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
    }
}