using Confluent.Kafka;
using VehicleService.Infrastructure.Services.Receivers;

namespace VehicleService.Infrastructure.Kafka.Proxy.Producer;

public interface IKafkaConsumerProxy<TKey, TValue> : IMessageReceiver<ConsumeResult<TKey, TValue>>
{
}