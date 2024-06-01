using Lab.ChaosEngineering.Domain.Enums;

namespace Lab.ChaosEngineering.AppServices.Dtos.Payments.Create
{
	public class CreatePaymentDTO
	{
		public string InvoiceNumber { get; set; }
		public decimal Amount { get; set; }
		public DateTime PaymentDate { get; set; }
		public PaymentStatus Status { get; set; }
	}
}