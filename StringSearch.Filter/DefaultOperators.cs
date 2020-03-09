using System.Collections.Generic;

namespace StringSearch.Filter
{
    static class DefaultOperators
    {
        public static HashSet<IOperator> LogicOperators => new HashSet<IOperator>()
        {
            new Operator("and", OperatorType.And),
            new Operator("or", OperatorType.Or)
        };

        public static HashSet<IOperator> ConditionOperators => new HashSet<IOperator>()
        {
            new Operator("null", OperatorType.IsNull),
            new Operator("nnull", OperatorType.IsNotNull),
            new Operator("between", OperatorType.Between),
            new Operator("nbetween", OperatorType.NotBetween),
            new Operator("endsw", OperatorType.EndsWith),
            new Operator("nendsw", OperatorType.DoesNotEndWith),
            new Operator("startsw", OperatorType.StartsWith),
            new Operator("nstartsw", OperatorType.DoesNotStartWith),
            new Operator("like", OperatorType.Like),
            new Operator("nlike", OperatorType.NotLike),
            new Operator("eq", OperatorType.Equals),
            new Operator("neq", OperatorType.NotEquals),
            new Operator("gt", OperatorType.GreaterThan),
            new Operator("gte", OperatorType.GreaterThanOrEqualTo),
            new Operator("lt", OperatorType.LessThan),
            new Operator("lte", OperatorType.LessThanOrEqualTo)
        };
    }
}
