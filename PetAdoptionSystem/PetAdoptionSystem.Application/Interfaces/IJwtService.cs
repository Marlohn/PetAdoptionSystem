namespace PetAdoptionSystem.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Guid id, string username, string role);
    }
}