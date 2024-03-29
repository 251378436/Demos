using IntegrationTestWithSubstituteExample.ReplacedServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VehicleService.Infrastructure.UseCases.Sensor;
using Xunit.Abstractions;

namespace IntegrationTestWithSubstituteExample;

internal class VehicleServiceFactory : WebApplicationFactory<Program>
{
    private readonly ITestOutputHelper _testOutputHelper;

    public VehicleServiceFactory(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<ISensorReceiver, ReplacedSensorReceiver>();
            services.AddSingleton<ISensorKafkaProducer, ReplacedSensorKafkaProducer>();
        });

        builder.ConfigureLogging(loggingBuilder =>
        {
            loggingBuilder.Services.AddSingleton<ILoggerProvider>(serviceProvider => new XUnitLoggerProvider(_testOutputHelper));
        });
    }
}
