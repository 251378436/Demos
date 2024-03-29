using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using VehicleService.Infrastructure.Kafka;
using VehicleService.Infrastructure.ServiceBus;

namespace VehicleService.Infrastructure.Extentions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterCommonServices(
        this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IKafkaProducersFactory, KafkaProducersFactory>();
        serviceCollection.AddSingleton<IKafkaConsumersFactory, KafkaConsumersFactory>();
        serviceCollection.AddSingleton<IServiceBusFactory, ServiceBusFactory>();
        serviceCollection.AddSingleton<IAvroSchemaRegistryManager, AvroSchemaRegistryManager>();
        serviceCollection.AddSingleton<IKafkaConfigManager, KafkaConfigManager>();

        return serviceCollection;
    }
}
