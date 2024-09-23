using AutoMapper;
using CommonTestUtilities.IdEncrypter;
using MyRecipeBook.Application.Services.AutoMapper;

namespace CommonTestUtilities.Mapper;

public class MapperBuilder
{
    public static IMapper Build()
    {
        var idEncripter = IdEncrypterBuilder.Build();

        return new MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping(idEncripter));
        }).CreateMapper();
    }
}
