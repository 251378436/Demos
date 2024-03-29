using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleService.Domain.UseCases.Sensor;
using VehicleService.Domain.UseCases.Sensor.Entity;

namespace ComponentTestExample.ReplacedServices;

internal class ReplacedSensorWriter : ISensorWriter
{
    public Task SendMessage(SensorMessage sensorMessage)
    {
        ReplacedSensorWriterMessage.SetMessage(sensorMessage);
        return Task.CompletedTask;
    }
}
