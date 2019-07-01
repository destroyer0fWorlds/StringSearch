using System;
using System.Collections.Generic;
using System.Linq;

namespace StringSearch.Groups
{
    class GroupIdentifier
    {
        private readonly IGroup _group;
        private readonly HashSet<IOperator> _operators;

        public GroupIdentifier(IGroup group, HashSet<IOperator> operators)
        {
            _group = group;
            _operators = operators;
        }

        public bool IsNestedCondition()
        {
            return _group.HasNestedGroups;
        }

        public bool IsRangeCondition()
        {
            var between = _operators.Single(i => i.Type == OperatorType.Between);
            var notBetween = _operators.Single(i => i.Type == OperatorType.NotBetween);
            return _group.Value.Contains(between.Token) || _group.Value.Contains(notBetween.Token);
        }

        public bool IsSingleCondition()
        {
            return !this.IsNestedCondition() && !this.IsRangeCondition();
        }
    }
}
