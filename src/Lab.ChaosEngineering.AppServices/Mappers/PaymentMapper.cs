using Lab.ChaosEngineering.AppServices.Dtos.Payments;
using Lab.ChaosEngineering.Domain.Entities;

namespace Lab.ChaosEngineering.AppServices.Mappers
{
	public static class PaymentMapper
	{
		public static PaymentDTO ToPaymentDTO(Guid paymentId, Payment payment)
		{
			return new PaymentDTO
			{
				PaymentId = paymentId,
				InvoiceNumber = payment.InvoiceNumber,
				Amount = payment.Amount,
				PaymentDate = payment.PaymentDate,
				Status = payment.Status,
				CreatedDate = payment.CreatedDate, 
				UpdatedDate = payment.UpdatedDate
			};
		}
	}
}