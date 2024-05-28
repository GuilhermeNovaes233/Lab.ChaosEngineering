using Lab.ChaosEngineering.Domain.Entities;
using Lab.ChaosEngineering.Domain.Interfaces.Repository.Payments;
using Microsoft.EntityFrameworkCore;

namespace Lab.ChaosEngineering.Infra.Repositories.Payments
{
	public class PaymentRepository : RepositoryBase<Payment>, IPaymentRepository
	{
		public PaymentRepository(DbContext context) : base(context)
		{
		}
	}
}
