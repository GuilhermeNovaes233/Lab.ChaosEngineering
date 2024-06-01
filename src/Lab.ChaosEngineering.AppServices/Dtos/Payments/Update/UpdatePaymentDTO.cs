using Lab.ChaosEngineering.Domain.Enums;

namespace Lab.ChaosEngineering.AppServices.Dtos.Payments.Update
{
	public class UpdatePaymentDTO
	{
		public string InvoiceNumber { get; set; }
		public decimal Amount { get; set; }
		public DateTime PaymentDate { get; set; }
		public PaymentStatus Status { get; set; }
	}
}