using Confluent.Kafka;

namespace VehicleService.Infrastructure.Kafka.Proxy.Producer;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8600
#pragma warning disable CS8603

public abstract class KafkaConsumerProxy<TKey, TValue> : IKafkaConsumerProxy<TKey, TValue>
{
    private IConsumer<TKey, TValue> kafkaConsumer;

    private readonly IKafkaConsumersFactory kafkaConsumersFactory;
    private readonly IKafkaConfigManager kafkaConfigManager;

    public KafkaConsumerProxy(IKafkaConsumersFactory kafkaConsumersFactory,
        IKafkaConfigManager kafkaConfigManager)
    {
        this.kafkaConsumersFactory = kafkaConsumersFactory;
        this.kafkaConfigManager = kafkaConfigManager;
    }

    public async Task<IEnumerable<ConsumeResult<TKey, TValue>>> ReceiveMessagesAsync(CancellationToken cancellationToken = default)
    {
        var consumer = GetConsumer();
        var list = new List<ConsumeResult<TKey, TValue>>();
        var message = await Task.Run(() => consumer.Consume(cancellationToken), cancellationToken);

        if (message == null || message.Message == null || message.Message.Value == null)
        {
            return list;
        }

        list.Add(message);
        return list;
    }

    public Task CompleteMessageAsync(ConsumeResult<TKey, TValue> message, CancellationToken cancellationToken = default)
    {
        var consumer = GetConsumer();

        consumer.StoreOffset(message);
        consumer.Commit();

        return Task.CompletedTask;
    }

    protected virtual IConsumer<TKey, TValue> GetConsumer()
    {
        if (kafkaConsumer == null)
        {
            var consumerConfig = this.kafkaConfigManager.CreateConsumerConfig(consumerConfigOptions);
            var schemaRegistryConfig = this.kafkaConfigManager.CreateSchemaRegistryConfig(schemaRegistryConfigOptions);
            kafkaConsumer = this.kafkaConsumersFactory.CreateConsumerWithSchema<TKey, TValue>(topic, consumerConfig, schemaRegistryConfig);
        }
        return kafkaConsumer;
    }

    protected abstract string topic { get; set; }

    protected abstract ConsumerConfigOptions consumerConfigOptions { get; set; }
    protected abstract SchemaRegistryConfigOptions schemaRegistryConfigOptions { get; set; }
}
