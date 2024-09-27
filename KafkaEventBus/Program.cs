using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace Clients
{
    class Program
    {
        public static IConfiguration ReadConfig()
        {
            // reads the client configuration from client.properties
            // and returns it as a configuration object
            return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddIniFile("client.properties", false)
            .Build();
        }

        public static void Produce(string topic, IConfiguration config)
        {
            // creates a new producer instance
            using var producer = new ProducerBuilder<string, string>(config.AsEnumerable()).Build();
            // produces a sample message to the user-created topic and prints
            // a message when successful or an error occurs
            producer.Produce(topic, new Message<string, string> { Key = "key", Value = "value" },
                deliveryReport =>
                {
                    Console.WriteLine(deliveryReport.Error.Code != ErrorCode.NoError
                        ? $"Failed to deliver message: {deliveryReport.Error.Reason}"
                        : $"Produced event to topic {topic}: key = {deliveryReport.Message.Key,-10} value = {deliveryReport.Message.Value}");
                }
            );

            // send any outstanding or buffered messages to the Kafka broker
            producer.Flush(TimeSpan.FromSeconds(10));
        }

        public static void Consume(string topic, IConfiguration config)
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

        public static void Main(string[] args)
        {
            // producer and consumer code here
            IConfiguration config = ReadConfig();
            const string topic = "Payments";

            Produce(topic, config);
            Consume(topic, config);
        }
    }
}
