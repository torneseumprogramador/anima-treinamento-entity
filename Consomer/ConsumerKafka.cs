using Confluent.Kafka;
using System;

public class ConsumerKafka
{
    public void Receive()
    {
        var conf = new ConsumerConfig
        {
            GroupId = "test-consumer-group",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var c = new ConsumerBuilder<Ignore, string>(conf).Build();
        c.Subscribe("test-topic");

        try
        {
            while (true)
            {
                var cr = c.Consume();
                Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
            }
        }
        catch (ConsumeException e)
        {
            Console.WriteLine($"Error occurred: {e.Error.Reason}");
        }
    }
}
