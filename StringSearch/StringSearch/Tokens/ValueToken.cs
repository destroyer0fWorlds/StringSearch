using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch.Tokens
{
    class ValueToken : Token
    {
        public ValueToken()
        {

        }

        public ValueToken(string value)
        {
            this.Value = value;
        }
    }
}
