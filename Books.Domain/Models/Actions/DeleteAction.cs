namespace Books.Domain.Models.Actions;

public class DeleteAction : BaseAction
{
    public DeleteAction(Book book) : base(book)
    {
        this.ActionName = "Delete";
    }
}