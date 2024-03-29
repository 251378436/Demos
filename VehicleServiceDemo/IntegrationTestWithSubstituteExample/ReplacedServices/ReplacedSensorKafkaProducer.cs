using Confluent.Kafka;
using VehicleService.Infrastructure.UseCases.Sensor;

namespace IntegrationTestWithSubstituteExample.ReplacedServices;

internal class ReplacedSensorKafkaProducer : ISensorKafkaProducer
{
    public int Flush(TimeSpan timeout)
    {
        return 0;
    }

    public Task<DeliveryResult<string, string>> ProduceAsync(string topic, Message<string, string> message, CancellationToken cancellationToken = default)
    {
        ReplacedSensorKafkaProducerMessage.SetMessage(message);
        return Task.FromResult(new DeliveryResult<string, string>());
    }
}
