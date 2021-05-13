using System;
using System.Reflection;
using Elastic.Apm.SerilogEnricher;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.File;

namespace BicycleApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var qwe = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}-{DateTime.UtcNow:yyyy-MM}";
			Log.Logger = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.Enrich.WithElasticApmCorrelationInfo()
				.MinimumLevel.Override("Microsoft", LogEventLevel.Error)
				.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
				{
					AutoRegisterTemplate = true,
					IndexFormat = $"syslog-{DateTime.UtcNow:yyyy-MM}",
					AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
					ModifyConnectionSettings = x => x.BasicAuthentication("user", "wqe"),
					CustomFormatter = new ElasticsearchJsonFormatter(),
					FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
					EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
					                   EmitEventFailureHandling.WriteToFailureSink |
					                   EmitEventFailureHandling.RaiseCallback,
					FailureSink = new FileSink("./failures.txt", new JsonFormatter(), null)
				})
				.CreateLogger();
			try
			{
				Log.Information("Starting up");
				CreateHostBuilder(args).Build().Run();
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Application start-up failed");
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.UseSerilog()
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
