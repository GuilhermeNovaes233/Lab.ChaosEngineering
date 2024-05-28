using System;

namespace Lab.ChaosEngineering.Domain.Entities
{
	public class Payment : EntityBase
	{
        public Payment()  { }

        public Payment(string invoiceNumber, decimal amount, DateTime paymentDate)
		{
			InvoiceNumber = invoiceNumber;
			Amount = amount;
			PaymentDate = paymentDate;
		}

		public string InvoiceNumber { get; private set; } = string.Empty;
		public decimal Amount { get; private set; } = decimal.Zero;
		public DateTime PaymentDate { get; private set; } = DateTime.Now;

		public override bool IsValid()
		{
			throw new NotImplementedException();
		}
	}
}