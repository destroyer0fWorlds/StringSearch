using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch.Tokens
{
    /// <summary>
    /// Base token parser
    /// </summary>
    abstract class TokenParser : ITokenParser
    {
        /// <summary>
        /// Parse a string value into tokens
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract IEnumerable<IToken> Parse(string value);

        /// <summary>
        /// Format the incoming value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string FormatValue(string value)
        {
            value = (value ?? string.Empty).Trim();

            if (value.StartsWith("("))
            {
                value = value.Substring(1);
            }
            if (value.EndsWith(")"))
            {
                value = value.Substring(0, value.Length - 1);
            }

            return value;
        }

        /// <summary>
        /// Convert a <see cref="OperatorType"/> enum to a <see cref="ConditionOperatorType"/> enum
        /// </summary>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        protected ConditionOperatorType ConvertOperatorTypeToConditionType(OperatorType tokenType)
        {
            var conditionType = ConditionOperatorType.Equals;
            switch (tokenType)
            {
                case OperatorType.Between:
                    conditionType = ConditionOperatorType.Between;
                    break;
                case OperatorType.NotBetween:
                    conditionType = ConditionOperatorType.NotBetween;
                    break;
                case OperatorType.Like:
                    conditionType = ConditionOperatorType.Like;
                    break;
                case OperatorType.NotLike:
                    conditionType = ConditionOperatorType.NotLike;
                    break;
                case OperatorType.StartsWith:
                    conditionType = ConditionOperatorType.StartsWith;
                    break;
                case OperatorType.DoesNotStartWith:
                    conditionType = ConditionOperatorType.DoesNotStartWith;
                    break;
                case OperatorType.EndsWith:
                    conditionType = ConditionOperatorType.EndsWith;
                    break;
                case OperatorType.DoesNotEndWith:
                    conditionType = ConditionOperatorType.DoesNotEndWith;
                    break;
                case OperatorType.Equals:
                    conditionType = ConditionOperatorType.Equals;
                    break;
                case OperatorType.NotEquals:
                    conditionType = ConditionOperatorType.NotEquals;
                    break;
                case OperatorType.LessThan:
                    conditionType = ConditionOperatorType.LessThan;
                    break;
                case OperatorType.LessThanOrEqualTo:
                    conditionType = ConditionOperatorType.LessThanOrEqualTo;
                    break;
                case OperatorType.GreaterThan:
                    conditionType = ConditionOperatorType.GreaterThan;
                    break;
                case OperatorType.GreaterThanOrEqualTo:
                    conditionType = ConditionOperatorType.GreaterThanOrEqualTo;
                    break;
                case OperatorType.IsNull:
                    conditionType = ConditionOperatorType.IsNull;
                    break;
                case OperatorType.IsNotNull:
                    conditionType = ConditionOperatorType.IsNotNull;
                    break;
            }
            return conditionType;
        }

        /// <summary>
        /// Validate that the parsed components are of the right size, shape, color, weight, etc.
        /// </summary>
        /// <param name="components"></param>
        protected abstract void ValidateComponents(string[] components);
    }
}
