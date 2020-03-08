using System;
using System.Collections.Generic;
using System.Linq;

namespace StringSearch.Tokens
{
    /// <summary>
    /// Utility for parsing and/or tokens
    /// </summary>
    class LogicTokenParser : TokenParser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogicTokenParser"/> class
        /// </summary>
        /// <param name="operators"></param>
        public LogicTokenParser(HashSet<IOperator> operators) : base(operators)
        {
            
        }

        /// <inheritdoc />
        public override IEnumerable<IToken> Parse(string value)
        {
            var orOperator = this.Operators.Single(i => i.Type == OperatorType.Or);
            if (value.Contains(orOperator.Value))
            {
                return new[] { new LogicToken(LogicOperatorType.Or) };
            }

            return new[] { new LogicToken(LogicOperatorType.And) };
        }
    }
}
