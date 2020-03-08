using System.Collections.Generic;

namespace StringSearch.Tokens
{
    /// <summary>
    /// Utility for parsing tokens out of a string value
    /// </summary>
    interface ITokenParser
    {
        /// <summary>
        /// Parse a string value into tokens
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IEnumerable<IToken> Parse(string value);
    }
}
