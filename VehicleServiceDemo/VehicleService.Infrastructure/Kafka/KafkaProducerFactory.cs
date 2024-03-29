using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

namespace VehicleService.Infrastructure.Kafka;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8600
#pragma warning disable CS8603

public interface IKafkaProducersFactory
{
    IProducer<TKey, TValue> CreateProducerWithSchema<TKey, TValue>(ProducerConfig producerConfig, SchemaRegistryConfig schemaRegistryConfig);

    IProducer<string, string> CreateProducerWithoutSchema(ProducerConfig producerConfig);
}

public class KafkaProducersFactory : IKafkaProducersFactory, IDisposable
{
    private readonly object factoryLock = new object();
    private CachedSchemaRegistryClient schemaRegistryClient;
    private List<KafkaProducerTracker> producersWithoutAvroSchema = new();
    private List<KafkaProducerTracker> producersWithAvroSchema = new();

    private readonly IAvroSchemaRegistryManager avroSchemaRegistryManager;
    public KafkaProducersFactory(IAvroSchemaRegistryManager avroSchemaRegistryManager)
    {
        this.avroSchemaRegistryManager = avroSchemaRegistryManager;
    }

    public IProducer<TKey, TValue> CreateProducerWithSchema<TKey, TValue>(ProducerConfig producerConfig, SchemaRegistryConfig schemaRegistryConfig)
    {
        var avroSerializerConfig = new AvroSerializerConfig
        {
            // optional Avro serializer properties:
            BufferBytes = 100
        };

        IProducer<TKey, TValue> producer;

        producer = (IProducer<TKey, TValue>)this.producersWithoutAvroSchema
            .FirstOrDefault(x => x.BootstrapServers == producerConfig.BootstrapServers
            && x.KeyType == typeof(TKey)
            && x.ValueType == typeof(TValue)
            )?.Producer;

        if (producer == null)
        {
            lock (factoryLock)
            {
                producer = (IProducer<TKey, TValue>)this.producersWithoutAvroSchema
                        .FirstOrDefault(x => x.BootstrapServers == producerConfig.BootstrapServers
                        && x.KeyType == typeof(TKey)
                        && x.ValueType == typeof(TValue)
                        )?.Producer;

                if (producer == null)
                {
                    var schemaRegistry = avroSchemaRegistryManager.CreateSchemaRegistryClient(schemaRegistryConfig);

                    if(typeof(TKey) == typeof(string))
                    {
                        producer = new ProducerBuilder<TKey, TValue>(producerConfig)
                                .SetErrorHandler((_, errorHandler) =>
                                {
                                    Console.WriteLine($"Failed to create kafka producer. Error: ${errorHandler}");
                                })
                                .SetValueSerializer(new AvroSerializer<TValue>(schemaRegistry, avroSerializerConfig))
                                .Build();
                    }
                    else
                    {
                        producer = new ProducerBuilder<TKey, TValue>(producerConfig)
                                .SetErrorHandler((_, errorHandler) =>
                                {
                                    Console.WriteLine($"Failed to create kafka producer. Error: ${errorHandler}");
                                })
                                .SetKeySerializer(new AvroSerializer<TKey>(schemaRegistry, avroSerializerConfig))
                                .SetValueSerializer(new AvroSerializer<TValue>(schemaRegistry, avroSerializerConfig))
                                .Build();
                    }


                    this.producersWithAvroSchema.Add(new KafkaProducerTracker()
                    {
                        BootstrapServers = producerConfig.BootstrapServers,
                        KeyType = typeof(TKey),
                        ValueType = typeof(TValue),
                        Producer = producer
                    });
                }
            }
        }

        return producer;
    }

    public IProducer<string, string> CreateProducerWithoutSchema(ProducerConfig producerConfig)
    {
        IProducer<string, string> producer;

        producer = (IProducer<string, string>)this.producersWithoutAvroSchema.FirstOrDefault(x => x.BootstrapServers == producerConfig.BootstrapServers)?.Producer;

        if(producer == null)
        {
            lock (factoryLock)
            {
                producer = (IProducer<string, string>)this.producersWithoutAvroSchema.FirstOrDefault(x => x.BootstrapServers == producerConfig.BootstrapServers)?.Producer;

                if (producer == null)
                {
                    producer = new ProducerBuilder<string, string>(producerConfig)
                    .SetErrorHandler((_, errorHandler) =>
                    {
                        Console.WriteLine($"Failed to create kafka producer. Error: ${errorHandler}");
                    })
                    .Build();

                    this.producersWithoutAvroSchema.Add(new KafkaProducerTracker()
                    {
                        BootstrapServers = producerConfig.BootstrapServers,
                        KeyType = typeof(string),
                        ValueType = typeof(string),
                        Producer = producer
                    });
                }
            }
        }

        return producer;
    }

    public void Dispose()
    {
        this.producersWithoutAvroSchema.ForEach(o => o.Producer.Dispose());
        this.producersWithAvroSchema.ForEach(o => o.Producer.Dispose());
    }

    private class KafkaProducerTracker
    {
        public string BootstrapServers { get; set; }
        public Type KeyType { get; set; }
        public Type ValueType { get; set; }
        public IClient Producer { get; set; }
    }
}