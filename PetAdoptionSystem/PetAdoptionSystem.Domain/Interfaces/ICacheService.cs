using Microsoft.Extensions.Caching.Memory;

namespace PetAdoptionSystem.Domain.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> createItem, MemoryCacheEntryOptions options);
        void Remove(string cacheKey);
    }
}