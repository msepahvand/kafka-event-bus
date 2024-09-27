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
				using (Process producerProcess = new())
				{
					producerProcess.StartInfo.FileName = $"{ClientsBasePath}\\Producer.exe";
					producerProcess.Start();
				}

				using Process consumerProcess = new();
				consumerProcess.StartInfo.FileName = $"{ClientsBasePath}\\Consumer.exe";
				consumerProcess.Start();

			}

			_ = Console.ReadKey();
		}
	}
}
