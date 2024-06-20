using Microsoft.Extensions.Caching.Memory;
using PetAdoptionSystem.Domain.Interfaces;

namespace PetAdoptionSystem.Infra.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> createItem, MemoryCacheEntryOptions options)
        {
            if (!_memoryCache.TryGetValue(cacheKey, out T cacheEntry))
            {
                cacheEntry = await createItem();
                _memoryCache.Set(cacheKey, cacheEntry, options);
            }

            return cacheEntry;
        }

        public void Remove(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }
    }
}