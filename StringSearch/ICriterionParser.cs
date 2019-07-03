using System;
using System.Collections.Generic;
using System.Text;
using StringSearch.Tokens;

namespace StringSearch
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
