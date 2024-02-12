using Microsoft.Extensions.Caching.Memory;
using Products.Business.Contracts;
using Products.Data.Contracts;

namespace Products.Business.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly IProductRepository _productRepository;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public string GetStatusName(short status)
        {
            if (_cache.TryGetValue(status, out string statusName))
            {
                return statusName;
            }

            var statusDictionary = new Dictionary<short, string>
            {
                { 1, "Active" },
                { 0, "Inactive" }
            };

            if (statusDictionary.ContainsKey(status))
            {
                statusName = statusDictionary[status];

                _cache.Set(status, statusName, TimeSpan.FromMinutes(5));
            }
            else
            {
                statusName = "Unknown";
            }

            return statusName;
        }
    }
}
