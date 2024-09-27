using Microsoft.Extensions.Configuration;

namespace Configuration
{
	public static class ConfigurationBuilderExtensions
	{
		public static IConfiguration Load()
		{
			// reads the client configuration from client.properties
			// and returns it as a configuration object
			return new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddIniFile("client.properties", false)
			.Build();
		}
	}
}
