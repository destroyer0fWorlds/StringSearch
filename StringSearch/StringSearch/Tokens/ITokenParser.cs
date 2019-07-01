using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch.Tokens
{
    interface ITokenParser
    {
        IEnumerable<IToken> Parse(string value);
    }
}
