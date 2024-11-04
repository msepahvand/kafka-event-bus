using Confluent.Kafka;

namespace Producer
{
	public class EventProducer
	{
		public void Produce(string topic1, string topic2, IEnumerable<KeyValuePair<string, string>> config)
		{
			var producerConfig = config.Where(c => 
			       !c.Key.Equals("session.timeout.ms")
				&& !c.Key.Equals("group.id")
				&& !c.Key.Equals("auto.offset.reset"));

			// creates a new producer instance
			using IProducer<string, string> producer = new ProducerBuilder<string, string>(producerConfig).Build();
			// produces a sample message to the user-created topic and prints
			// a message when successful or an error occurs
			var message1 = new Message<string, string>
			{
				Key = "EventId",
				Value = $"{Guid.NewGuid()}"
			};

			var message2 = new Message<string, string>
			{
				Key = "EventId",
				Value = $"{DateTime.UtcNow.ToFileTimeUtc()}"
			};

			producer.Produce(topic1, message1, deliveryReport =>
			{
				Console.ForegroundColor = (ConsoleColor.Red);
				Console.WriteLine(deliveryReport.Error.Code != ErrorCode.NoError
				   ? $"Failed to deliver message: {deliveryReport.Error.Reason}"
				   : $"Produced event to topic {topic1}: key = {deliveryReport.Message.Key,-10} value = {deliveryReport.Message.Value}");
			});

			producer.Produce(topic2, message2, deliveryReport =>
			{
				Console.ForegroundColor = (ConsoleColor.Red);
				Console.WriteLine(deliveryReport.Error.Code != ErrorCode.NoError
				   ? $"Failed to deliver message: {deliveryReport.Error.Reason}"
				   : $"Produced event to topic {topic2}: key = {deliveryReport.Message.Key,-10} value = {deliveryReport.Message.Value}");
			});

			Console.ForegroundColor = (ConsoleColor.Black);
			// send any outstanding or buffered messages to the Kafka broker
			producer.Flush(TimeSpan.FromSeconds(10));
		}
	}
}
