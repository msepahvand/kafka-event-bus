﻿using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace Producer
{
	public class EventProducer
	{
		public void Produce(string topic, IConfiguration config)
		{
			// creates a new producer instance
			using IProducer<string, string> producer = new ProducerBuilder<string, string>(config.AsEnumerable()).Build();
			// produces a sample message to the user-created topic and prints
			// a message when successful or an error occurs
			var message = new Message<string, string>
			{
				Key = "EventId",
				Value = $"{Guid.NewGuid()}"

			};
			Console.ForegroundColor = (ConsoleColor.Red);

			producer.Produce(topic, message, deliveryReport =>
			{
				Console.WriteLine(deliveryReport.Error.Code != ErrorCode.NoError
				   ? $"Failed to deliver message: {deliveryReport.Error.Reason}"
				   : $"Produced event to topic {topic}: key = {deliveryReport.Message.Key,-10} value = {deliveryReport.Message.Value}");
			});
			Console.ForegroundColor = (ConsoleColor.Black);
			// send any outstanding or buffered messages to the Kafka broker
			producer.Flush(TimeSpan.FromSeconds(10));
		}
	}
}
