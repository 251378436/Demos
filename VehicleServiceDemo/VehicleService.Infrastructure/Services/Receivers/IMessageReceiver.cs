using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleService.Infrastructure.Services.Receivers;

public interface IMessageReceiver<TMessage>
{
    Task<IEnumerable<TMessage>> ReceiveMessagesAsync(CancellationToken cancellationToken = default);

    Task CompleteMessageAsync(TMessage message, CancellationToken cancellationToken = default);
}
