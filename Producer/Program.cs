using Configuration;

namespace Producer
{
	public class Program
	{
		const string topic1 = "payments";
		const string topic2 = "payment_status";
		static void Main(string[] args)
		{
			var config = ConfigurationBuilderExtensions.LoadProducerConfiguration();
			Console.WriteLine("Start Producing:");
			new EventProducer().Produce(topic1, topic2, config);
		}
	}
}
