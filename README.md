# kafka-event-bus
### Local setup
Set the Configuration values in `launchSettings.json`:
```
{
	"profiles": {
		"KafkaEventBus.Host.Console": {
			"commandName": "Project",
			"environmentVariables": {
				"KAFKA_CONFIG_PREFIX__bootstrap.servers": "p.......confluent.cloud:9092",
				"KAFKA_CONFIG_PREFIX__sasl.mechanisms": "PLAIN",
				"KAFKA_CONFIG_PREFIX__sasl.password": "",
				"KAFKA_CONFIG_PREFIX__sasl.username": "",
				"KAFKA_CONFIG_PREFIX__security.protocol": "SASL_SSL",
				"KAFKA_CONFIG_PREFIX__session.timeout.ms": "45000",
				"KAFKA_CONFIG_PREFIX__group.id": "csharp-group-1",
				"KAFKA_CONFIG_PREFIX__auto.offset.reset": "earliest"
			}
		}
	}
}
```
### Pub-Sub
![image](https://github.com/user-attachments/assets/38c6e214-5d87-4137-b361-17ba5b57ecf4)

### Streaming with Streamiz
![image](https://github.com/user-attachments/assets/722f9276-a49a-4aa3-9962-eb4e58eccfb3)
