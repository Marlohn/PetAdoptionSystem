using Microsoft.Extensions.Caching.Memory;
using PetAdoptionSystem.Domain.Interfaces;

namespace PetAdoptionSystem.Infra.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private const int CacheDurationMinutes = 10;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T? Get<T>(string key)
        {
            return _cache.TryGetValue(key, out T? value) ? value : default;
        }

        public void Set<T>(string key, T value)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheDurationMinutes)
            };
            _cache.Set(key, value, cacheEntryOptions);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}