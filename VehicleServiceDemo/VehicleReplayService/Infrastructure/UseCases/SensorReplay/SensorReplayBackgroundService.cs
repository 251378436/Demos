using Confluent.Kafka;
using VehicleReplayService.Domain.UseCases.SensorReplay;
using VehicleService.Domain.UseCases.Sensor.Entity;
using VehicleService.Infrastructure.Services.Receivers;
using VehicleService.Infrastructure.UseCases.Sensor;

namespace VehicleReplayService.Infrastructure.UseCases.SensorReplay;

public class SensorReplayBackgroundService : BaseMessagesbackgroundService<ConsumeResult<string, string>, SensorRequest>
{
    public SensorReplayBackgroundService(ISensorReplayReceiver sensorReceiver,
        IHostApplicationLifetime applicationLifetime,
        ILogger<SensorBackgroundService> logger,
        ISensorReplayUseCase useCase) : base(sensorReceiver, applicationLifetime, logger, useCase)
    {
    }

    protected override Task<string> GetMessageBody(ConsumeResult<string, string> message)
    {
        return Task.FromResult(message.Value);
    }

    protected override Task<SensorRequest> GetRequest(ConsumeResult<string, string> message, string messageBody)
    {
        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<SensorRequest>(message.Value);
        return Task.FromResult(result);
    }
}