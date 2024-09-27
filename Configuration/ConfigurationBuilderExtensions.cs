using Microsoft.Extensions.Configuration;

namespace Configuration
{
	public static class ConfigurationBuilderExtensions
	{
		public static IEnumerable<KeyValuePair<string, string>> LoadProducerConfiguration()
		{
			return Load()
				.Where(c =>
				   !c.Key.Equals("session.timeout.ms")
				&& !c.Key.Equals("group.id")
				&& !c.Key.Equals("auto.offset.reset"));
		}

		public static IEnumerable<KeyValuePair<string, string>> LoadConsumerConfiguration()
		{
			return Load();
		}

		private static IEnumerable<KeyValuePair<string, string>> Load()
		{
			var config = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddEnvironmentVariables(static (cfg) =>
			{
				cfg.Prefix = "KAFKA_CONFIG_PREFIX__";
			})
			.Build()
			.AsEnumerable()
			.Where(c => !string.IsNullOrWhiteSpace(c.Key) && !string.IsNullOrWhiteSpace(c.Value));

			return config!;
		}
	}
}
