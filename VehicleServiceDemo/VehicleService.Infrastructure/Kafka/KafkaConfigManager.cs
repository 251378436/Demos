using Confluent.Kafka;
using Confluent.SchemaRegistry;

namespace VehicleService.Infrastructure.Kafka;

public interface IKafkaConfigManager
{
    ProducerConfig CreateProducerConfig(ProducerConfigOptions options);
    ConsumerConfig CreateConsumerConfig(ConsumerConfigOptions options);
    SchemaRegistryConfig CreateSchemaRegistryConfig(SchemaRegistryConfigOptions options);
}

public class KafkaConfigManager : IKafkaConfigManager
{
    public ProducerConfig CreateProducerConfig(ProducerConfigOptions options)
    {
        IDictionary<string, string> config = new Dictionary<string, string>();
        config.Add("sasl.mechanism", "PLAIN");
        config.Add("acks", "1");
        config.Add("batch.size", "50000");
        config.Add("linger.ms", "15");
        config.Add("compression.type", "snappy");
        config.Add("retries", "50");
        config.Add("message.timeout.ms", "300000");
        config.Add("message.send.max.retries", "50");
        config.Add("retry.backoff.ms", "250");

        var producerConfig = new ProducerConfig(config)
        {
            BootstrapServers = options.BootstrapServers,
            SecurityProtocol = SecurityProtocol.SaslSsl,
            SaslMechanism = SaslMechanism.Plain,
            EnableSslCertificateVerification = false,
            SaslUsername = options.SaslUsername,
            SaslPassword = options.SaslPassword
        };

        return producerConfig;
    }

    public ConsumerConfig CreateConsumerConfig(ConsumerConfigOptions options)
    {
        var config = new ConsumerConfig();
        config.BootstrapServers = options.BootstrapServers;
        config.SecurityProtocol = SecurityProtocol.SaslSsl;
        config.SaslMechanism = SaslMechanism.Plain;
        config.EnableSslCertificateVerification = false;
        config.SaslUsername = options.SaslUsername;
        config.SaslPassword = options.SaslPassword;

        config.GroupId = options.GroupId;
        config.EnableAutoOffsetStore = false;
        config.EnableAutoCommit = false;
        config.AutoOffsetReset = AutoOffsetReset.Earliest;
        config.EnablePartitionEof = true;
        config.PartitionAssignmentStrategy = PartitionAssignmentStrategy.CooperativeSticky;

        return config;
    }

    public SchemaRegistryConfig CreateSchemaRegistryConfig(SchemaRegistryConfigOptions options)
    {
        var schemaRegistryConfig = new SchemaRegistryConfig
        {
            Url = options.Url,
            BasicAuthUserInfo = $"{options.Username}:{options.Password}"
        };

        return schemaRegistryConfig;
    }
}
