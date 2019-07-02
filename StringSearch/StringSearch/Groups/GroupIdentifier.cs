using System;
using System.Collections.Generic;
using System.Linq;

namespace StringSearch.Groups
{
    /// <summary>
    /// Utility for identifying group type
    /// </summary>
    class GroupIdentifier
    {
        private readonly IGroup _group;
        private readonly HashSet<IOperator> _operators;

        /// <summary>
        /// Initializes a new instances of the <see cref="GroupIdentifier"/> class
        /// </summary>
        /// <param name="group"></param>
        /// <param name="operators"></param>
        public GroupIdentifier(IGroup group, HashSet<IOperator> operators)
        {
            _group = group;
            _operators = operators;
        }

        /// <summary>
        /// Indicates whether this is a nested condition
        /// </summary>
        /// <returns></returns>
        public bool IsNestedCondition()
        {
            return _group.HasNestedGroups;
        }

        /// <summary>
        /// Indicates whether this is a range condition
        /// </summary>
        /// <returns></returns>
        public bool IsRangeCondition()
        {
            var between = _operators.Single(i => i.Type == OperatorType.Between);
            var notBetween = _operators.Single(i => i.Type == OperatorType.NotBetween);
            return _group.Value.Contains(between.Value) || _group.Value.Contains(notBetween.Value);
        }

        /// <summary>
        /// Indicates whether this is a single value condition
        /// </summary>
        /// <returns></returns>
        public bool IsSingleCondition()
        {
            return !this.IsNestedCondition() && !this.IsRangeCondition();
        }
    }
}
