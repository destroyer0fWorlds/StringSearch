using System;
using System.Collections.Generic;
using System.Text;
using StringSearch.Tokens;

namespace StringSearch
{
    interface ICriterionParser
    {
        ICriterion Parse(IEnumerable<IToken> tokens);
    }
}
