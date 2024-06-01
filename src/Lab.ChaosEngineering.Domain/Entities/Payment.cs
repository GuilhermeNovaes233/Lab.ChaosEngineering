using Lab.ChaosEngineering.Domain.Enums;
using System;

namespace Lab.ChaosEngineering.Domain.Entities
{
	public class Payment : EntityBase
	{
        public Payment()  { }

		public Payment(string invoiceNumber, decimal amount, DateTime paymentDate, PaymentStatus status)
		{
			InvoiceNumber = invoiceNumber;
			Amount = amount;
			PaymentDate = paymentDate;
			Status = status;
		}

		public string InvoiceNumber { get; private set; } = string.Empty;
		public decimal Amount { get; private set; } = decimal.Zero;
		public DateTime PaymentDate { get; private set; } = DateTime.Now;
		public PaymentStatus Status { get; private set; } = PaymentStatus.Pending;

		public override bool IsValid()
		{
			throw new NotImplementedException();
		}
	}
}