using System;
using System.Collections.Generic;
using System.Linq;

namespace StringSearch.Tokens
{
    class LogicTokenParser : TokenParser
    {
        private readonly HashSet<IOperator> _operators;

        public LogicTokenParser(HashSet<IOperator> operators)
        {
            _operators = operators;
        }

        public override IEnumerable<IToken> Parse(string value)
        {
            var orOperator = _operators.Single(i => i.Type == OperatorType.Or);
            if (value.Contains(orOperator.Token))
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
