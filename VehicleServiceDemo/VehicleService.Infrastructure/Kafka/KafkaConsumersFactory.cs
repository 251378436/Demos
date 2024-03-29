using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

namespace VehicleService.Infrastructure.Kafka;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8600
#pragma warning disable CS8603

public interface IKafkaConsumersFactory
{
    IConsumer<TKey, TValue> CreateConsumerWithSchema<TKey, TValue>(string topic, ConsumerConfig consumerConfig, SchemaRegistryConfig schemaRegistryConfig);

    IConsumer<string, string> CreateConsumerWithoutSchema(string topic, ConsumerConfig consumerConfig);
}

public class KafkaConsumersFactory : IKafkaConsumersFactory, IDisposable
{
    private readonly object factoryLock = new object();
    private List<KafkaConsumerTracker> consumersWithoutAvroSchema = new();
    private List<KafkaConsumerTracker> consumersWithAvroSchema = new();

    private readonly IAvroSchemaRegistryManager avroSchemaRegistryManager;

    public KafkaConsumersFactory(IAvroSchemaRegistryManager avroSchemaRegistryManager)
    {
        this.avroSchemaRegistryManager = avroSchemaRegistryManager;
    }

    public IConsumer<TKey, TValue> CreateConsumerWithSchema<TKey, TValue>(string topic, ConsumerConfig consumerConfig, SchemaRegistryConfig schemaRegistryConfig)
    {
        var avroSerializerConfig = new AvroSerializerConfig
        {
            // optional Avro serializer properties:
            BufferBytes = 100
        };

        IConsumer<TKey, TValue> consumer;

        consumer = (IConsumer<TKey, TValue>)this.consumersWithoutAvroSchema
            .FirstOrDefault(x => x.BootstrapServers == consumerConfig.BootstrapServers
            && x.Topic == topic
            )?.Consumer;

        if (consumer == null)
        {
            lock (factoryLock)
            {
                consumer = (IConsumer<TKey, TValue>)this.consumersWithoutAvroSchema
                            .FirstOrDefault(x => x.BootstrapServers == consumerConfig.BootstrapServers
                            && x.Topic == topic
                            )?.Consumer;

                if (consumer == null)
                {
                    var schemaRegistry = avroSchemaRegistryManager.CreateSchemaRegistryClient(schemaRegistryConfig);

                    if (typeof(TKey) == typeof(string))
                    {
                        consumer = new ConsumerBuilder<TKey, TValue>(consumerConfig)
                                .SetErrorHandler((_, errorHandler) =>
                                {
                                    Console.WriteLine($"Failed to create kafka Consumer. Error: ${errorHandler}");
                                })
                                .SetValueDeserializer(new AvroDeserializer<TValue>(schemaRegistry).AsSyncOverAsync())
                                .Build();
                    }
                    else
                    {
                        consumer = new ConsumerBuilder<TKey, TValue>(consumerConfig)
                                .SetErrorHandler((_, errorHandler) =>
                                {
                                    Console.WriteLine($"Failed to create kafka Consumer. Error: ${errorHandler}");
                                })
                                .SetKeyDeserializer(new AvroDeserializer<TKey>(schemaRegistry).AsSyncOverAsync())
                                .SetValueDeserializer(new AvroDeserializer<TValue>(schemaRegistry).AsSyncOverAsync())
                                .Build();
                    }

                    this.consumersWithAvroSchema.Add(new KafkaConsumerTracker()
                    {
                        BootstrapServers = consumerConfig.BootstrapServers,
                        Topic = topic,
                        Consumer = consumer
                    });
                }
            }
        }
        return consumer;
    }

    public IConsumer<string, string> CreateConsumerWithoutSchema(string topic, ConsumerConfig consumerConfig)
    {
        IConsumer<string, string> consumer;

        consumer = (IConsumer<string, string>)this.consumersWithoutAvroSchema
                                    .FirstOrDefault(x => x.BootstrapServers == consumerConfig.BootstrapServers
                                                    && x.Topic == topic)?.Consumer;

        if (consumer == null)
        {
            lock (factoryLock)
            {
                consumer = (IConsumer<string, string>)this.consumersWithoutAvroSchema
                        .FirstOrDefault(x => x.BootstrapServers == consumerConfig.BootstrapServers
                                         && x.Topic == topic)?.Consumer;

                if (consumer == null)
                {
                    consumer = new ConsumerBuilder<string, string>(consumerConfig)
                    .SetErrorHandler((_, errorHandler) =>
                    {
                        Console.WriteLine($"Failed to create kafka consumer. Error: ${errorHandler}");
                    })
                    .Build();

                    this.consumersWithoutAvroSchema.Add(new KafkaConsumerTracker()
                    {
                        BootstrapServers = consumerConfig.BootstrapServers,
                        Topic = topic,
                        Consumer = consumer
                    });

                    consumer.Subscribe(topic);
                }
            }
        }

        return consumer;
    }

    public void Dispose()
    {
        this.consumersWithoutAvroSchema.ForEach(o => o.Consumer.Dispose());
        this.consumersWithAvroSchema.ForEach(o => o.Consumer.Dispose());
    }

    private class KafkaConsumerTracker
    {
        public string BootstrapServers { get; set; }
        public string Topic { get; set; }
        public IClient Consumer { get; set; }
    }
}