using VehicleService.Infrastructure.ServiceBus;
using VehicleService.Infrastructure.ServiceBus.Proxy;

namespace VehicleService.Infrastructure.UseCases.Sensor;

public interface ISensorReceiver : IServiceBusReceiverProxy
{
}


public class SensorReceiver : ServiceBusReceiverProxy, ISensorReceiver
{
    public SensorReceiver(IServiceBusFactory serviceBusReceiverFactory) : base(serviceBusReceiverFactory)
    {
    }

    protected override string Endpoint { get; set; } = "Endpoint=sb://localhost/;SharedAccessKeyName=all;SharedAccessKey=CLwo3FQ3S39Z4pFOQDefaiUd1dSsli4XOAj3Y9Uh1E=;EnableAmqpLinkRedirect=false";
    protected override string Subscription { get; set; } = "PointHistory/Subscriptions/cip";
}
