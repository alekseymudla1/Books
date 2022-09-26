using Amazon.DynamoDBv2.DataModel;

namespace Books.Domain.Models;

[DynamoDBTable("booksDemo")]
public class Book
{
    [DynamoDBHashKey("bookId")]
    public string BookId { get; init; }
    
    [DynamoDBProperty("ISBN")]
    public string Isbn { get; init; }
    
    [DynamoDBProperty("Title")]
    public string Title { get; init; }
    
    [DynamoDBProperty("Description")]
    public string Description { get; init; }
}