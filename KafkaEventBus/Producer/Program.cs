using Configuration;
using Microsoft.Extensions.Configuration;

namespace Producer
{
	public class Program
	{
		const string topic = "Payments";
		static void Main(string[] args)
		{
			IConfiguration config = ConfigurationBuilderExtensions.Load();
			Console.WriteLine("Start Producing:");
			new EventProducer().Produce(topic, config);
		}
	}
}
