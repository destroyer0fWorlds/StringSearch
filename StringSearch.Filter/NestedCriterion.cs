using System.Collections.Generic;

namespace StringSearch.Filter
{
    /// <summary>
    /// Nested criterion
    /// </summary>
    public class NestedCriterion : ICriterion
    {
        /// <inheritdoc />
        public string Raw { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <summary>
        /// Nested criteria
        /// </summary>
        public IEnumerable<ICriterion> Criteria { get; set; }

        /// <inheritdoc />
        public LogicOperatorType LogicOperator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NestedCriterion"/> class
        /// </summary>
        public NestedCriterion()
        {
            this.Name = "Nested";
        }
    }
}
