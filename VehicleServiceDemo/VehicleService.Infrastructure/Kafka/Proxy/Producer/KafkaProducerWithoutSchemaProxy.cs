using Confluent.Kafka;

namespace VehicleService.Infrastructure.Kafka.Proxy.Producer;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8600
#pragma warning disable CS8603

public abstract class KafkaProducerWithoutSchemaProxy : IKafkaProducerProxy<string, string>
{
    private IProducer<string, string> kafkaProducer;

    private readonly IKafkaProducersFactory kafkaProducersFactory;
    private readonly IKafkaConfigManager kafkaConfigManager;

    public KafkaProducerWithoutSchemaProxy(IKafkaProducersFactory kafkaProducersFactory, IKafkaConfigManager kafkaConfigManager)
    {
        this.kafkaProducersFactory = kafkaProducersFactory;
        this.kafkaConfigManager = kafkaConfigManager;
    }

    public Task<DeliveryResult<string, string>> ProduceAsync(string topic, Message<string, string> message, CancellationToken cancellationToken = default)
    {
        var producer = GetProducer();

        return producer.ProduceAsync(topic, message, cancellationToken);
    }

    public int Flush(TimeSpan timeout)
    {
        var producer = GetProducer();

        return producer.Flush(timeout);
    }

    protected virtual IProducer<string, string> GetProducer()
    {
        if (kafkaProducer == null)
        {
            var producerConfig = this.kafkaConfigManager.CreateProducerConfig(producerConfigOptions);
            kafkaProducer = this.kafkaProducersFactory.CreateProducerWithoutSchema(producerConfig);
        }
        return kafkaProducer;
    }

    protected abstract ProducerConfigOptions producerConfigOptions { get; set; }
}