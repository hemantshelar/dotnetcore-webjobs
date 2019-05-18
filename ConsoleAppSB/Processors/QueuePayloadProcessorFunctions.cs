using ConsoleAppSB.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppSB.Processors
{
	public class QueuePayloadProcessorFunctions
	{
		private readonly IDataStore dataStore;
		public QueuePayloadProcessorFunctions(IDataStore dataStore)
		{
			this.dataStore = dataStore;
		}
		public void ProcessQueueMessage([QueueTrigger("myqueue")] string message, ILogger logger)
		{
			logger.LogCritical(message);
			this.dataStore.ProcessData();
		}

		public void SBTopicListener([ServiceBusTrigger("testqueue", Connection = "AzureWebJobsServiceBus")] string myQueueItem , Int32 deliveryCount, string messageId, DateTime enqueuedTimeUtc,  ILogger log)
		{
			Console.WriteLine("data received.");
		}
	}
}
