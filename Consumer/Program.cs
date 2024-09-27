using Configuration;
using Microsoft.Extensions.Configuration;

namespace Consumer
{
	public class Program
	{
		const string topic = "Payments";
		static void Main(string[] args)
		{
			IConfiguration config = ConfigurationBuilderExtensions.Load();
			Console.WriteLine("Start Consuming:");
			new EventConsumer().Consume(topic, config);			
		}
	}
}
