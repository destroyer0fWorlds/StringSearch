using System;
using System.Collections.Generic;
using System.Text;
using StringSearch.Tokens;

namespace StringSearch
{
    class CriterionParser : ICriterionParser
    {
        public ICriterion Parse(IEnumerable<IToken> tokens)
        {
            var valueCriterion = new Criterion();
            foreach (var token in tokens)
            {
                switch (token)
                {
                    case NameToken nameToken:
                        valueCriterion.Name = nameToken.Name;
                        break;
                    case OperatorToken operatorToken:
                        valueCriterion.Operator = operatorToken.Operator;
                        break;
                    case ValueToken valueToken:
                        valueCriterion.Value = valueToken.Value;
                        break;
                }
            }
            return valueCriterion;
        }
    }
}
