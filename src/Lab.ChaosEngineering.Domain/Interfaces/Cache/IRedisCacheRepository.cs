using System;

namespace Lab.ChaosEngineering.Domain.Cache
{
	public interface IRedisCacheRepository
	{
		Task<string> GetValueAsync(string key);
		Task SetValueAsync(string key, string value, TimeSpan? expiry = null);
		Task<bool> KeyExistsAsync(string key);
		Task<bool> RemoveKeyAsync(string key);
	}
}