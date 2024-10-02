using System.Net;

namespace MyRecipeBook.Exceptions.ExceptionsBase;
public class RefreshTokenExpiredException : MyRecipeBookException
{
    public RefreshTokenExpiredException() : base(ResourceMessagesException.EXPIRED_SESSION) { }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}
