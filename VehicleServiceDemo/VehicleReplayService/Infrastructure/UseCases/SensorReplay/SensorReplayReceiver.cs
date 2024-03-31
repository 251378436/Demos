using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json.Linq;
using VehicleService.Infrastructure.Kafka;
using VehicleService.Infrastructure.Kafka.Options;
using VehicleService.Infrastructure.Kafka.Proxy.Producer;
namespace VehicleReplayService.Infrastructure.UseCases.SensorReplay;

public interface ISensorReplayReceiver : IKafkaConsumerProxy<string, string>
{

}

public class SensorReplayReceiver : KafkaConsumerWithoutSchemaProxy, ISensorReplayReceiver
{
    public SensorReplayReceiver(IKafkaConsumersFactory kafkaConsumersFactory,
        IKafkaConfigManager kafkaConfigManager) : base(kafkaConsumersFactory, kafkaConfigManager)
    {

    }

    protected override string topic { get; set; } = "vehicle.service.sensor.request";
    protected override ConsumerConfigOptions consumerConfigOptions { get; set; } = new ConsumerConfigOptions()
    {
        BootstrapServers = "localhost:9092",
        SaslUsername = "admin",
        SaslPassword = "admin-secret",
        GroupId = "test-comsumer-group",
        EnableSslCertificateVerification = false
    };

}
