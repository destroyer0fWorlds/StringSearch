using System.Collections.Generic;
using StringSearch.Tokens;

namespace StringSearch
{
    /// <summary>
    /// Range criterion parser
    /// </summary>
    class RangeCriterionParser : ICriterionParser
    {
        /// <inheritdoc />
        public ICriterion Parse(IEnumerable<IToken> tokens)
        {
            var rangeCriterion = new RangeCriterion();
            foreach (var token in tokens)
            {
                switch (token)
                {
                    case NameToken nameToken:
                        rangeCriterion.Name = nameToken.Name;
                        break;
                    case OperatorToken operatorToken:
                        rangeCriterion.Operator = operatorToken.Operator;
                        break;
                    case RangeToken rangeToken:
                        rangeCriterion.StartValue = rangeToken.StartValue;
                        rangeCriterion.EndValue = rangeToken.EndValue;
                        break;
                }
            }
            return rangeCriterion;
        }
    }
}
