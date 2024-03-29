using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleService.Domain.Entity;

namespace VehicleService.Domain.UseCases.Sensor.Entity;

public class SensorMessage
{
    public Customer Customer { get; set; }
    public Guid VehicleId { get; set; }
    public Dictionary<string, string> Tags { get; set; }
}
