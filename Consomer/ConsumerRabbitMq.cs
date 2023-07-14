using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class ConsumerRabbitMq
{
    public void Receive()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "hello",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            System.Console.WriteLine(" Received: {0}", message);
        };
        channel.BasicConsume(queue: "hello",
                             autoAck: true,
                             consumer: consumer);

        System.Console.WriteLine(" Press [enter] to exit.");
        Thread.Sleep(2000);
        // System.Console.ReadLine();
    }
}
