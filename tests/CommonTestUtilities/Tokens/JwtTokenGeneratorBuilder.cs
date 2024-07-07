using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Infrastructure.Security.Tokens.Access.Generator;

namespace CommonTestUtilities.Tokens
{
    public static class JwtTokenGeneratorBuilder
    {
        public static IAccessTokenGenerator Build() => new JwtTokenGenerator(5, "tttttttttttttttttttttttttttttttt");
    }
}
