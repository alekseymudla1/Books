using Books.Domain.Models;

namespace Books.Api;

public class BookDTO
{
    public string? BookId { get; init; }
 
    public string? Isbn { get; init; }
    
    public string? Title { get; init; }
    
    public string? Description { get; init; }
    
    public Book ToBook()
    {
        return new Book()
        {
            BookId = this.BookId,
            Title = this.Title,
            Isbn = this.Isbn,
            Description = this.Description
        };
    }
}