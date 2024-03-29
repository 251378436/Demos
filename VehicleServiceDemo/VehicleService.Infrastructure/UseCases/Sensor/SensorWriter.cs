using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleService.Domain.UseCases.Sensor;
using VehicleService.Domain.UseCases.Sensor.Entity;
using static Confluent.Kafka.ConfigPropertyNames;

namespace VehicleService.Infrastructure.UseCases.Sensor;

public class SensorWriter : ISensorWriter
{
    private readonly ISensorKafkaProducer sensorKafkaProducer;
    public SensorWriter(ISensorKafkaProducer sensorKafkaProducer)
    {
        this.sensorKafkaProducer = sensorKafkaProducer;
    }

    public async Task SendMessage(SensorMessage sensorMessage)
    {
        var message = new Message<string, string>();
        message.Key = sensorMessage.Customer.CustomerId.ToString();
        message.Value = Newtonsoft.Json.JsonConvert.SerializeObject(sensorMessage);

        await sensorKafkaProducer.ProduceAsync("vehicle.service.sensor", message);

        sensorKafkaProducer.Flush(TimeSpan.FromSeconds(500));
    }
}
