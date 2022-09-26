namespace Books.Domain.Models.Actions;

public class UpdateAction : BaseAction
{
    public Book ChangedBook { get; }
    
    public UpdateAction(Book changedBook, Book book) : base(book)
    {
        this.ChangedBook = changedBook;
        this.ActionName = "Update";
    }
}