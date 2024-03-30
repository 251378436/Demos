using VehicleReplayService.Domain.UseCases.SensorReplay;
using VehicleService.Domain.UseCases.Sensor;
using VehicleService.Infrastructure.UseCases.Sensor;

namespace VehicleReplayService.Infrastructure.UseCases.SensorReplay;

public static class SensorReplayServiceCollectionExtensions
{
    public static IServiceCollection RegisterSensorReplay(
        this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ISensorKafkaProducer, SensorKafkaProducer>();
        serviceCollection.AddSingleton<ISensorReplayReceiver, SensorReplayReceiver>();
        serviceCollection.AddSingleton<ISensorReplayWriter, SensorReplayWriter>();
        serviceCollection.AddSingleton<ISensorReplayUseCase, SensorReplayUseCase>();
        serviceCollection.AddSingleton<ISensorReplayKafkaProducer, SensorReplayKafkaProducer>();

        serviceCollection.AddHostedService<SensorReplayBackgroundService>();

        return serviceCollection;
    }
}
