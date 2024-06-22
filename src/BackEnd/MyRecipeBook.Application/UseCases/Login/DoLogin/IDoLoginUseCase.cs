using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin
{
    internal interface IDoLoginUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
    }
}
