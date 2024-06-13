using MyRecipeBook.Application.Services.Criptography;

namespace CommonTestUtilities.Cryptograpy
{
    public class PasswordEncripterBuilder
    {
        public static PasswordEncripter Build() => new PasswordEncripter("abc1234");
    }
}
