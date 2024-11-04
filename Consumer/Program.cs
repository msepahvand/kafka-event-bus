using Configuration;

namespace Consumer
{
	public class Program
	{
		const string topic = "payments";
		static void Main(string[] args)
		{
			var config = ConfigurationBuilderExtensions.LoadConsumerConfiguration();
			Console.WriteLine("Start Consuming:");
			new EventConsumer().Consume(topic, config);			
		}
	}
}
