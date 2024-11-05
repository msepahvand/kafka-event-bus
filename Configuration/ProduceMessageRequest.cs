namespace Configuration
{
	public record ProduceMessageRequest
	{
		public string Key { get; set; }
		public string Value { get; set; }
		public string Topic { get; set; }
	}
}
