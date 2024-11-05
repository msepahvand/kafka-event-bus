using Confluent.Kafka;

namespace Consumer
{
	public class EventConsumer
	{
		public void Consume(IEnumerable<string> topics, IEnumerable<KeyValuePair<string, string>> config)
		{
			// creates a new consumer instance
			using IConsumer<string, string> consumer = new ConsumerBuilder<string, string>(config)
				.Build();

			foreach (var topic in topics)
			{
				consumer.Subscribe(topic);
			}

			while (true)
			{
				foreach (var topic in topics)
				{
					// consumes messages from the subscribed topic and prints them to the console
					ConsumeResult<string, string> cr = consumer.Consume();
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine($"Consumed event from topic {topic}: key = {cr.Message.Key,-10} value = {cr.Message.Value}");
					Console.ForegroundColor = ConsoleColor.Black;
				}
			}
		}
	}
}
