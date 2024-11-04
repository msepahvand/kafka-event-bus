using Configuration;

namespace Producer
{
	public class Program
	{
		const string topic = "payments";
		static void Main(string[] args)
		{
			var config = ConfigurationBuilderExtensions.LoadProducerConfiguration();
			Console.WriteLine("Start Producing:");
			new EventProducer().Produce(topic, config);
		}
	}
}
