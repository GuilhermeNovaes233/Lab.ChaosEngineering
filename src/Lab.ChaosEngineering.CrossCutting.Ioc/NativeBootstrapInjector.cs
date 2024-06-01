using Lab.ChaosEngineering.AppServices.AppServices;
using Lab.ChaosEngineering.AppServices.Interfaces;
using Lab.ChaosEngineering.Domain.Interfaces.Repository.Payments;
using Lab.ChaosEngineering.Infra.Repositories.Payments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lab.ChaosEngineering.CrossCutting.Ioc
{
	public static class NativeBootstrapInjector
	{
		public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
		{
			//AppServices
			services.AddScoped<IPaymentAppService, PaymentAppService>();

			// Infra 
			services.AddScoped<IPaymentRepository, PaymentRepository>();
		}
	}
}