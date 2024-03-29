using Azure.Messaging.ServiceBus;
using VehicleService.Infrastructure.Services.Receivers;

namespace VehicleService.Infrastructure.ServiceBus.Proxy;

public interface IServiceBusReceiverProxy : IMessageReceiver<ServiceBusReceivedMessage>
{

}
