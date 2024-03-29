using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VehicleService.Domain.UseCases.Sensor;
using VehicleService.Domain.UseCases.Sensor.Entity;
using VehicleService.Infrastructure.Services.Receivers;

namespace VehicleService.Infrastructure.UseCases.Sensor;

public class SensorBackgroundService : BaseMessagesbackgroundService<ServiceBusReceivedMessage, SensorRequest>
{
    public SensorBackgroundService(ISensorReceiver sensorReceiver,
        IHostApplicationLifetime applicationLifetime,
        ILogger<SensorBackgroundService> logger,
        ISensorUseCase useCase) : base(sensorReceiver, applicationLifetime, logger, useCase)
    {
    }

    protected override Task<string> GetMessageBody(ServiceBusReceivedMessage message)
    {
        return Task.FromResult(message.Body.ToString());
    }

    protected override Task<SensorRequest> GetRequest(ServiceBusReceivedMessage message, string messageBody)
    {
        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<SensorRequest>(messageBody);
        return Task.FromResult(result);
    }
}
