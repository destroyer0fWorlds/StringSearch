using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch.Tokens
{
    abstract class TokenParser : ITokenParser
    {
        public abstract IEnumerable<IToken> Parse(string value);

        protected string FormatCondition(string condition)
        {
            var value = (condition ?? string.Empty).Trim();

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

        protected abstract void ValidateComponents(string[] components);
    }
}
