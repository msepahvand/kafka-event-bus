using Configuration;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace Clients
{
	public class Program
	{
		const string ClientsBasePath = "C:\\src\\kafka-event-bus\\KafkaEventBus\\bin\\Debug\\net8.0\\";
		public static void Main(string[] args)
		{			
			IConfiguration config = ConfigurationBuilderExtensions.Load();

			Console.WriteLine("Press ESC to stop");

			while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
			{
				using (var producerProcess = new Process())
				{
					producerProcess.StartInfo.FileName = $"{ClientsBasePath}\\Producer.exe";
					producerProcess.Start();
				}

				using (var consumerProcess = new Process())
				{
					consumerProcess.StartInfo.FileName = $"{ClientsBasePath}\\Consumer.exe";
					consumerProcess.Start();
				}

			}

			_ = Console.ReadKey();
		}
	}
}
