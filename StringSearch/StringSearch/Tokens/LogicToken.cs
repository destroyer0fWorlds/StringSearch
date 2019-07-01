using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch.Tokens
{
    class LogicToken : Token
    {
        public LogicOperatorType Operator { get; set; }

        public LogicToken()
        {

        }

        public LogicToken(LogicOperatorType @operator)
        {
            Operator = @operator;
        }
    }
}
