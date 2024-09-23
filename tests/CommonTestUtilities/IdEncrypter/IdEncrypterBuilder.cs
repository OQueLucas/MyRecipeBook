using Sqids;

namespace CommonTestUtilities.IdEncrypter;
public class IdEncrypterBuilder
{
    public static SqidsEncoder<long> Build()
    {
        return new SqidsEncoder<long>(new()
        {
            MinLength = 3,
            Alphabet = "LsmcFyBgQWqSh1wfKADXGiI0Yu93vRU2enMopdjPa87rN4TzZblCJxEH6tkV5O"
        });
    }
}
