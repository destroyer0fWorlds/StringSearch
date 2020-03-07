using System;
using System.Collections.Generic;
using System.Linq;
using StringSearch.Groups;
using StringSearch.Tokens;

namespace StringSearch
{
    /// <summary>
    /// Utility for parsing a filter string to a hierarchical structure of objects
    /// </summary>
    public class Parser
    {
        private readonly HashSet<IOperator> _operators;

        /// <summary>
        /// Initialize an instance of a <see cref="Parser"/> class
        /// </summary>
        public Parser()
        {
            _operators = this.InitOperators(null);
        }

        /// <summary>
        /// Initialize an instance of a <see cref="Parser"/> class specifying override values
        /// </summary>
        /// <param name="operatorOverrides"></param>
        public Parser(Dictionary<OperatorType, string> operatorOverrides)
        {
            if (operatorOverrides == null) { throw new ArgumentNullException(nameof(operatorOverrides), "Operator overrides cannot be null"); }
            
            _operators = this.InitOperators(operatorOverrides);
        }

        /// <summary>
        /// Parse a filter string
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<ICriterion> Parse(string filter)
        {
            var groups = new Grouper(_operators).Group(filter);
            return this.Parse(groups);
        }

        /// <summary>
        /// Parse a filter string
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="operatorOverrides"></param>
        /// <returns></returns>
        public IEnumerable<ICriterion> Parse(string filter, Dictionary<OperatorType, string> operatorOverrides)
        {
            if (operatorOverrides == null) { throw new ArgumentNullException(nameof(operatorOverrides), "Operator overrides cannot be null"); }

            var operators = this.InitOperators(operatorOverrides);
            var groups = new Grouper(operators).Group(filter);
            return this.Parse(groups);
        }

        /// <summary>
        /// Parse a filter string to a custom type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public T ParseAs<T>(string filter, IConverter<T> converter) where T : class
        {
            return converter.ConvertTo(this.Parse(filter));
        }

        /// <summary>
        /// Parse a filter string to a custom type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="converter"></param>
        /// <param name="operatorOverrides"></param>
        /// <returns></returns>
        public T ParseAs<T>(string filter, IConverter<T> converter, Dictionary<OperatorType, string> operatorOverrides) where T : class
        {
            return converter.ConvertTo(this.Parse(filter, operatorOverrides));
        }

        /// <summary>
        /// Recursively parse a collection of groups into criteria
        /// </summary>
        /// <param name="groups"></param>
        /// <returns></returns>
        private IEnumerable<ICriterion> Parse(IEnumerable<IGroup> groups)
        {
            IGroup previousGroup = null;
            var criteria = new List<ICriterion>();
            foreach (var group in groups)
            {
                if (group is CriterionGroup criterionGroup)
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

        private HashSet<IOperator> InitOperators(Dictionary<OperatorType, string> operatorOverrides)
        {
            var allOperators = _operators;

            if (allOperators == null)
            {
                // Build the defaults
                var defaultOperators = new HashSet<IOperator>();
                defaultOperators.UnionWith(DefaultOperators.ConditionOperators);
                defaultOperators.UnionWith(DefaultOperators.LogicOperators);
                allOperators = defaultOperators;
            }

            if (operatorOverrides != null)
            {
                // Convert the overrides to something comparable
                var customOperators = new HashSet<IOperator>(operatorOverrides.Select(i => new Operator() { Type = i.Key, Value = i.Value }));

                // Merge the overrides and defaults. Only use the defaults when an override is not present
                customOperators.UnionWith(allOperators);
                allOperators = customOperators;
            }

            return allOperators;
        }
    }
}
