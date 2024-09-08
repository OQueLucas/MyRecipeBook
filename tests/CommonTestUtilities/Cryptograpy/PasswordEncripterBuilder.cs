using MyRecipeBook.Domain.Security.Criptography;
using MyRecipeBook.Infrastructure.Security.Criptography;

namespace CommonTestUtilities.Cryptograpy
{
    public  class PasswordEncripterBuilder
    {
        public static IPasswordEncripter Build() => new Sha512Encripter("abc1234");
    }
}
