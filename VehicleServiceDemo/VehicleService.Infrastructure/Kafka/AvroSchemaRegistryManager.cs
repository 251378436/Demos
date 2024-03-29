using Confluent.SchemaRegistry;

namespace VehicleService.Infrastructure.Kafka;
#pragma warning disable CS8618
#pragma warning disable CS8600

public interface IAvroSchemaRegistryManager
{
    CachedSchemaRegistryClient CreateSchemaRegistryClient(SchemaRegistryConfig config);
}

public class AvroSchemaRegistryManager : IAvroSchemaRegistryManager, IDisposable
{
    private readonly Dictionary<string, CachedSchemaRegistryClient> schemaRegistryClients = new();
    private readonly object factoryLock = new object();

    public CachedSchemaRegistryClient CreateSchemaRegistryClient(SchemaRegistryConfig config)
    {
        CachedSchemaRegistryClient client;
        if(!schemaRegistryClients.TryGetValue(config.Url, out client))
        {
            lock (this.factoryLock)
            {
                if (!schemaRegistryClients.TryGetValue(config.Url, out client))
                {
                    client = new CachedSchemaRegistryClient(config);
                    schemaRegistryClients.Add(config.Url, client);
                }
            }
        }

        return client;
    }

    public void Dispose()
    {
        schemaRegistryClients.Values.ToList().ForEach(x => x.Dispose());
    }
}
