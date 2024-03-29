using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestWithSubstituteExample.ReplacedServices;

internal static class ReplacedSensorReceiverMessageCollection
{
    private static List<ServiceBusReceivedMessage> collection = new List<ServiceBusReceivedMessage>();
    private static readonly object messageLock = new object();

    public static void AddMessage(ServiceBusReceivedMessage message)
    {
        lock (messageLock)
        {
            collection.Add(message);
        }
    }

    public static IReadOnlyList<ServiceBusReceivedMessage> GetCollection()
    {
        lock (messageLock)
        {
            if (collection.Count > 0)
            {
                IReadOnlyList<ServiceBusReceivedMessage> result = collection;
                collection = new List<ServiceBusReceivedMessage>();
                return result;
            }
            else
            {
                return collection;
            }
        }
    }
}
