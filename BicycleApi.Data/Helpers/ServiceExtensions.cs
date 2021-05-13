using BicycleApi.Data.Interfaces;
using BicycleApi.Data.Services;
using BicycleApi.DBData.Repository;

using Microsoft.Extensions.DependencyInjection;
using Sieve.Services;

namespace BicycleApi.Data.Helpers
{
	public static class ServiceExtensions
	{
		public static void AddServicesInjections(this IServiceCollection services)
		{
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped<IDetailService, DetailService>();
			services.AddScoped<IBrandService, BrandService>();
			services.AddScoped<ICountryService, CountryService>();
			services.AddScoped<SieveProcessor>();
		}
	}
}
