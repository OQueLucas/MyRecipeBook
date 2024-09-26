using CommonTestUtilities.IdEncrypter;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;

namespace WebApi.Test.Recipe.Register;
public class UpdateRecipeInvalidTokenTest : MyRecipeBookClassFixture
{
    private const string METHOD = "recipe";

    public UpdateRecipeInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Error_Token_Invalid()
    {
        var request = RequestRecipeJsonBuilder.Build();
        var id = IdEncrypterBuilder.Build().Encode(1);

        var response = await DoPut(method: $"{METHOD}/{id}", request: request, token: "tokenInvalid");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized); 
    }

    [Fact]
    public async Task Error_Without_Token()
    {
        var request = RequestRecipeJsonBuilder.Build();
        var id = IdEncrypterBuilder.Build().Encode(1);

        var response = await DoPut(method: $"{METHOD}/{id}", request: request, token: string.Empty);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Token_With_User_NotFound()
    {
        var request = RequestRecipeJsonBuilder.Build();
        var id = IdEncrypterBuilder.Build().Encode(1);

        var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

        var response = await DoPut(method: $"{METHOD}/{id}", request: request, token: token);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
