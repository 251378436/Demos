using Azure.Messaging.ServiceBus;

namespace VehicleService.Infrastructure.ServiceBus;

public interface IServiceBusFactory
{
    ServiceBusReceiver CreateServiceBusReceiver(string endpoint, string subscription, ServiceBusReceiverOptions? serviceBusReceiverOptions);
}
