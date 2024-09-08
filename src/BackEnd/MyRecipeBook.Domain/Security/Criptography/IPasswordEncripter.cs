namespace MyRecipeBook.Domain.Security.Criptography;
public interface IPasswordEncripter
{
    public string Encrypt(string password);
}
