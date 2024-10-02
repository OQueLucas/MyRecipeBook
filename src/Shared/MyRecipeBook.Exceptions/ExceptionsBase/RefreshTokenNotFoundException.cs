using System.Net;

namespace MyRecipeBook.Exceptions.ExceptionsBase;
public class RefreshTokenNotFoundException : MyRecipeBookException
{
    public RefreshTokenNotFoundException() : base(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE) { }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}
