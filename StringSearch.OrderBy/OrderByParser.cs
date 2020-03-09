using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch.OrderBy
{
    /// <summary>
    /// Utility for parsing an order by string to a hierarchical structure of objects
    /// </summary>
    public class OrderByParser
    {
        private readonly Dictionary<OrderByDirection, string> _orderByDirections;

        /// <summary>
        /// Initialize an instance of a <see cref="OrderByParser"/> class
        /// </summary>
        public OrderByParser()
        {
            _orderByDirections = this.InitDirections(null);
        }

        /// <summary>
        /// Initialize an instance of a <see cref="OrderByParser"/> class specifying override values
        /// </summary>
        /// <param name="orderByOverrides"></param>
        public OrderByParser(Dictionary<OrderByDirection, string> orderByOverrides)
        {
            if (orderByOverrides == null) { throw new ArgumentNullException(nameof(orderByOverrides), "Order by overrides cannot be null"); }

            _orderByDirections = this.InitDirections(orderByOverrides);
        }

        /// <summary>
        /// Parse an order by string
        /// </summary>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public IEnumerable<IOrderedCriterion> Parse(string orderBy)
        {
            return this.ParseOrderByString(orderBy, _orderByDirections);
        }

        /// <summary>
        /// Parse an order by string
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="orderByOverrides"></param>
        /// <returns></returns>
        public IEnumerable<IOrderedCriterion> Parse(string orderBy, Dictionary<OrderByDirection, string> orderByOverrides)
        {
            if (orderByOverrides == null) { throw new ArgumentNullException(nameof(orderByOverrides), "Order by overrides cannot be null"); }

            var orderByDirections = this.InitDirections(orderByOverrides);

            return this.ParseOrderByString(orderBy, orderByDirections);
        }

        /// <summary>
        /// Parse an order by string to a custom type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="orderBy"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public T ParseAs<T>(string orderBy, IOrderByConverter<T> converter) where T : class
        {
            return converter.ConvertTo(this.Parse(orderBy, _orderByDirections));
        }

        /// <summary>
        /// Parse an order by string to a custom type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="orderBy"></param>
        /// <param name="converter"></param>
        /// <param name="orderByOverrides"></param>
        /// <returns></returns>
        public T ParseAs<T>(string orderBy, IOrderByConverter<T> converter, Dictionary<OrderByDirection, string> orderByOverrides) where T : class
        {
            return converter.ConvertTo(this.Parse(orderBy, _orderByDirections));
        }

        /// <summary>
        /// Initialize the list of directions by combining all available into a single collection
        /// </summary>
        /// <param name="orderByOverrides"></param>
        /// <returns></returns>
        private Dictionary<OrderByDirection, string> InitDirections(Dictionary<OrderByDirection, string> orderByOverrides)
        {
            var allDirections = _orderByDirections;

            if (allDirections == null)
            {
                // Build the defaults
                allDirections = new Dictionary<OrderByDirection, string>()
                {
                    { OrderByDirection.Ascending, "asc" },
                    { OrderByDirection.Descending, "desc" }
                };
            }

            if (orderByOverrides != null)
            {
                // Merge the overrides and defaults
                if (orderByOverrides.ContainsKey(OrderByDirection.Ascending))
                {
                    // Take the ascending override
                    allDirections[OrderByDirection.Ascending] = orderByOverrides[OrderByDirection.Ascending];
                }
                if (orderByOverrides.ContainsKey(OrderByDirection.Descending))
                {
                    // Take the descending override
                    allDirections[OrderByDirection.Descending] = orderByOverrides[OrderByDirection.Descending];
                }
            }

            return allDirections;
        }

        /// <summary>
        /// Parse the order by string into a collection of <see cref="IOrderedCriterion"/>
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="orderByDirections"></param>
        /// <returns></returns>
        private IEnumerable<IOrderedCriterion> ParseOrderByString(string orderBy, Dictionary<OrderByDirection, string> orderByDirections)
        {
            var orderedCriteria = new List<IOrderedCriterion>();
            if (string.IsNullOrWhiteSpace(orderBy)) { return orderedCriteria; }

            var ascDirection = orderByDirections[OrderByDirection.Ascending];
            var ascValue = $"[{ascDirection}]";

            var descDirection = orderByDirections[OrderByDirection.Descending];
            var descValue = $"[{descDirection}]";

            var orderByCriteria = orderBy.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var orderByCriterion in orderByCriteria)
            {
                var orderedCriterion = new OrderedCriterion();
                var trimmedCriterion = orderByCriterion.Trim();
                if (trimmedCriterion.EndsWith(ascValue))
                {
                    orderedCriterion.Name = trimmedCriterion.Substring(0, trimmedCriterion.Length - ascValue.Length);
                    orderedCriterion.Direction = OrderByDirection.Ascending;
                }
                else if (trimmedCriterion.EndsWith(descValue))
                {
                    orderedCriterion.Name = trimmedCriterion.Substring(0, trimmedCriterion.Length - descValue.Length);
                    orderedCriterion.Direction = OrderByDirection.Descending;
                }
                else
                {
                    var unknownDirectionMessage = $"Invalid format '{orderByCriterion}'. Order by conditions must be in the format: name[direction].";
                    throw new FormatException(unknownDirectionMessage);
                }

                orderedCriteria.Add(orderedCriterion);
            }

            return orderedCriteria;
        }
    }
}
