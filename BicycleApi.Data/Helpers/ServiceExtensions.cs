using BicycleApi.Data.Services;
using BicycleApi.DBData.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BicycleApi.Data.Helpers
{
	public static class ServiceExtensions
	{
		public static void AddServicesInjections(this IServiceCollection services)
		{
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped<DetailService>();
		}
	}
}
