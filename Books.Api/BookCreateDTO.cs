using Books.Domain.Models;

namespace Books.Api;

public class BookCreateDTO
{
    public string? Isbn { get; init; }
    
    public string? Title { get; init; }
    
    public string? Description { get; init; }
    
    public Book ToBook()
    {
        return new Book()
        {
            BookId = Guid.NewGuid().ToString(),
            Title = this.Title,
            Isbn = this.Isbn,
            Description = this.Description
        };
    }
}