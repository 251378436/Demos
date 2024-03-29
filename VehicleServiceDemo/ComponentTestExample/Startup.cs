using ComponentTestExample.ReplacedServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleService.Domain.UseCases.Sensor;
using VehicleService.Infrastructure.Extentions;
using VehicleService.Infrastructure.UseCases.Sensor;

namespace ComponentTestExample;

public class Startup
{
    public void ConfigureServices(IServiceCollection services) 
    {
        services.RegisterCommonServices();
        services.RegisterSensor();
        var backgroundService = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(SensorBackgroundService));
        services.Remove(backgroundService);

        services.AddSingleton<ISensorWriter, ReplacedSensorWriter>();

    }
}
