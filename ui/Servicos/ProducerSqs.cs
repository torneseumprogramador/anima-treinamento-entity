#nullable disable

using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Threading.Tasks;

public class ProducerSqs
{
    private readonly AmazonSQSClient _sqsClient;
    private readonly string _queueUrl;

    public ProducerSqs()
    {
        var awsAccessKey = "AKIA3DVZFLJH7KFWAGDY"; //Environment.GetEnvironmentVariable("AWS_ACCESS_KEY");
        var awsSecretKey = "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"; //Environment.GetEnvironmentVariable("AWS_SECRET_KEY");
        var awsRegion = "us-east-1"; // Environment.GetEnvironmentVariable("AWS_REGION");
        _queueUrl = "https://sqs.us-east-1.amazonaws.com/763818760783/anima"; //Environment.GetEnvironmentVariable("AWS_SQS_QUEUE_URL");

        if (string.IsNullOrEmpty(awsAccessKey) || string.IsNullOrEmpty(awsSecretKey) || string.IsNullOrEmpty(awsRegion))
        {
            throw new Exception("Missing AWS environment variables");
        }

        var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(awsAccessKey, awsSecretKey);
        var config = new AmazonSQSConfig { RegionEndpoint = RegionEndpoint.GetBySystemName(awsRegion) };
        _sqsClient = new AmazonSQSClient(awsCredentials, config);
    }

    public async Task SendAsync(string message)
    {
        var sendRequest = new SendMessageRequest
        {
            QueueUrl = _queueUrl, // URL da fila
            MessageBody = message
        };

        await _sqsClient.SendMessageAsync(sendRequest);
    }
}
