using VehicleService.Domain.UseCases.Sensor;

namespace VehicleReplayService.Domain.UseCases.SensorReplay;

public class SensorReplayUseCase : SensorUseCase, ISensorReplayUseCase
{
    public SensorReplayUseCase(ILogger<SensorUseCase> logger,
        ISensorWriter sensorWriter) : base(logger, sensorWriter)
    {

    }
}
