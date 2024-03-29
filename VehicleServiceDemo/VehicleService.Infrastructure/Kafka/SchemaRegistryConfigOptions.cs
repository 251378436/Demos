
namespace VehicleService.Infrastructure.Kafka;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

public class SchemaRegistryConfigOptions
{
    public string Url { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
