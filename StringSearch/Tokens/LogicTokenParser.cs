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
        private readonly HashSet<IOperator> _operators;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicTokenParser"/> class
        /// </summary>
        /// <param name="operators"></param>
        public LogicTokenParser(HashSet<IOperator> operators)
        {
            _operators = operators;
        }

        /// <summary>
        /// Parse a string value into tokens
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override IEnumerable<IToken> Parse(string value)
        {
            var orOperator = _operators.Single(i => i.Type == OperatorType.Or);
            if (value.Contains(orOperator.Value))
            {
                return new[] { new LogicToken(LogicOperatorType.Or) };
            }

            return new[] { new LogicToken(LogicOperatorType.And) };
        }

        protected override void ValidateComponents(string[] components)
        {
            throw new NotImplementedException();
        }
    }
}
