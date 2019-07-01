using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch.Tokens
{
    class OperatorToken : Token
    {
        public ConditionOperatorType Operator { get; set; }

        public OperatorToken()
        {

        }

        public OperatorToken(ConditionOperatorType @operator)
        {
            Operator = @operator;
        }
    }
}
