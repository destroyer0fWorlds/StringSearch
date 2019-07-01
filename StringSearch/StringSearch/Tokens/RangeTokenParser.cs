using System;
using System.Collections.Generic;
using System.Linq;

namespace StringSearch.Tokens
{
    class RangeTokenParser : TokenParser
    {
        private readonly HashSet<IOperator> _operators;

        public RangeTokenParser(HashSet<IOperator> operators)
        {
            _operators = operators;
        }

        public override IEnumerable<IToken> Parse(string value)
        {
            var sanitizedValue = this.FormatCondition(value);

            var components = sanitizedValue.Split(new[] { "[", "]", "-" }, StringSplitOptions.RemoveEmptyEntries);
            this.ValidateComponents(components);

            // Convert from TokenType to ConditionType
            var op = components[1];
            var @operator = _operators.FirstOrDefault(i => i.Token == op);
            if (@operator == null)
            {
                throw new NotSupportedException($"No operator exists for '{op}'");
            }
            var type = this.ConvertOperatorTypeToConditionType(@operator.Type);

            return new IToken[]
            {
                new NameToken(components[0]),
                new OperatorToken(type),
                new RangeToken(startValue: components[2], endValue: components[3])
            };
        }

        protected override void ValidateComponents(string[] components)
        {
            if (components.Length != 4)
            {
                throw new FormatException("Invalid condition format. A range condition must be in the format: '(property[operator]start-end)'");
            }
        }
    }
}
