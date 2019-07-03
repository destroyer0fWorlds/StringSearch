using System;
using System.Collections.Generic;
using System.Linq;

namespace StringSearch.Tokens
{
    /// <summary>
    /// Utility for parsing single value criteria tokens
    /// </summary>
    class SingleTokenParser : TokenParser
    {
        private readonly HashSet<IOperator> _operators;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleTokenParser"/> class
        /// </summary>
        /// <param name="operators"></param>
        public SingleTokenParser(HashSet<IOperator> operators)
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
            var sanitizedValue = this.FormatValue(value);

            var components = sanitizedValue.Split(new[] { "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
            this.ValidateComponents(components);

            // Convert from TokenType to ConditionType
            var op = components[1];
            var @operator = _operators.FirstOrDefault(i => i.Value == op);
            if (@operator == null)
            {
                throw new NotSupportedException($"No operator exists for '{op}'");
            }
            var type = this.ConvertOperatorTypeToConditionType(@operator.Type);

            return new IToken[]
            {
                new NameToken(components[0]),
                new OperatorToken(type),
                new ValueToken(components[2])
            };
        }

        protected override void ValidateComponents(string[] components)
        {
            if (components.Length != 3)
            {
                throw new FormatException("Invalid condition format. A single condition must be in the format: '(property[operator]value)'");
            }
        }
    }
}
