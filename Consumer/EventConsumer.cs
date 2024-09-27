﻿using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace Consumer
{
	public class EventConsumer
	{
		public void Consume(string topic, IConfiguration config)
		{
			config["group.id"] = "csharp-group-1";
			config["auto.offset.reset"] = "earliest";

			// creates a new consumer instance
			using var consumer = new ConsumerBuilder<string, string>(config.AsEnumerable()).Build();
			consumer.Subscribe(topic);
			while (true)
			{
				// consumes messages from the subscribed topic and prints them to the console
				var cr = consumer.Consume();
				Console.WriteLine($"Consumed event from topic {topic}: key = {cr.Message.Key,-10} value = {cr.Message.Value}");
			}

			// closes the consumer connection
			consumer.Close();
		}
	}
}
