using Microsoft.OpenApi.Models;
using MyRecipeBook.API.Binders;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyRecipeBook.API.Filters;

public class IdsFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var encryptedIds = context
            .ApiDescription
            .ParameterDescriptions
            .Where(x => x.ModelMetadata.BinderType == typeof(MyRecipeBookIdBinder))
            .ToDictionary(d => d.Name, d => d);

        foreach (var parameter in operation.Parameters)
        {
            if (encryptedIds.TryGetValue(parameter.Name, out var apiParameter))
            {
                parameter.Schema.Format = string.Empty;
                parameter.Schema.Type = "string";
            }
        }

        foreach (var schema in context.SchemaRepository.Schemas.Values)
        {
            foreach (var parameter in schema.Properties)
            {
                if (encryptedIds.TryGetValue(parameter.Key, out var apiParameter))
                {
                    parameter.Value.Format = string.Empty;
                    parameter.Value.Type = "string";
                }
            }
        }
    }
}
