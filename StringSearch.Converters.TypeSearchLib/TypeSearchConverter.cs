using System.Collections.Generic;
using TypeSearch;
using TypeSearch.Criteria;

namespace StringSearch.Converters.TypeSearchLib
{
    /// <summary>
    /// Convert the results of <see cref="StringSearch"/> (<see cref="ICriterion"/>) to <see cref="TypeSearch"/>.<see cref="TypeSearch.WhereCriteria{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TypeSearchConverter<T> : IConverter<WhereCriteria<T>>
        where T : class
    {
        /// <summary>
        /// Convert the results of <see cref="StringSearch"/> (<see cref="ICriterion"/>) to <see cref="TypeSearch"/>.<see cref="TypeSearch.WhereCriteria{T}"/>
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public WhereCriteria<T> ConvertTo(IEnumerable<ICriterion> criteria)
        {
            var typeSearchCriteria = new WhereCriteria<T>();
            typeSearchCriteria.Criteria.AddRange(this.RecursiveConvertTo(criteria));
            return typeSearchCriteria;
        }

        /// <summary>
        /// Recursively convert to a collection of <see cref="TypeSearch"/> criteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        private List<CriteriaContainer<T>> RecursiveConvertTo(IEnumerable<ICriterion> criteria)
        {
            var typeSearchCriteria = new List<CriteriaContainer<T>>();
            foreach (var criterion in criteria)
            {
                var criteriaContainer = new CriteriaContainer<T>()
                {
                    Operator = criterion.LogicOperator == LogicOperatorType.And ? LogicalOperator.And : LogicalOperator.Or
                };

                if (criterion is Criterion singleCriterion)
                {
                    criteriaContainer.SingleCriterion = new SingleCriterion<T>()
                    {
                        Name = singleCriterion.Name,
                        Value = singleCriterion.Value,
                        Operator = this.ConvertConditionTypeToSingleType(singleCriterion.Operator)
                    };
                }
                else if (criterion is RangeCriterion rangeCriterion)
                {
                    criteriaContainer.RangeCriterion = new TypeSearch.Criteria.RangeCriterion()
                    {
                        Name = rangeCriterion.Name,
                        StartValue = rangeCriterion.StartValue,
                        EndValue = rangeCriterion.EndValue,
                        Operator = this.ConvertConditionTypeToRangeType(rangeCriterion.Operator)
                    };
                }
                else
                {
                    var nestedCriteria = (NestedCriterion)criterion;
                    criteriaContainer.CriteriaCollection = new WhereCriteria<T>();
                    criteriaContainer.CriteriaCollection.Criteria.AddRange(this.RecursiveConvertTo(nestedCriteria.Criteria));
                }

                typeSearchCriteria.Add(criteriaContainer);
            }

            return typeSearchCriteria;
        }

        /// <summary>
        /// Convert a <see cref="StringSearch"/> <see cref="ConditionOperatorType"/> to a <see cref="TypeSearch"/> <see cref="SingleOperator"/>
        /// </summary>
        /// <param name="conditionType"></param>
        /// <returns></returns>
        private SingleOperator ConvertConditionTypeToSingleType(ConditionOperatorType conditionType)
        {
            var op = SingleOperator.Equals;
            switch (conditionType)
            {
                case ConditionOperatorType.Like:
                    op = SingleOperator.Like;
                    break;
                case ConditionOperatorType.NotLike:
                    op = SingleOperator.NotLike;
                    break;
                case ConditionOperatorType.StartsWith:
                    op = SingleOperator.StartsWith;
                    break;
                case ConditionOperatorType.DoesNotStartWith:
                    op = SingleOperator.DoesNotStartWith;
                    break;
                case ConditionOperatorType.EndsWith:
                    op = SingleOperator.EndsWith;
                    break;
                case ConditionOperatorType.DoesNotEndWith:
                    op = SingleOperator.DoesNotEndWith;
                    break;
                case ConditionOperatorType.Equals:
                    op = SingleOperator.Equals;
                    break;
                case ConditionOperatorType.NotEquals:
                    op = SingleOperator.NotEquals;
                    break;
                case ConditionOperatorType.LessThan:
                    op = SingleOperator.LessThan;
                    break;
                case ConditionOperatorType.LessThanOrEqualTo:
                    op = SingleOperator.LessThanOrEqualTo;
                    break;
                case ConditionOperatorType.GreaterThan:
                    op = SingleOperator.GreaterThan;
                    break;
                case ConditionOperatorType.GreaterThanOrEqualTo:
                    op = SingleOperator.GreaterThanOrEqualTo;
                    break;
                case ConditionOperatorType.IsNull:
                    op = SingleOperator.IsNull;
                    break;
                case ConditionOperatorType.IsNotNull:
                    op = SingleOperator.IsNotNull;
                    break;
            }
            return op;
        }

        /// <summary>
        /// Convert a <see cref="StringSearch"/> <see cref="ConditionOperatorType"/> to a <see cref="TypeSearch"/> <see cref="RangeOperator"/>
        /// </summary>
        /// <param name="conditionType"></param>
        /// <returns></returns>
        private RangeOperator ConvertConditionTypeToRangeType(ConditionOperatorType conditionType)
        {
            var op = RangeOperator.Between;
            switch (conditionType)
            {
                case ConditionOperatorType.Between:
                    op = RangeOperator.Between;
                    break;
                case ConditionOperatorType.NotBetween:
                    op = RangeOperator.NotBetween;
                    break;
            }
            return op;
        }
    }
}
