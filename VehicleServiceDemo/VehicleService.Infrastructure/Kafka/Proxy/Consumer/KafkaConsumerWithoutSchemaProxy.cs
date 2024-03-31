using Confluent.Kafka;
using VehicleService.Infrastructure.Kafka.Options;

namespace VehicleService.Infrastructure.Kafka.Proxy.Producer;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8600
#pragma warning disable CS8603

public abstract class KafkaConsumerWithoutSchemaProxy : IKafkaConsumerProxy<string, string>
{
    private IConsumer<string, string> kafkaConsumer;

    private readonly IKafkaConsumersFactory kafkaConsumersFactory;
    private readonly IKafkaConfigManager kafkaConfigManager;

    public KafkaConsumerWithoutSchemaProxy(IKafkaConsumersFactory kafkaConsumersFactory, 
        IKafkaConfigManager kafkaConfigManager)
    {
        this.kafkaConsumersFactory = kafkaConsumersFactory;
        this.kafkaConfigManager = kafkaConfigManager;
    }

    public async Task<IEnumerable<ConsumeResult<string, string>>> ReceiveMessagesAsync(CancellationToken cancellationToken = default)
    {
        var consumer = GetConsumer();
        var list = new List<ConsumeResult<string, string>>();
        var message = await Task.Run(() => consumer.Consume(cancellationToken), cancellationToken);

        if (message == null || message.Message == null || message.Message.Value == null)
        {
            return list;
        }

        list.Add(message);
        return list;
    }

    public Task CompleteMessageAsync(ConsumeResult<string, string> message, CancellationToken cancellationToken = default)
    {
        var consumer = GetConsumer();

        consumer.StoreOffset(message);
        consumer.Commit();

        return Task.CompletedTask;
    }

    protected virtual IConsumer<string, string> GetConsumer()
    {
        if (kafkaConsumer == null)
        {
            var consumerConfig = this.kafkaConfigManager.CreateConsumerConfig(consumerConfigOptions);
            kafkaConsumer = this.kafkaConsumersFactory.CreateConsumerWithoutSchema(topic, consumerConfig);
        }
        return kafkaConsumer;
    }

    protected abstract string topic { get; set; }

    protected abstract ConsumerConfigOptions consumerConfigOptions { get; set; }
}