using VehicleService.Infrastructure.Kafka.Proxy.Producer;
using VehicleService.Infrastructure.Kafka;
using vehicle.sensor.massage.replay;
using VehicleService.Infrastructure.Kafka.Options;

namespace VehicleReplayService.Infrastructure.UseCases.SensorReplay;

public interface ISensorReplayKafkaProducer : IKafkaProducerProxy<SensorMessageKey, SensorMessageValue>
{

}

public class SensorReplayKafkaProducer : KafkaProducerProxy<SensorMessageKey, SensorMessageValue>, ISensorReplayKafkaProducer
{
    public SensorReplayKafkaProducer(IKafkaProducersFactory kafkaProducersFactory,
        IKafkaConfigManager kafkaConfigManager) : base(kafkaProducersFactory, kafkaConfigManager)
    {
    }

    protected override ProducerConfigOptions producerConfigOptions { get; set; } = new ProducerConfigOptions()
    {
        BootstrapServers = "localhost:9092",
        SaslUsername = "admin",
        SaslPassword = "admin-secret",
        EnableSslCertificateVerification = false
    };

    protected override SchemaRegistryConfigOptions schemaRegistryConfigOptions { get; set; } = new SchemaRegistryConfigOptions()
    {
        Url = "http://localhost:8081",
        Username = "ckp_tester",
        Password = "test_secret",
    };
}
