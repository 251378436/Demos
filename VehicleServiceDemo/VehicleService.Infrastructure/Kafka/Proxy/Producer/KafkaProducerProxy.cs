using Confluent.Kafka;
using VehicleService.Infrastructure.Kafka.Options;

namespace VehicleService.Infrastructure.Kafka.Proxy.Producer;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8600
#pragma warning disable CS8603

public abstract class KafkaProducerProxy<TKey, TValue> : IKafkaProducerProxy<TKey, TValue>
{
    private IProducer<TKey, TValue> kafkaProducer;

    private readonly IKafkaProducersFactory kafkaProducersFactory;
    private readonly IKafkaConfigManager kafkaConfigManager;

    public KafkaProducerProxy(IKafkaProducersFactory kafkaProducersFactory, IKafkaConfigManager kafkaConfigManager)
    {
        this.kafkaProducersFactory = kafkaProducersFactory;
        this.kafkaConfigManager = kafkaConfigManager;
    }

    public Task<DeliveryResult<TKey, TValue>> ProduceAsync(string topic, Message<TKey, TValue> message, CancellationToken cancellationToken = default)
    {
        var producer = GetProducer();

        return producer.ProduceAsync(topic, message, cancellationToken);
    }

    public int Flush(TimeSpan timeout)
    {
        var producer = GetProducer();

        return producer.Flush(timeout);
    }

    protected virtual IProducer<TKey, TValue> GetProducer()
    {
        if(kafkaProducer == null)
        {
            var producerConfig = this.kafkaConfigManager.CreateProducerConfig(producerConfigOptions);
            var schemaConfig = this.kafkaConfigManager.CreateSchemaRegistryConfig(schemaRegistryConfigOptions);
            kafkaProducer = this.kafkaProducersFactory.CreateProducerWithSchema<TKey, TValue>(producerConfig, schemaConfig);
        }
        return kafkaProducer;
    }

    protected abstract ProducerConfigOptions producerConfigOptions { get; set; }
    protected abstract SchemaRegistryConfigOptions schemaRegistryConfigOptions { get; set; }
}