

// new ConsumerKafka().Receive();
while(true)
    new ConsumerRabbitMq().Receive();


// while(true)
// {
//     Console.WriteLine("Lendo mensagem ...");
//     // await new SqsConsumer().ReceiveMessagesAsync();
// }
