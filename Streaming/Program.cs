using Configuration;

namespace Streaming
{

	public class Program
	{
		static async Task Main(string[] args)
		{
			var host = new StreamHost();

			var config = ConfigurationBuilderExtensions.LoadConsumerConfiguration();
			await host.StartAsync("payments", config);

			Console.ReadLine();
		}
	}
}
