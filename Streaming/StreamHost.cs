using Streamiz.Kafka.Net.SerDes;
using Streamiz.Kafka.Net.Stream;
using Streamiz.Kafka.Net.Table;
using Streamiz.Kafka.Net;
using Confluent.Kafka;
using Configuration;

namespace Streaming
{
	public class StreamHost
	{
		public StreamHost() { }

		public async Task StartAsync(string leftTopic, string rightTopic, string destinationTopic, IEnumerable<KeyValuePair<string, string>> cfg)
		{
			var config = new StreamConfig<StringSerDes, StringSerDes>();
			config.ApplicationId = "payments-test-streaming-app";
			config.BootstrapServers = cfg.Get("bootstrap.servers");
			config.SecurityProtocol = SecurityProtocol.SaslPlaintext;
			config.SecurityProtocol = SecurityProtocol.SaslSsl;
			config.AutoOffsetReset = AutoOffsetReset.Earliest;
			config.CommitIntervalMs = 10 * 1000;
			config.NumStreamThreads = 1;
			config.SaslPassword = cfg.Get("sasl.password");
			config.SaslUsername = cfg.Get("sasl.username");
			config.SaslMechanism = SaslMechanism.Plain;
			config.AllowAutoCreateTopics = false;
			StreamBuilder builder = new StreamBuilder();

			var kstream = builder.Stream<string, string>(leftTopic);
			var ktable = builder.Table(rightTopic, InMemory.As<string, string>("table-store"));

			kstream.Join(ktable, (v, v1) => $"{v}-aggregated-{v1}")
				   .To(destinationTopic);

			Topology t = builder.Build();
			KafkaStream stream = new KafkaStream(t, config);

			Console.CancelKeyPress += (o, e) =>
			{
				stream.Dispose();
			};

			await stream.StartAsync();
		}
	}
}
