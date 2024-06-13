namespace MyRecipeBook.Domain.Repositories.User;
public interface IUnitOfWork
{
    public Task Commit();
}
