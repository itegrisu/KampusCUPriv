using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Abstractions.Redis
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IConnectionMultiplexer _mux;
        private readonly IDatabase _db;

        public RedisCacheService(IConnectionMultiplexer mux)
        {
            _mux = mux;
            _db = mux.GetDatabase();
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var data = await _db.StringGetAsync(key);
            if (!data.HasValue) return default;
            return JsonSerializer.Deserialize<T>(data!);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var json = JsonSerializer.Serialize(value);
            return _db.StringSetAsync(key, json, expiry);
        }

        public Task RemoveAsync(string key) => _db.KeyDeleteAsync(key);

        public async Task RemoveByPattern(string pattern)
        {
            // örn pattern = "Clubs_"  -> silinecek anahtarlar Clubs_*
            foreach (var ep in _mux.GetEndPoints())
            {
                var server = _mux.GetServer(ep);
                await foreach (var key in server.KeysAsync(_db.Database, pattern + "*"))
                    await _db.KeyDeleteAsync(key).ConfigureAwait(false);
            }
        }
    }
}
