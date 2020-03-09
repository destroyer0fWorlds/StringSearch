using System;
using System.Collections.Generic;
using System.Linq;

namespace StringSearch.Filter.Tokens
{
    /// <summary>
    /// Utility for parsing single value criteria tokens
    /// </summary>
    class SingleTokenParser : TokenParser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleTokenParser"/> class
        /// </summary>
        /// <param name="operators"></param>
        public SingleTokenParser(HashSet<IOperator> operators) : base(operators)
        {
            
        }

        /// <inheritdoc />
        public override IEnumerable<IToken> Parse(string value)
        {
            var sanitizedValue = this.FormatValue(value);

            var components = this.SplitValue(sanitizedValue);
            if (components.Length != 3)
            {
                throw new FormatException("Invalid format. A single condition must be in the format: '(property[operator]value)'");
            }

            // Convert from TokenType to ConditionType
            var op = components[1];
            var @operator = this.Operators.FirstOrDefault(i => i.Value == op);
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
    }
}
