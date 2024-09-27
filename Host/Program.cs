using Configuration;
using Producer;
using System.Diagnostics;

namespace Clients
{
	public class Program
	{
		const string ClientsBasePath = "C:\\src\\kafka-event-bus\\Host\\bin\\Debug\\net8.0";
		const string Topic = "Payments";
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

			Enumerable.Range(0, 1000)
				.ToList()
				.ForEach(x =>
			{
				producer.Produce(Topic, producerConfig);
			});

			_ = Console.ReadKey();
		}
	}
}
