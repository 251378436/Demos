using Confluent.Kafka;
using Confluent.SchemaRegistry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleService.Infrastructure.Kafka;

public interface IKafkaConfigManager
{
    ProducerConfig CreateProducerConfig(ProducerConfigOptions producerConfigOptions);
    SchemaRegistryConfig CreateSchemaRegistryConfig(SchemaRegistryConfigOptions options);
}

public class KafkaConfigManager : IKafkaConfigManager
{
    public ProducerConfig CreateProducerConfig(ProducerConfigOptions producerConfigOptions)
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
            //BootstrapServers = "localhost:9092",
            BootstrapServers = producerConfigOptions.BootstrapServers,
            SecurityProtocol = SecurityProtocol.SaslSsl,
            SaslMechanism = SaslMechanism.Plain,
            EnableSslCertificateVerification = false,
            //SaslUsername = "admin",
            //SaslPassword = "admin-secret",
            SaslUsername = producerConfigOptions.SaslUsername,
            SaslPassword = producerConfigOptions.SaslPassword
        };

        return producerConfig;
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
