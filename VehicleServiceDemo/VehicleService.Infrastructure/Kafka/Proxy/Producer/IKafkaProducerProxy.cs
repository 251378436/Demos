using Confluent.Kafka;

namespace VehicleService.Infrastructure.Kafka.Proxy.Producer;

public interface IKafkaProducerProxy<TKey, TValue>
{
    Task<DeliveryResult<TKey, TValue>> ProduceAsync(string topic, Message<TKey, TValue> message, CancellationToken cancellationToken = default);

    int Flush(TimeSpan timeout);
}