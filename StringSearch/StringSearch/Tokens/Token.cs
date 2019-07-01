using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch.Tokens
{
    abstract class Token : IToken
    {
        public string Value { get; set; }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
