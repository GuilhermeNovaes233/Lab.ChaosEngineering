using Lab.ChaosEngineering.Domain.Enums; 

namespace Lab.ChaosEngineering.AppServices.Dtos.Payments
{
	public class PaymentDTO
	{
		public Guid PaymentId { get; set; }
		public string InvoiceNumber { get; set; }
		public decimal Amount { get; set; }
		public DateTime PaymentDate { get; set; }
		public PaymentStatus Status { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
	}
}