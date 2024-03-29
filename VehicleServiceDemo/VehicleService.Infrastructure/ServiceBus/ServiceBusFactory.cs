using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;

namespace VehicleService.Infrastructure.ServiceBus;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8600

public class ServiceBusFactory : IServiceBusFactory, IAsyncDisposable
{
    private readonly object factoryLock = new object();
    private readonly ILogger<ServiceBusFactory> logger;
    private readonly Dictionary<string, ServiceBusClient> clients = new();
    private List<ServiceBusReciverTracker> receivers = new();

    public ServiceBusFactory(ILogger<ServiceBusFactory> logger)
    {
        this.logger = logger;
    }

    #region ServiceBus Receiver

    public ServiceBusReceiver CreateServiceBusReceiver(string endpoint, string subscription, ServiceBusReceiverOptions? serviceBusReceiverOptions)
    {
        var client = CreateServiceBusClient(endpoint.Trim());

        return CreateServiceBusReceiver(client, subscription, serviceBusReceiverOptions);
    }

    private ServiceBusReceiver CreateServiceBusReceiver(ServiceBusClient client, string subscription, ServiceBusReceiverOptions? serviceBusReceiverOptions)
    {
        ServiceBusReceiver? receiver;

        receiver = this.receivers.FirstOrDefault(x => x.Subscription == subscription && x.ServiceBusClient == client)?.ServiceBusReceiver;

        if (receiver != null) return receiver;

        lock (factoryLock)
        {
            receiver = this.receivers.FirstOrDefault(x => x.Subscription == subscription && x.ServiceBusClient == client)?.ServiceBusReceiver;

            if (receiver != null) return receiver;

            var options = serviceBusReceiverOptions ?? new ServiceBusReceiverOptions()
            {
                ReceiveMode = ServiceBusReceiveMode.PeekLock,
                PrefetchCount = 10
            };

            receiver = client.CreateReceiver(subscription, options);

            this.receivers.Add(new ServiceBusReciverTracker
            {
                ServiceBusClient = client,
                Subscription = subscription,
                ServiceBusReceiver = receiver
            });

            logger.LogInformation($"Created a new ServiceBus receiver {subscription}");

            return receiver;
        }
    }

    private class ServiceBusReciverTracker
    {
        public ServiceBusClient ServiceBusClient { get; set; }
        public string Subscription { get; set; }
        public ServiceBusReceiver ServiceBusReceiver { get; set; }
    }

    #endregion ServiceBus Receiver

    private ServiceBusClient CreateServiceBusClient(string endpoint)
    {
        ServiceBusClient serviceBusClient;
        if (!clients.TryGetValue(endpoint, out serviceBusClient))
        {
            lock (factoryLock)
            {
                if (!clients.TryGetValue(endpoint, out serviceBusClient))
                {
                    serviceBusClient = new ServiceBusClient(endpoint);
                    clients.Add(endpoint, serviceBusClient);
                }
            }
        }

        return serviceBusClient;
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var client in clients.Values)
        {
            await client.DisposeAsync();
        }

        foreach (var receiver in receivers)
        {
            await receiver.ServiceBusReceiver.DisposeAsync();
        }
    }

}