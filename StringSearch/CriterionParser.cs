using System.Collections.Generic;
using StringSearch.Tokens;

namespace StringSearch
{
    /// <summary>
    /// Single value criterion parser
    /// </summary>
    class CriterionParser : ICriterionParser
    {
        /// <inheritdoc />
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
