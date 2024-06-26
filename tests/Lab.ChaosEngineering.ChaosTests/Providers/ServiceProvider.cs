﻿using Lab.ChaosEngineering.Domain.Cache;
using Lab.ChaosEngineering.Infra.Repositories.Cache;
using Lab.ChaosEngineering.Services.Interfaces;
using Lab.ChaosEngineering.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Contrib.Simmy;
using Polly.Contrib.Simmy.Outcomes;
using StackExchange.Redis;

namespace Lab.ChaosEngineering.ChaosTests.Providers
{
    public static class TestServiceProvider
	{
		public static ServiceProvider CreateServiceProvider(string redisConnectionString, double chaosInjectionRate = 0.5)
		{
			return new ServiceCollection()
				.AddLogging()
				.AddTransient<IRedisCacheRepository, RedisCacheRepository>(provider =>
				{
					// Cria a política de caos usando Simmy
					var chaosPolicy = MonkeyPolicy.InjectExceptionAsync(with =>
						with.Fault(new RedisConnectionException(ConnectionFailureType.UnableToConnect, "Simmy: Redis connection failed"))
							.InjectionRate(chaosInjectionRate) // 50% de chance de injeção de falha
							.Enabled());

					// Cria políticas de retry e timeout
					var retryPolicy = Policy.Handle<RedisConnectionException>()
						.Or<RedisTimeoutException>()
						.RetryAsync(3);

					var timeoutPolicy = Policy.TimeoutAsync<string>(TimeSpan.FromSeconds(3));

					// Combina as políticas de caos, retry e timeout
					var cachePolicy = Policy.WrapAsync(chaosPolicy, retryPolicy, (IAsyncPolicy)timeoutPolicy);

					return new RedisCacheRepository(redisConnectionString, cachePolicy);
				})
				.AddTransient<IPayPalPaymentService, PayPalPaymentService>()
				.BuildServiceProvider();
		}
	}
}