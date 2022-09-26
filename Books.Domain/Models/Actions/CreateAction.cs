namespace Books.Domain.Models.Actions;

public class CreateAction : BaseAction
{
    public CreateAction(Book book) : base(book)
    {
        this.ActionName = "Create";
    }
}