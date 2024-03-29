using Confluent.Kafka;

namespace IntegrationTestWithSubstituteExample.ReplacedServices;

internal static class ReplacedSensorKafkaProducerMessage
{
    private static Message<string, string> message;

    internal static void SetMessage(Message<string, string> input)
    {
        message = input;
    }

    internal static Message<string, string> GetMessage()
    {
        return message;
    }
}
