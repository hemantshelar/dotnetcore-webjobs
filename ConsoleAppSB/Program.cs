using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Configuration;

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
			   c.AddJsonFile("Myappsettings.json", optional: false, reloadOnChange: true);

		   });
			builder.ConfigureWebJobs(b =>
			{
				b.AddAzureStorageCoreServices();
				b.AddAzureStorage();
			});

			builder.ConfigureLogging((context, b) =>
			{
				b.AddConsole();
			});

			using (var host = builder.Build())
			{
				host.Run();
			}
		}
	}


	public static class Functions
	{
		public static void ProcessQueueMessage([QueueTrigger("myqueue")] string message, ILogger logger)
		{
			logger.LogInformation(message);
		}
	}
}
