using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch.Tokens
{
    class NameToken : Token
    {
        public string Name => this.Value;

        public NameToken()
        {

        }

        public NameToken(string name)
        {
            this.Value = name;
        }
    }
}
