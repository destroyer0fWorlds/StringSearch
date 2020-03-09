using System.Collections.Generic;
using StringSearch.Filter.Tokens;

namespace StringSearch.Filter
{
    /// <summary>
    /// Utility for parsing tokens into criteria
    /// </summary>
    interface ICriterionParser
    {
        /// <summary>
        /// Parse a collection of tokens as a criterion
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        ICriterion Parse(IEnumerable<IToken> tokens);
    }
}
