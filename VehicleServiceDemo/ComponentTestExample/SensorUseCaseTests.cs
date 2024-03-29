using FluentAssertions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VehicleService.Domain.Entity;
using VehicleService.Domain.UseCases.Sensor.Entity;
using VehicleService.Domain.UseCases.Sensor;
using ComponentTestExample.ReplacedServices;

namespace ComponentTestExample;

public class SensorUseCaseTests
{
    private readonly ISensorUseCase useCase;

    public SensorUseCaseTests(ISensorUseCase useCase)
    {
        this.useCase = useCase;    
    }


    [Fact]
    public async Task Process_WhenCustomerIsStandard_ThenSendBasicMessageout()
    {
        // Arrange
        var sensorRequest = new SensorRequest();
        sensorRequest.CustomerId = Guid.Parse("d8288aa9-4dee-495e-8bba-2cc92714b915");
        sensorRequest.VehicleId = Guid.Parse("82ed666a-2b2e-4380-bfd5-cfb5d72d6d8c");

        // Act
        var act = () => useCase.Propcess(sensorRequest);

        // Assert
        await act.Should()
            .NotThrowAsync();

        var expectedSensorMessage = new SensorMessage();
        expectedSensorMessage.VehicleId = Guid.Parse("82ed666a-2b2e-4380-bfd5-cfb5d72d6d8c");
        expectedSensorMessage.Customer = new Customer();
        expectedSensorMessage.Customer.CustomerId = Guid.Parse("d8288aa9-4dee-495e-8bba-2cc92714b915");
        expectedSensorMessage.Customer.CustomerType = CustomerType.Standard;
        expectedSensorMessage.Customer.Description = "Test Customer";
        expectedSensorMessage.Tags = new Dictionary<string, string>();
        expectedSensorMessage.Tags.Add("latitude", "13.22");
        expectedSensorMessage.Tags.Add("longitude", "-34.98");

        var actualSensorMessage = ReplacedSensorWriterMessage.GetMessage();
        var actual = JsonConvert.SerializeObject(actualSensorMessage);
        var expected = JsonConvert.SerializeObject(expectedSensorMessage);
        actual
            .Should()
            .Be(expected);
    }

    [Fact]
    public async Task Process_WhenCustomerIsPremium_ThenSendPremiumMessageout()
    {
        // Arrange
        var sensorRequest = new SensorRequest();
        sensorRequest.CustomerId = Guid.Parse("954cbf0c-b594-4d19-828c-6f3bbedec728");
        sensorRequest.VehicleId = Guid.Parse("82ed666a-2b2e-4380-bfd5-cfb5d72d6d8c");

        // Act
        var act = () => useCase.Propcess(sensorRequest);

        // Assert
        await act.Should()
            .NotThrowAsync();

        var expectedSensorMessage = new SensorMessage();
        expectedSensorMessage.VehicleId = Guid.Parse("82ed666a-2b2e-4380-bfd5-cfb5d72d6d8c");
        expectedSensorMessage.Customer = new Customer();
        expectedSensorMessage.Customer.CustomerId = Guid.Parse("954cbf0c-b594-4d19-828c-6f3bbedec728");
        expectedSensorMessage.Customer.CustomerType = CustomerType.Premium;
        expectedSensorMessage.Customer.Description = "Test Customer";
        expectedSensorMessage.Tags = new Dictionary<string, string>();
        expectedSensorMessage.Tags.Add("latitude", "13.22");
        expectedSensorMessage.Tags.Add("longitude", "-34.98");
        expectedSensorMessage.Tags.Add("temperature", "15.23");
        expectedSensorMessage.Tags.Add("humidity", "55");
        expectedSensorMessage.Tags.Add("light", "82");

        var actualSensorMessage = ReplacedSensorWriterMessage.GetMessage();
        var actual = JsonConvert.SerializeObject(actualSensorMessage);
        var expected = JsonConvert.SerializeObject(expectedSensorMessage);
        actual
            .Should()
            .Be(expected);
    }

    [Fact]
    public async Task Process_WhenCustomerIsExpired_ThenThrowException()
    {
        // Arrange
        var sensorRequest = new SensorRequest();
        sensorRequest.CustomerId = Guid.Parse("d4126485-3f38-4f8f-b8af-36a058272528");
        sensorRequest.VehicleId = Guid.Parse("82ed666a-2b2e-4380-bfd5-cfb5d72d6d8c");

        // Act
        var act = () => useCase.Propcess(sensorRequest);

        // Assert
        await act.Should()
            .ThrowAsync<Exception>().WithMessage("Customer is not supported");
    }
}