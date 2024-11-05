using Configuration;

namespace Producer
{
	public class Program
	{
		const string topic1 = "payments";
		const string topic2 = "payment_status";

		static List<ProduceMessageRequest> produceMessageRequests =
               [new ProduceMessageRequest
				{
					Key = "EventId",
					Topic = topic1,
					Value = Guid.NewGuid().ToString()
				},
				new ProduceMessageRequest
				{
					Key = "EventId",
					Topic = topic2,
					Value = DateTime.Now.ToFileTimeUtc().ToString()
				}];

		static void Main(string[] args)
		{
			var config = ConfigurationBuilderExtensions.LoadProducerConfiguration();
			Console.WriteLine("Start Producing:");
			new EventProducer().Produce(produceMessageRequests, config);
		}
	}
}
