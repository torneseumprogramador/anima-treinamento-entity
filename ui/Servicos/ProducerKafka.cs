using Confluent.Kafka;
using System;

public class ProducerKafka
{
    public async void Send(string message)
    {
        var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

        using var p = new ProducerBuilder<Null, string>(config).Build();
        try
        {
            var dr = await p.ProduceAsync("test-topic", new Message<Null, string> { Value = message });
            System.Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
        }
        catch (ProduceException<Null, string> e)
        {
            System.Console.WriteLine($"Delivery failed: {e.Error.Reason}");
        }
    }
}
