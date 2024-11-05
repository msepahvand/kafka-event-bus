using Configuration;
using Producer;
using System.Diagnostics;

namespace Clients
{
	public class Program
	{
		const string ClientsBasePath = "C:\\src\\kafka-event-bus\\Host\\bin\\Debug\\net8.0";
		const string Topic1 = "payments";
		const string Topic2 = "payment_status";
		public static void Main(string[] args)
		{
			var producerConfig = ConfigurationBuilderExtensions.LoadProducerConfiguration();
			var producer = new EventProducer();

			using (Process producerProcess = new())
			{
				producerProcess.StartInfo.FileName = $"{ClientsBasePath}\\Producer.exe";
				producerProcess.Start();
			}
			using (Process consumerProcess = new())
			{
				consumerProcess.StartInfo.FileName = $"{ClientsBasePath}\\Consumer.exe";
				consumerProcess.Start();
			}
			using (Process consumerProcess = new())
			{
				consumerProcess.StartInfo.FileName = $"{ClientsBasePath}\\Streaming.Host.Console.exe";
				consumerProcess.Start();
			}

			// Create some dummmy events
			List<ProduceMessageRequest> produceMessageRequests =
			[
				new ProduceMessageRequest
				{
					Key = "EventId",
					Topic = Topic1,
					Value = Guid.NewGuid().ToString()
				},
				new ProduceMessageRequest
				{
					Key = "EventId",
					Topic = Topic2,
					Value = DateTime.Now.ToFileTimeUtc().ToString()
				}
			];

			Enumerable.Range(0, 1000)
				.ToList()
				.ForEach(x =>
			{
				producer.Produce(produceMessageRequests, producerConfig);
			});

			_ = Console.ReadKey();
		}
	}
}
