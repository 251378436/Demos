using Azure.Messaging.ServiceBus;
using FluentAssertions;
using IntegrationTestWithSubstituteExample.ReplacedServices;
using Newtonsoft.Json;
using VehicleService.Domain.Entity;
using VehicleService.Domain.UseCases.Sensor.Entity;
using Xunit.Abstractions;

namespace IntegrationTestWithSubstituteExample;

public class SensorUseCaseTests
{

    public SensorUseCaseTests(ITestOutputHelper testOutputHelper)
    {
        var vehicleServiceFactory = new VehicleServiceFactory(testOutputHelper);
        var vehicleServiceClient = vehicleServiceFactory.CreateClient();
    }

    [Fact]
    public async Task Process_WhenCustomerIsStandard_ThenSendBasicMessageout()
    {
        // Arrange
        var sensorRequest = new SensorRequest();
        sensorRequest.CustomerId = Guid.Parse("d8288aa9-4dee-495e-8bba-2cc92714b915");
        sensorRequest.VehicleId = Guid.Parse("82ed666a-2b2e-4380-bfd5-cfb5d72d6d8c");

        var payload = JsonConvert.SerializeObject(sensorRequest);
        var binaryData = BinaryData.FromString(payload);
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(binaryData);

        // Act
        ReplacedSensorReceiverMessageCollection.AddMessage(message);

        // Assert
        await Task.Delay(TimeSpan.FromSeconds(5));

        var expectedSensorMessage = new SensorMessage();
        expectedSensorMessage.VehicleId = Guid.Parse("82ed666a-2b2e-4380-bfd5-cfb5d72d6d8c");
        expectedSensorMessage.Customer = new Customer();
        expectedSensorMessage.Customer.CustomerId = Guid.Parse("d8288aa9-4dee-495e-8bba-2cc92714b915");
        expectedSensorMessage.Customer.CustomerType = CustomerType.Standard;
        expectedSensorMessage.Customer.Description = "Test Customer";
        expectedSensorMessage.Tags = new Dictionary<string, string>();
        expectedSensorMessage.Tags.Add("latitude", "13.22");
        expectedSensorMessage.Tags.Add("longitude", "-34.98");

        var actualSensorMessage = ReplacedSensorKafkaProducerMessage.GetMessage();
        actualSensorMessage.Key.Should().Be("d8288aa9-4dee-495e-8bba-2cc92714b915");
        var actual = actualSensorMessage.Value;
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

        var payload = JsonConvert.SerializeObject(sensorRequest);
        var binaryData = BinaryData.FromString(payload);
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(binaryData);

        // Act
        ReplacedSensorReceiverMessageCollection.AddMessage(message);

        // Assert
        await Task.Delay(TimeSpan.FromSeconds(5));

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

        var actualSensorMessage = ReplacedSensorKafkaProducerMessage.GetMessage();
        actualSensorMessage.Key.Should().Be("954cbf0c-b594-4d19-828c-6f3bbedec728");
        var actual = actualSensorMessage.Value;
        var expected = JsonConvert.SerializeObject(expectedSensorMessage);
        actual
            .Should()
            .Be(expected);
    }

}