namespace Mql2Fdk
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MqlAdapter
    {
        #region String Functions

        /// <summary>
        /// Search for a substring. Returns the position in the string from which the searched substring begins, or -1 if the substring has not been found.
        /// </summary>
        /// <param name="text">String to search in.</param>
        /// <param name="matched_text">Substring to search for.</param>
        /// <param name="start">Position in the string to start search from.</param>
        /// <returns></returns>
        protected static int StringFind(string text, string matched_text, int start = 0)
        {
            var result = text.IndexOf(matched_text, start);
            return result;
        }

        /// <summary>
        /// Extracts a substring from text string starting from the given position.
        /// The function returns a copy of the extracted substring if possible, otherwise, it returns an empty string. 
        /// </summary>
        /// <param name="text">String from which the substring will be extracted</param>
        /// <param name="start">Substring starting index. Can be from 0 to StringLen(text)-1.</param>
        /// <param name="length">Length of the substring extracted. If the parameter value exceeds or equals to 0 or the parameter is not specified, the substring will be extracted starting from the given position and up to the end of the string.</param>
        /// <returns></returns>
        protected static string StringSubstr(string text, int start, int length = 0)
        {
            return length == 0 ? text.Substring(start) : text.Substring(start, length);
        }

        /// <summary>
        /// Returns character (code) from the specified position in the string. 
        /// </summary>
        /// <param name="text">String</param>
        /// <param name="pos">Char position in the string. Can be from 0 to StringLen(text)-1.</param>
        /// <returns></returns>
        protected static int StringGetChar(string text, int pos)
        {
            return text[pos];
        }

        /// <summary>
        /// The function cuts line feed characters, spaces and tabs in the left part of the string. The function returns a copy of the trimmed string, if possible. Otherwise, it returns an empty string. 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected static string StringTrimLeft(string text)
        {
            return text.TrimStart();
        }

        /// <summary>
        /// The function cuts line feed characters, spaces and tabs in the right part of the string. The function returns a copy of the trimmed string, if possible. Otherwise, it returns an empty string. 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected static string StringTrimRight(string text)
        {
            return text.TrimEnd();
        }

        /// <summary>
        /// Returns character count in a string. 
        /// </summary>
        /// <param name="text">String where the length must be calculated.</param>
        /// <returns></returns>
        protected static int StringLen(string text)
        {
            return text.Length;
        }

        /// <summary>
        /// Forms a string of the data passed and returns it. Parameters can be of any type. Amount of passed parameters cannot exceed 64.
        /// Parameters are transformed into strings according the same rules as those used in functions of Print(), Alert() and Comment(). The returned string is obtained as a result of concatenate of strings converted from the function parameters.
        /// The StringConcatenate() works faster and more memory-saving than when strings are concatenated using addition operations (+). 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected static string StringConcatenate(params object[] args)
        {
            return string.Concat(args);
        }

        #endregion
    }
}
