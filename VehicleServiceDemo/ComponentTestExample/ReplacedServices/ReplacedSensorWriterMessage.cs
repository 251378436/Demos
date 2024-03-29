using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleService.Domain.UseCases.Sensor.Entity;

namespace ComponentTestExample.ReplacedServices;

internal static class ReplacedSensorWriterMessage
{
    private static SensorMessage sensorMessage;

    internal static void SetMessage(SensorMessage input)
    {
        sensorMessage = input;
    }

    internal static SensorMessage GetMessage()
    {
        return sensorMessage;
    }
}
