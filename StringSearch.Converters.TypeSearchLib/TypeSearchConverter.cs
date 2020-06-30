using System.Collections.Generic;
using TypeSearch;
using TypeSearch.Criteria;
using StringSearch.Filter;
using StringSearch.OrderBy;

namespace StringSearch.Converters.TypeSearchLib
{
    /// <summary>
    /// Convert <see cref="StringSearch"/> filter and order by results to 
    /// <see cref="TypeSearch"/>.<see cref="TypeSearch.FilterCriteria{T}"/> and 
    /// <see cref="TypeSearch"/>.<see cref="TypeSearch.SortCriteria{T}"/> respectively
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TypeSearchConverter<T> : IFilterConverter<FilterCriteria<T>>, IOrderByConverter<SortCriteria<T>>
        where T : class
    {
        /// <summary>
        /// Convert the results of <see cref="StringSearch"/> (<see cref="ICriterion"/>) to <see cref="TypeSearch"/>.<see cref="TypeSearch.FilterCriteria{T}"/>
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public FilterCriteria<T> ConvertTo(IEnumerable<ICriterion> criteria)
        {
            var typeSearchCriteria = new FilterCriteria<T>();
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
                else if (criterion is StringSearch.Filter.RangeCriterion rangeCriterion)
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
                    criteriaContainer.NestedFilter = new FilterCriteria<T>();
                    criteriaContainer.NestedFilter.Criteria.AddRange(this.RecursiveConvertTo(nestedCriteria.Criteria));
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

        /// <summary>
        /// Convert the results of <see cref="StringSearch"/> (<see cref="IOrderedCriterion"/>) to <see cref="TypeSearch"/>.<see cref="TypeSearch.SortCriteria{T}"/>
        /// </summary>
        /// <param name="orderedCriteria"></param>
        /// <returns></returns>
        public SortCriteria<T> ConvertTo(IEnumerable<IOrderedCriterion> orderedCriteria)
        {
            var sortCriteria = new SortCriteria<T>();
            foreach (var orderedCriterion in orderedCriteria)
            {
                var sortCriterion = new SortCriterion() { Name = orderedCriterion.Name };
                if (orderedCriterion.Direction == OrderByDirection.Ascending)
                {
                    // Ascending
                    sortCriterion.SortDirection = SortDirection.Ascending;
                }
                else
                {
                    // Descending
                    sortCriterion.SortDirection = SortDirection.Descending;
                }
                sortCriteria.Criteria.Add(sortCriterion);
            }
            return sortCriteria;
        }
    }
}
