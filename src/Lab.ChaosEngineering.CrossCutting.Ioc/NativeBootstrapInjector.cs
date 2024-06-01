using Lab.ChaosEngineering.AppServices.AppServices;
using Lab.ChaosEngineering.AppServices.Interfaces;
using Lab.ChaosEngineering.Domain.Cache;
using Lab.ChaosEngineering.Domain.Interfaces.Repository.Payments;
using Lab.ChaosEngineering.Infra.Cache;
using Lab.ChaosEngineering.Infra.Context;
using Lab.ChaosEngineering.Infra.Repositories.Payments;
using Lab.ChaosEngineering.Services.Interfaces;
using Lab.ChaosEngineering.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 

namespace Lab.ChaosEngineering.CrossCutting.Ioc
{
	public static class NativeBootstrapInjector
	{
		public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
		{
			//DB
			services.AddDbContext<DataContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			//Infra
			services.AddScoped<IPaymentRepository, PaymentRepository>();
			services.AddScoped<IRedisCacheRepository, RedisCacheRepository>();

			//AppServices
			services.AddScoped<IPaymentAppService, PaymentAppService>();

			//Services
			services.AddScoped<IPayPalPaymentService, PayPalPaymentService>();
		}
	}
}