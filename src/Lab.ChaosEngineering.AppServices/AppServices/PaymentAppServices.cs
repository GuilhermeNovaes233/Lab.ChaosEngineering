using Lab.ChaosEngineering.AppServices.Dtos.Payments.Create;
using Lab.ChaosEngineering.AppServices.Dtos.Payments.Update;
using Lab.ChaosEngineering.AppServices.Dtos.Payments;
using Lab.ChaosEngineering.AppServices.Interfaces;
using Lab.ChaosEngineering.Domain.Entities;
using Lab.ChaosEngineering.Domain.Interfaces.Repository.Payments;
using Microsoft.Extensions.Logging;
using Lab.ChaosEngineering.AppServices.Mappers;

namespace Lab.ChaosEngineering.AppServices.AppServices
{
	public class PaymentAppService : IPaymentAppService
	{
		private readonly ILogger<PaymentAppService> _logger;
		private readonly IPaymentRepository _paymentRepository;

		public PaymentAppService(ILogger<PaymentAppService> logger, IPaymentRepository paymentRepository)
		{
			_logger = logger;
			_paymentRepository = paymentRepository;
		}

		public async Task<PaymentDTO> CreatePaymentAsync(CreatePaymentDTO request)
		{
			try
			{
				var payment = new Payment(request.InvoiceNumber, request.Amount, request.PaymentDate, request.Status);
				await _paymentRepository.AddAsync(payment);

				return PaymentMapper.ToPaymentDTO(payment.Id, payment); 
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while creating a payment.");
				throw;
			}
		}

		public async Task<PaymentDTO> GetPaymentByIdAsync(Guid paymentId)
		{
			try
			{
				var payment = await _paymentRepository.GetByIdAsync(paymentId);
				if (payment == null)
				{
					_logger.LogWarning($"Payment with ID {paymentId} not found.");
					return null;
				}
				return PaymentMapper.ToPaymentDTO(paymentId, payment);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"An error occurred while getting payment with ID {paymentId}.");
				throw;
			}
		}

		public async Task<PaymentDTO> UpdatePaymentAsync(Guid paymentId, UpdatePaymentDTO request)
		{
			try
			{
				var payment = await _paymentRepository.GetByIdAsync(paymentId);
				if (payment == null)
				{
					_logger.LogWarning($"Payment with ID {paymentId} not found.");
					return null;
				}

				payment = new Payment(request.InvoiceNumber, request.Amount, request.PaymentDate, request.Status);
				await _paymentRepository.UpdateAsync(payment);

				return PaymentMapper.ToPaymentDTO(paymentId, payment);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"An error occurred while updating payment with ID {paymentId}.");
				throw;
			}
		}
	}
}