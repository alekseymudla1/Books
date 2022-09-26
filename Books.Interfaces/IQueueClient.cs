namespace Books.Interfaces;

public interface IQueueClient
{
    Task SendAction(string body);
}