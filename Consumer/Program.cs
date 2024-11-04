using Configuration;

namespace Consumer
{
	public class Program
	{
		const string topic1 = "payments";
		const string topic2 = "payment_status";
		const string topic3 = "payments_aggregated";
		static void Main(string[] args)
		{
			var config = ConfigurationBuilderExtensions.LoadConsumerConfiguration();
			Console.WriteLine("Start Consuming:");
			new EventConsumer().Consume(topic1, topic2, topic3, config);
		}
	}
}
