using System;
using System.Collections.Generic;
using System.Linq;

namespace StringSearch.Filter.Tokens
{
    /// <summary>
    /// Utility for parsing range criteria tokens
    /// </summary>
    class RangeTokenParser : TokenParser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RangeTokenParser"/> class
        /// </summary>
        /// <param name="operators"></param>
        public RangeTokenParser(HashSet<IOperator> operators) : base(operators)
        {
            
        }

        /// <inheritdoc />
        public override IEnumerable<IToken> Parse(string value)
        {
            var sanitizedValue = this.FormatValue(value);

            // Initial split
            var rangeComponents = this.SplitValue(sanitizedValue);
            if (rangeComponents.Length != 3)
            {
                throw new FormatException("Invalid range. A range condition must be in the format: '(property[operator]start-end)'");
            }

            // Split the values
            var rangeValue = rangeComponents[2];
            var valueComponents = rangeValue.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
            if (valueComponents.Length < 2)
            {
                throw new FormatException($"Invalid range. Value '{rangeValue}' does not contain a hyphen. A range's start and end values must be separated by a hyphen like '(property[operator]start-end)'");
            }
            else if (valueComponents.Length > 2)
            {
                throw new FormatException($"Invalid range. Value '{rangeValue}' has too many hyphens. A range's start and end values must be separated by a single hyphen like '(property[operator]start-end)'");
            }

            // Merge all the components
            var components = new string[]
            {
                rangeComponents[0], // 0 - name
                rangeComponents[1], // 1 - op
                valueComponents[0], // 2 - start value
                valueComponents[1]  // 3 - end value
            };

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
                new RangeToken(startValue: components[2], endValue: components[3])
            };
        }
    }
}
