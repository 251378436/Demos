using VehicleService.Domain.UseCases.Sensor.Entity;

namespace VehicleService.Domain.UseCases.Sensor;

public interface ISensorWriter
{
    Task SendMessage(SensorMessage sensorMessage);
}