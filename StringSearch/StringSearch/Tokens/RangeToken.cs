using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch.Tokens
{
    class RangeToken : Token
    {
        public string StartValue { get; set; }

        public string EndValue { get; set; }

        public RangeToken()
        {
            
        }

        public RangeToken(string startValue, string endValue)
        {
            StartValue = startValue;
            EndValue = endValue;
        }
    }
}
