namespace VehicleService.Domain.UseCases.Sensor.Entity;

public class SensorRequest
{
    public Guid VehicleId { get; set; }
    public Guid CustomerId { get; set; }
}