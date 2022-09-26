namespace Books.Domain.Models.Actions;

public abstract class BaseAction
{
    public Book Book { get; init; }
    
    public string ActionName { get; protected init; }

    protected BaseAction(Book book)
    {
        this.Book = book;
    }
}