using Azure.Messaging.ServiceBus;

namespace VehicleService.Infrastructure.ServiceBus.Proxy;

#pragma warning disable CS8618

public abstract class ServiceBusReceiverProxy : IServiceBusReceiverProxy
{
    protected readonly IServiceBusFactory ServiceBusReceiverFactory;
    private ServiceBusReceiver serviceBusReceiver;

    public ServiceBusReceiverProxy(IServiceBusFactory serviceBusReceiverFactory)
    {
        this.ServiceBusReceiverFactory = serviceBusReceiverFactory;
    }

    public virtual async Task<IEnumerable<ServiceBusReceivedMessage>> ReceiveMessagesAsync(CancellationToken cancellationToken = default)
    {
        var receiver = GetReceiver();

        var messages = await receiver.ReceiveMessagesAsync(10, cancellationToken: cancellationToken);
        return messages;
    }

    public virtual Task CompleteMessageAsync(ServiceBusReceivedMessage message, CancellationToken cancellationToken = default)
    {
        var receiver = GetReceiver();

        return receiver.CompleteMessageAsync(message, cancellationToken);
    }

    protected abstract string Endpoint { get; set; }
    protected abstract string Subscription { get; set; }
    protected virtual ServiceBusReceiverOptions? ReceiverOptions { get; set; } = null;

    protected virtual ServiceBusReceiver GetReceiver()
    {
        if (serviceBusReceiver == null)
        {
            serviceBusReceiver = ServiceBusReceiverFactory.CreateServiceBusReceiver(Endpoint, Subscription, ReceiverOptions);
        }
        return serviceBusReceiver;
    }
}