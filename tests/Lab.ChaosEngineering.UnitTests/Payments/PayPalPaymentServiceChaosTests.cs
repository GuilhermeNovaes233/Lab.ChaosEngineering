using Lab.ChaosEngineering.Domain.Cache;
using Lab.ChaosEngineering.Infra.Cache;
using Lab.ChaosEngineering.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Contrib.Simmy;
using Polly.Contrib.Simmy.Outcomes;
using StackExchange.Redis;

namespace Lab.ChaosEngineering.ChaosTests.Payments
{
	public class PayPalPaymentServiceTests
	{
		private PayPalPaymentService _payPalPaymentService;

		//TODO - PROVISORIO VOU SEPARAR EM OUTRA CLASSE
		public void Setup()
		{
			var serviceProvider = new ServiceCollection()
				.AddLogging(builder =>
				{
					builder.SetMinimumLevel(LogLevel.Debug);
				})
				.AddTransient<IRedisCacheRepository, RedisCacheRepository>(provider =>
				{
					var connectionString = "your-redis-connection-string";

					// Cria a política de caos usando Simmy
					var chaosPolicy = MonkeyPolicy.InjectExceptionAsync(with =>
						with.Fault(new RedisConnectionException(ConnectionFailureType.UnableToConnect, "Simmy: Redis connection failed"))
							.InjectionRate(0.5) // 50% de chance de injeção de falha
							.Enabled());

					// Cria políticas de retry e timeout
					var retryPolicy = Policy.Handle<RedisConnectionException>()
						.Or<RedisTimeoutException>()
						.RetryAsync(3);

					var timeoutPolicy = Policy.TimeoutAsync<string>(TimeSpan.FromSeconds(3));

					// Combina as políticas de caos, retry e timeout
					var cachePolicy = Policy.WrapAsync(chaosPolicy, retryPolicy, (IAsyncPolicy)timeoutPolicy);

					return new RedisCacheRepository(connectionString, cachePolicy);
				})
				.AddTransient<PayPalPaymentService>()
				.BuildServiceProvider();

			var logger = serviceProvider.GetService<ILogger<PayPalPaymentService>>();
			var redisCacheRepository = serviceProvider.GetService<IRedisCacheRepository>();

			_payPalPaymentService = new PayPalPaymentService(logger, redisCacheRepository);
		}

		[Fact]
		public async Task GeneratePaymentLink_ReturnsLinkFromExternalService_WhenRedisChaosOccurs()
		{
			// Arrange
			decimal amount = 100;
			string expectedLinkFromExternalService = $"https://www.paypal.com/payment?amount={amount}";

			// Act
			var result = await _payPalPaymentService.GeneratePaymentLinkAsync(amount);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(expectedLinkFromExternalService, result);
		}
	}
}