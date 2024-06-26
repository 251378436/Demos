﻿using Microsoft.Extensions.Logging;
using VehicleService.Infrastructure.Kafka;
using VehicleService.Infrastructure.Kafka.Options;
using VehicleService.Infrastructure.Kafka.Proxy.Producer;

namespace VehicleService.Infrastructure.UseCases.Sensor;

public interface ISensorKafkaProducer : IKafkaProducerProxy<string, string>
{

}

public class SensorKafkaProducer : KafkaProducerWithoutSchemaProxy, ISensorKafkaProducer
{
    public SensorKafkaProducer(ILogger<SensorKafkaProducer> logger,
        IKafkaProducersFactory kafkaProducersFactory,
        IKafkaConfigManager kafkaConfigManager) : base(logger, kafkaProducersFactory, kafkaConfigManager)
    {
    }

    protected override ProducerConfigOptions producerConfigOptions { get; set; } = new ProducerConfigOptions()
    {
        BootstrapServers = "localhost:9092",
        SaslUsername = "admin",
        SaslPassword = "admin-secret",
        EnableSslCertificateVerification = false
    };
}
