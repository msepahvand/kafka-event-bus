using Streamiz.Kafka.Net.SerDes;
using Streamiz.Kafka.Net.Stream;
using Streamiz.Kafka.Net.Table;
using Streamiz.Kafka.Net;
using Confluent.Kafka;

namespace Streaming
{
	public class StreamHost
	{
		public StreamHost() { }

		public async Task StartAsync(string topic1, string topic2, IEnumerable<KeyValuePair<string, string>> cfg)
		{
			var config = new StreamConfig<StringSerDes, StringSerDes>();
			config.ApplicationId = "payments-test-streaming-app";
			config.BootstrapServers = cfg.Single(x => x.Key == "bootstrap.servers").Value;
			config.SecurityProtocol = SecurityProtocol.SaslPlaintext;
			config.SecurityProtocol = SecurityProtocol.SaslSsl;
			config.AutoOffsetReset = AutoOffsetReset.Earliest;
			config.CommitIntervalMs = 10 * 1000;

			config.NumStreamThreads = 1;
			config.SaslPassword = cfg.Single(x => x.Key == "sasl.password").Value;
			config.SaslUsername = cfg.Single(x => x.Key == "sasl.username").Value;
			config.SaslMechanism = SaslMechanism.Plain;
			config.AllowAutoCreateTopics = false;
			StreamBuilder builder = new StreamBuilder();

			var kstream = builder.Stream<string, string>(topic1);
			var ktable = builder.Table(topic2, InMemory.As<string, string>("table-store"));

			kstream.Join(ktable, (v, v1) => $"{v}-aggregated-{v1}")
				   .To("payments_aggregated");

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
