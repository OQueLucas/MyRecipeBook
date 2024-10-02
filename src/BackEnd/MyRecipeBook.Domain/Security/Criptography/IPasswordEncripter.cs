namespace MyRecipeBook.Domain.Security.Criptography;
public interface IPasswordEncripter
{
    public string Encrypt(string password);
    public bool IsValid(string password, string passwordHash);
}
