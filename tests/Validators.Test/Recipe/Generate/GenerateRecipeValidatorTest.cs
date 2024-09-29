using CommonTestUtilities.Requests;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.Recipe.Generate;
using MyRecipeBook.Domain.ValueObject;
using MyRecipeBook.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Validators.Test.Recipe.Generate;
public class GenerateRecipeValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new GenerateRecipeValidator();

        var request = RequestGenerateRecipeJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_More_Maximum_Ingredient()
    {

        var validator = new GenerateRecipeValidator();

        var request = RequestGenerateRecipeJsonBuilder.Build(count: MyRecipeBookRuleConstants.MAXIMUM_INGREDIENTS_GENERATE_RECIPE + 1);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.INVALID_NUMBER_INGREDIENTS));
    }

    [Fact]
    public void Error_Duplicated_Ingredient()
    {
        var validator = new GenerateRecipeValidator();

        var request = RequestGenerateRecipeJsonBuilder.Build(count: MyRecipeBookRuleConstants.MAXIMUM_INGREDIENTS_GENERATE_RECIPE - 1);
        request.Ingredients.Add(request.Ingredients[0]);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.DUPLICATED_INGREDIENTS_IN_LIST));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("         ")]
    [InlineData("")]
    [SuppressMessage("Usage", "xUnit1012:Null should only be used for nullable parameters", Justification = "Because it is a unit test")]
    public void Error_Empty_Ingredient(string ingredient)
    {
        var validator = new GenerateRecipeValidator();

        var request = RequestGenerateRecipeJsonBuilder.Build(count: 1);
        request.Ingredients.Add(ingredient);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.INGREDIENT_EMPTY));
    }

    [Fact]
    public void Error_Ingredient_Not_Followind_Pattern()
    {
        var validator = new GenerateRecipeValidator();

        var request = RequestGenerateRecipeJsonBuilder.Build(count: MyRecipeBookRuleConstants.MAXIMUM_INGREDIENTS_GENERATE_RECIPE - 1);
        request.Ingredients.Add("This is an invalid ingredient because is too long");

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.INGREDIENT_NOT_FOLLOWING_PATTERN));
    }
}
