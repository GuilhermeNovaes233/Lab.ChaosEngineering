using Lab.ChaosEngineering.Domain.Cache;
using Lab.ChaosEngineering.Services.Interfaces;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Lab.ChaosEngineering.Services.Services
{
	public class PayPalPaymentService : IPayPalPaymentService
	{
		private const string CacheKeyPrefix = "PaymentLink";

		private readonly ILogger<PayPalPaymentService> _logger;
		private readonly IRedisCacheRepository _cacheRepository;

		public PayPalPaymentService(ILogger<PayPalPaymentService> logger, IRedisCacheRepository cacheRepository)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_cacheRepository = cacheRepository ?? throw new ArgumentNullException(nameof(_cacheRepository));
		}

		public async Task<string> GeneratePaymentLinkAsync(decimal amount)
		{
			try
			{
				_logger.LogInformation("Generating payment link for amount {Amount}.", amount);

				var cachedLink = await _cacheRepository.GetValueAsync(BuildCacheKey(amount));
				if (!string.IsNullOrEmpty(cachedLink))
				{
					return cachedLink;
				}

				//TODO - MOCK 
				var paymentLink = $"https://www.example.com/payment?amount={amount}";

 				await _cacheRepository.SetValueAsync(BuildCacheKey(amount), paymentLink);

				return paymentLink;
			}
			catch (RedisConnectionException)
			{
				_logger.LogWarning("Failed to access Redis cache, falling back to external service.");

				//TODO - MOCK 
				return $"https://www.example.com/payment?amount={amount}";
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to generate payment link for amount {Amount}.", amount);
				throw;
			}
		}

		public async Task<bool> ProcessPaymentFromQRCodeAsync(string qrCode)
		{
			try
			{
				_logger.LogInformation("Processing payment from QR code: {QRCode}.", qrCode);

				// Lógica para processar o pagamento do QR code...

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to process payment from QR code: {QRCode}.", qrCode);
				return false;
			}
		}

		private string BuildCacheKey(decimal amount) => $"{CacheKeyPrefix}_{amount}";
	}
}