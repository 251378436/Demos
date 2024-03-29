
namespace VehicleService.Domain.Services.UseCases;

public interface IUseCase<TRequest>
{
    Task Propcess(TRequest sensorRequest, CancellationToken cancellationToken = default, IDictionary<string, object>? properties = null);
}
