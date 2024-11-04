using Confluent.Kafka;

namespace Consumer
{
	public class EventConsumer
	{ 
		public void Consume(string topic1, string topic2, string topic3, IEnumerable<KeyValuePair<string, string>> config)
		{
			// creates a new consumer instance
			using IConsumer<string, string> consumer = new ConsumerBuilder<string, string>(config).Build();
			consumer.Subscribe(topic1);
			consumer.Subscribe(topic2);
			consumer.Subscribe(topic3);

			while (true)
			{
				// consumes messages from the subscribed topic and prints them to the console
				ConsumeResult<string, string> cr1 = consumer.Consume();
				Console.ForegroundColor = (ConsoleColor.Green);
				Console.WriteLine($"Consumed event from topic {topic1}: key = {cr1.Message.Key,-10} value = {cr1.Message.Value}");
				Console.ForegroundColor = ConsoleColor.Black;

				// consumes messages from the subscribed topic and prints them to the console
				ConsumeResult<string, string> cr2 = consumer.Consume();
				Console.ForegroundColor = (ConsoleColor.Green);
				Console.WriteLine($"Consumed event from topic {topic2}: key = {cr2.Message.Key,-10} value = {cr2.Message.Value}");
				Console.ForegroundColor = ConsoleColor.Black;

				// consumes messages from the subscribed topic and prints them to the console
				ConsumeResult<string, string> cr3 = consumer.Consume();
				Console.ForegroundColor = (ConsoleColor.Green);
				Console.WriteLine($"Consumed event from topic {topic3}: key = {cr3.Message.Key,-10} value = {cr3.Message.Value}");
				Console.ForegroundColor = ConsoleColor.Black;
			}
		}
	}
}
