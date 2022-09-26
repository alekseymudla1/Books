using Amazon.SQS;
using Amazon.SQS.Model;
using Books.Interfaces;

namespace Books.Api.Managers;

public class QueueClient : IQueueClient
{
    private readonly string _sqsUrl;
    private readonly IAmazonSQS _client;
    
    public QueueClient(IAmazonSQS client)
    {
        _client = client;
        _sqsUrl = Environment.GetEnvironmentVariable("SQS_URL") ?? throw new ArgumentException("Environment variable SQS_URL not defined");
    }
    
    public async Task SendAction(string body)
    {
        SendMessageRequest sendMessageRequest = new SendMessageRequest();
        sendMessageRequest.QueueUrl = _sqsUrl;
        sendMessageRequest.MessageGroupId = "Booksdemo";
        sendMessageRequest.MessageDeduplicationId = Guid.NewGuid().ToString();
        sendMessageRequest.MessageBody = body;

        await _client.SendMessageAsync(sendMessageRequest);
    }
}