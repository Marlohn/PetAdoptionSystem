using PetAdoptionSystem.Domain.Models;

namespace PetAdoptionSystem.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> AddAsync(User user);
        Task<User?> GetByUsernameAndPasswordAsync(string username, string password);
        Task<User?> GetByIdAsync(Guid id);
    }
}