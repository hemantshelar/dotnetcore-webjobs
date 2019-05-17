using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ConsoleAppSB.Interfaces;
using ConsoleAppSB.Services;
using Microsoft.Azure.WebJobs.Host;

namespace ConsoleAppSB
{
	class Program
	{
		static void Main(string[] args)
		{
			HostBuilder builder = new HostBuilder();

			//Below piece of code is not required if name of json file is 'appsettings.json'
			builder.ConfigureAppConfiguration(c =>
		   {
			   c.AddJsonFile("myAppsettings.json", optional: true, reloadOnChange: true);
			   c.AddEnvironmentVariables();
		   })
			.ConfigureWebJobs(b =>
			{
				b.AddAzureStorageCoreServices();
				b.AddAzureStorage();
				b.Services.AddSingleton<IDataStore, DataStore>();
			})
			.ConfigureLogging((context, b) =>
			{
				b.AddConsole();
			});

			using (var host = builder.Build())
			{
				host.Run();
			}
		}
	}


	//public class Functions
	//{
	//	private readonly IDataStore dataStore;
	//	public Functions(IDataStore dataStore)
	//	{
	//		this.dataStore = dataStore;
	//	}
	//	public void ProcessQueueMessage([QueueTrigger("myqueue")] string message, ILogger logger)
	//	{
	//		logger.LogInformation(message);
	//		this.dataStore.ProcessData();
	//	}
	//}
}
