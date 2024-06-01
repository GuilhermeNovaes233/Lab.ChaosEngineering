using Lab.ChaosEngineering.Domain.Cache;
using Polly;
using Polly.Timeout;
using Polly.Wrap;
using StackExchange.Redis;

namespace Lab.ChaosEngineering.Infra.Cache
{
	public class RedisCacheRepository : IRedisCacheRepository
	{
		private readonly ConnectionMultiplexer _redisConnection;
		private readonly AsyncPolicyWrap _cachePolicy;

		public RedisCacheRepository(string connectionString, AsyncPolicyWrap cachePolicy)
		{
			_redisConnection = ConnectionMultiplexer.Connect(connectionString);
			_cachePolicy = cachePolicy;
		}

		public async Task<string?> GetValueAsync(string key)
		{
			return await _cachePolicy.ExecuteAsync(async () =>
			{
				var db = _redisConnection.GetDatabase();
				return await db.StringGetAsync(key);
			});
		}

		public async Task SetValueAsync(string key, string value, TimeSpan? expiry = null)
		{
			await _cachePolicy.ExecuteAsync(async () =>
			{
				var db = _redisConnection.GetDatabase();
				await db.StringSetAsync(key, value, expiry);
			});
		}

		public async Task<bool> KeyExistsAsync(string key)
		{
			return await _cachePolicy.ExecuteAsync(async () =>
			{
				var db = _redisConnection.GetDatabase();
				return await db.KeyExistsAsync(key);
			});
		}

		public async Task<bool> RemoveKeyAsync(string key)
		{
			return await _cachePolicy.ExecuteAsync(async () =>
			{
				var db = _redisConnection.GetDatabase();
				return await db.KeyDeleteAsync(key);
			});
		}
	}
}