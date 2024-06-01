using Lab.ChaosEngineering.ChaosTests.Providers;
using Lab.ChaosEngineering.Domain.Cache;
using Lab.ChaosEngineering.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lab.ChaosEngineering.ChaosTests.Payments
{
	public class PayPalPaymentServiceTests
	{
		private PayPalPaymentService _payPalPaymentService;

		public void Setup()
		{
			var serviceProvider = TestServiceProvider.CreateServiceProvider("redis-connection-string");

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