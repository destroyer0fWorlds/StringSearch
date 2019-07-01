using System;
using System.Collections.Generic;
using System.Linq;
using StringSearch.Groups;
using StringSearch.Tokens;

namespace StringSearch
{
    public class Parser
    {
        private readonly HashSet<IOperator> _operators;

        public Parser()
        {
            // To do: make these configurable so user can override with custom tokens
            _operators = new HashSet<IOperator>()
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
                new Operator("lte", OperatorType.LessThanOrEqualTo),
                new Operator("and", OperatorType.And),
                new Operator("or", OperatorType.Or)
            };
        }

        public IEnumerable<ICriterion> Parse(string filter)
        {
            var groups = new Grouper(_operators).Group(filter);
            return this.Parse(groups);
        }

        public T ParseAs<T>(string filter, IConverter<T> converter) where T : class
        {
            return converter.ConvertTo(this.Parse(filter));
        }

        private IEnumerable<ICriterion> Parse(IEnumerable<IGroup> groups)
        {
            IGroup previousGroup = null;
            var criteria = new List<ICriterion>();
            foreach (var group in groups)
            {
                if (group is ConditionGroup conditionGroup)
                {
                    ICriterion criterion = null;
                    if (group.HasNestedGroups)
                    {
                        criterion = new NestedCriterion() { Criteria = this.Parse(group.NestedGroups) };
                    }
                    else
                    {
                        ICriterionParser criterionParser = null;
                        if (group.Tokens.Any(i => i is RangeToken))
                        {
                            criterionParser = new RangeCriterionParser();
                        }
                        else
                        {
                            criterionParser = new CriterionParser();
                        }
                        criterion = criterionParser.Parse(group.Tokens);
                    }

                    // Join multiple conditions with a logical And/Or
                    if (previousGroup != null && previousGroup is LogicGroup logicGroup)
                    {
                        criterion.LogicOperator = ((LogicToken)logicGroup.Tokens[0]).Operator;
                    }

                    criteria.Add(criterion);
                }

                previousGroup = group;
            }

            return criteria;
        }
    }
}
