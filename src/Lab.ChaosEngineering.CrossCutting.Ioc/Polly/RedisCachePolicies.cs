using Polly;
using Polly.Timeout;
using Polly.Wrap;
using StackExchange.Redis;

namespace Lab.ChaosEngineering.Services.Polly
{
	public static class RedisCachePolicies
	{
		public static AsyncPolicyWrap GetDefaultRedisCachePolicy()
		{
			return Policy
					.TimeoutAsync(TimeSpan.FromSeconds(3))
					.WrapAsync(Policy.Handle<TimeoutRejectedException>()
						.Or<RedisConnectionException>()
						.Or<RedisTimeoutException>()
						.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));
		}

		public static AsyncPolicyWrap GetRedisCachePolicyWithRetry(int retryCount, Func<int, TimeSpan> sleepDurationProvider)
		{
			return Policy
					.TimeoutAsync(TimeSpan.FromSeconds(3))
					.WrapAsync(Policy.Handle<TimeoutRejectedException>()
						.Or<RedisConnectionException>()
						.Or<RedisTimeoutException>()
						.WaitAndRetryAsync(retryCount, retryAttempt => sleepDurationProvider(retryAttempt)));
		}

		public static AsyncPolicyWrap GetRedisCachePolicyWithFallback(Func<CancellationToken, Task> fallbackAction)
		{
			return Policy
					.TimeoutAsync(TimeSpan.FromSeconds(3))
					.WrapAsync(Policy.Handle<TimeoutRejectedException>()
						.Or<RedisConnectionException>()
						.Or<RedisTimeoutException>()
						.FallbackAsync(fallbackAction));

		}
	}
}