﻿using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.API.Attributes;
using MyRecipeBook.Application.UseCases.Recipe.Register;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.API.Controller;
[AuthenticatedUser]
public class RecipeController : MyRecipeBookController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredRecipeJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterRecipeUseCase useCase, [FromBody] RequestRecipeJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
