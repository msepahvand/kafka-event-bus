using Confluent.Kafka;

namespace Producer
{
	public class EventProducer
	{
		public void Produce(IEnumerable<ProduceMessageRequest> requests, IEnumerable<KeyValuePair<string, string>> config)
		{
			var producerConfig = config.Where(c =>
				   !c.Key.Equals("session.timeout.ms")
				&& !c.Key.Equals("group.id")
				&& !c.Key.Equals("auto.offset.reset"));

			// creates a new producer instance
			using IProducer<string, string> producer = new ProducerBuilder<string, string>(producerConfig).Build();

			// produces a sample message to the user-created topic and prints
			// a message when successful or an error occurs
			foreach (var request in requests) 
			{
				var message = new Message<string, string>
				{
					Key = request.Key,
					Value = request.Value
				};

				producer.Produce(request.Topic, message, deliveryReport =>
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(deliveryReport.Error.Code != ErrorCode.NoError
					   ? $"Failed to deliver message: {deliveryReport.Error.Reason}"
					   : $"Produced event to topic {request.Topic}: key = {deliveryReport.Message.Key,-10} value = {deliveryReport.Message.Value}");
				});
			}

			Console.ForegroundColor = ConsoleColor.Black;
			// send any outstanding or buffered messages to the Kafka broker
			producer.Flush(TimeSpan.FromSeconds(10));
		}
	}
}