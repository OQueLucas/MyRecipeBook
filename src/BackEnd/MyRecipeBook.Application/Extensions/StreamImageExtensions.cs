using FileTypeChecker.Extensions;
using FileTypeChecker.Types;

namespace MyRecipeBook.Application.Extensions;
public static class StreamImageExtensions
{
    public static (bool isValidImage, string extension) ValidateAndGetImageExtensions(this Stream stream)
    {
        var result = (false, string.Empty);
        if (stream.Is<PortableNetworkGraphic>())
            result = (true, NomalizeExtension(PortableNetworkGraphic.TypeExtension));
        else if (stream.Is<JointPhotographicExpertsGroup>())
            result = (true, NomalizeExtension(JointPhotographicExpertsGroup.TypeExtension));

        stream.Position = 0;

        return result;
    }

    private static string NomalizeExtension(string extension)
    {
        return extension.StartsWith('.') ? extension : $".{extension}";
    }
}
