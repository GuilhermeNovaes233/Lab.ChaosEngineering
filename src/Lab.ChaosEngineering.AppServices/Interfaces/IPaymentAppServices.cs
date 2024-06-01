using Lab.ChaosEngineering.AppServices.Dtos.Payments.Create;
using Lab.ChaosEngineering.AppServices.Dtos.Payments.Update;
using Lab.ChaosEngineering.AppServices.Dtos.Payments;

namespace Lab.ChaosEngineering.AppServices.Interfaces
{
	public interface IPaymentAppService
	{
		Task<PaymentDTO> CreatePaymentAsync(CreatePaymentDTO request);
		Task<PaymentDTO> GetPaymentByIdAsync(Guid paymentId);
		Task<PaymentDTO> UpdatePaymentAsync(Guid paymentId, UpdatePaymentDTO request);
	}
}