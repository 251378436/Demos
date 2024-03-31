namespace VehicleService.Infrastructure.Kafka.Options;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

public class ProducerConfigOptions
{
    public string BootstrapServers { get; set; }
    public string SaslUsername { get; set; }
    public string SaslPassword { get; set; }
    public bool EnableSslCertificateVerification { get; set; }
}
