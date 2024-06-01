using Lab.ChaosEngineering.Domain.Cache;
using Lab.ChaosEngineering.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Lab.ChaosEngineering.Services.Services
{
	public class PayPalPaymentService : IPayPalPaymentService
	{
		private const string CacheKeyPrefix = "PaymentLink";

		private readonly ILogger<PayPalPaymentService> _logger;
		private readonly IRedisCacheRepository _cacheService;

		public PayPalPaymentService(ILogger<PayPalPaymentService> logger, IRedisCacheRepository cacheService)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
		}

		public async Task<string> GeneratePaymentLinkAsync(decimal amount)
		{
			try
			{
				_logger.LogInformation("Generating payment link for amount {Amount}.", amount);

				var cachedLink = await _cacheService.GetValueAsync(BuildCacheKey(amount));
				if (!string.IsNullOrEmpty(cachedLink))
				{
					return cachedLink;
				}

				//TODO - ADICIONAR CHAMADA A SERVIÇO EXTERNO
				var paymentLink = $"https://www.paypal.com/payment?amount={amount}";

				await _cacheService.SetValueAsync(BuildCacheKey(amount), paymentLink, TimeSpan.FromHours(1));

				return paymentLink;
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

		private string BuildCacheKey(decimal amount)
		{
			return $"{CacheKeyPrefix}_{amount}";
		}
	}
}