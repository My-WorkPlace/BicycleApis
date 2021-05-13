using System;
using BicycleApi.Data.Helpers;
using BicycleApi.DBData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BicycleApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AppDBContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection"),
					b => b.MigrationsAssembly("BicycleApi")));

			services.AddControllers();
			services.AddServicesInjections();
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.AddSwaggerGen();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseSerilogRequestLogging(); // <-- Add this line

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app
				.UseDeveloperExceptionPage()
				.UseSwagger()
				.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"));

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
