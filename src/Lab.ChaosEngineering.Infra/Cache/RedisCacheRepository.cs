using Lab.ChaosEngineering.Domain.Cache;
using StackExchange.Redis;

namespace Lab.ChaosEngineering.Infra.Cache
{
	public class RedisCacheRepository : IRedisCacheRepository
	{
		private readonly ConnectionMultiplexer _redisConnection;

		public RedisCacheRepository(string connectionString)
		{
			_redisConnection = ConnectionMultiplexer.Connect(connectionString);
		}

		public async Task<string> GetValueAsync(string key)
		{
			var db = _redisConnection.GetDatabase();
			return await db.StringGetAsync(key);
		}

		public async Task SetValueAsync(string key, string value, TimeSpan? expiry = null)
		{
			var db = _redisConnection.GetDatabase();
			await db.StringSetAsync(key, value, expiry);
		}

		public async Task<bool> KeyExistsAsync(string key)
		{
			var db = _redisConnection.GetDatabase();
			return await db.KeyExistsAsync(key);
		}

		public async Task<bool> RemoveKeyAsync(string key)
		{
			var db = _redisConnection.GetDatabase();
			return await db.KeyDeleteAsync(key);
		}
	}
}