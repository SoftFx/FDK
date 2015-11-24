namespace Mql2Fdk.Translator.Lexer
{
    public interface IMatcher
    {
        /// <summary>
        /// Return the number of characters that this "regex" or equivalent
        /// matches.
        /// </summary>
        /// <param name="text">The text to be matched</param>
        /// <returns>The number of characters that matched</returns>
        int Match(string text);
    }
}