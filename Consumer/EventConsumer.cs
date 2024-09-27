﻿using Confluent.Kafka;

namespace Consumer
{
	public class EventConsumer
	{ 
		public void Consume(string topic, IEnumerable<KeyValuePair<string, string>> config)
		{
			// creates a new consumer instance
			using IConsumer<string, string> consumer = new ConsumerBuilder<string, string>(config).Build();
			consumer.Subscribe(topic);
			while (true)
			{
				// consumes messages from the subscribed topic and prints them to the console
				ConsumeResult<string, string> cr = consumer.Consume();
				Console.ForegroundColor = (ConsoleColor.Green);
				Console.WriteLine($"Consumed event from topic {topic}: key = {cr.Message.Key,-10} value = {cr.Message.Value}");
				Console.ForegroundColor = ConsoleColor.Black;
			}
		}
	}
}
