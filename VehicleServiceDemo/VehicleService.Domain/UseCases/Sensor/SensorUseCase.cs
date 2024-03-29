using Microsoft.Extensions.Logging;
using VehicleService.Domain.Entity;
using VehicleService.Domain.UseCases.Sensor.Entity;

namespace VehicleService.Domain.UseCases.Sensor;

public class SensorUseCase : ISensorUseCase
{
    private readonly ILogger<SensorUseCase> logger;
    private readonly ISensorWriter sensorWriter;

    public SensorUseCase(ILogger<SensorUseCase> logger, 
        ISensorWriter sensorWriter)
    {
        this.logger = logger;
        this.sensorWriter = sensorWriter;
    }

    public async Task Propcess(SensorRequest sensorRequest, CancellationToken cancellationToken = default, IDictionary<string, object>? properties = null)
    {
        var customer = await GetCustomer(sensorRequest.CustomerId);

        if (customer.CustomerType == CustomerType.Standard)
        {
            var sensorMessage = new SensorMessage();
            sensorMessage.Customer = customer;
            sensorMessage.VehicleId = sensorRequest.VehicleId;
            sensorMessage.Tags = new Dictionary<string, string>();
            sensorMessage.Tags.Add("latitude", "13.22");
            sensorMessage.Tags.Add("longitude", "-34.98");

            await this.sensorWriter.SendMessage(sensorMessage);
        }
        else if (customer.CustomerType == CustomerType.Premium)
        {
            var sensorMessage = new SensorMessage();
            sensorMessage.Customer = customer;
            sensorMessage.VehicleId = sensorRequest.VehicleId;
            sensorMessage.Tags = new Dictionary<string, string>();
            sensorMessage.Tags.Add("latitude", "13.22");
            sensorMessage.Tags.Add("longitude", "-34.98");
            sensorMessage.Tags.Add("temperature", "15.23");
            sensorMessage.Tags.Add("humidity", "55");
            sensorMessage.Tags.Add("light", "82");

            await this.sensorWriter.SendMessage(sensorMessage);
        }
        else
        {
            throw new Exception("Customer is not supported");
        }
    }

    private Task<Customer> GetCustomer(Guid customerId)
    {
        var result = new Customer();
        result.CustomerId = customerId;
        result.Description = "Test Customer";

        if (customerId == Guid.Parse("d8288aa9-4dee-495e-8bba-2cc92714b915"))
        {
            result.CustomerType = CustomerType.Standard;
        }
        else if (customerId == Guid.Parse("d4126485-3f38-4f8f-b8af-36a058272528"))
        {
            result.CustomerType = CustomerType.Expired;
        }
        else
        {
            result.CustomerType = CustomerType.Premium;
        }

        return Task.FromResult(result);
    }
}