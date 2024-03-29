using Azure.Messaging.ServiceBus;
using VehicleService.Infrastructure.UseCases.Sensor;

namespace IntegrationTestWithSubstituteExample.ReplacedServices;

internal class ReplacedSensorReceiver : ISensorReceiver
{
    public Task CompleteMessageAsync(ServiceBusReceivedMessage message, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<ServiceBusReceivedMessage>> ReceiveMessagesAsync(CancellationToken cancellationToken = default)
    {
        await Task.Delay(1000);
        var result = ReplacedSensorReceiverMessageCollection.GetCollection();
        return result;
    }
}