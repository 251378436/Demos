using Confluent.Kafka;
using vehicle.sensor.massage.replay;
using VehicleReplayService.Domain.UseCases.SensorReplay;
using VehicleService.Domain.UseCases.Sensor.Entity;

namespace VehicleReplayService.Infrastructure.UseCases.SensorReplay;

public class SensorReplayWriter : ISensorReplayWriter
{
    private readonly ISensorReplayKafkaProducer producer;

    public SensorReplayWriter(ISensorReplayKafkaProducer producer)
    {
        this.producer = producer;
    }

    public async Task SendMessage(SensorMessage sensorMessage)
    {
        var message = new Message<SensorMessageKey, SensorMessageValue>();
        var messageKey = new SensorMessageKey();
        messageKey.customer_id = sensorMessage.Customer.CustomerId.ToString();

        var messageValue = new SensorMessageValue();
        messageValue.vehicle_id = sensorMessage.VehicleId.ToString();
        messageValue.customer = new Customer()
        {
            customer_id = sensorMessage.Customer.CustomerId.ToString(),
            description = sensorMessage.Customer.Description,
            customer_type = (CustomerType)sensorMessage.Customer.CustomerType,
        };
        messageValue.tags = sensorMessage.Tags;

        message.Key = messageKey;
        message.Value = messageValue;

        await producer.ProduceAsync("vehicle.service.sensor.replay", message);

        producer.Flush(TimeSpan.FromSeconds(500));
    }
}