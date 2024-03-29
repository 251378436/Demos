using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using VehicleService.Domain.UseCases.Sensor;

namespace VehicleService.Infrastructure.UseCases.Sensor;

public static class SensorServiceCollectionExtensions
{
    public static IServiceCollection RegisterSensor(
        this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ISensorKafkaProducer, SensorKafkaProducer>();
        serviceCollection.AddSingleton<ISensorReceiver, SensorReceiver>();
        serviceCollection.AddSingleton<ISensorWriter, SensorWriter>();
        serviceCollection.AddSingleton<ISensorUseCase, SensorUseCase>();

        serviceCollection.AddHostedService<SensorBackgroundService>();

        return serviceCollection;
    }
}
