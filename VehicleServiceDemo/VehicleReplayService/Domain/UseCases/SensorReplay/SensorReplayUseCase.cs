using VehicleService.Domain.UseCases.Sensor;

namespace VehicleReplayService.Domain.UseCases.SensorReplay;

public class SensorReplayUseCase : SensorUseCase, ISensorReplayUseCase
{
    public SensorReplayUseCase(ILogger<SensorUseCase> logger,
        ISensorReplayWriter sensorWriter) : base(logger, sensorWriter)
    {

    }
}
