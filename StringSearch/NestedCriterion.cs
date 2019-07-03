using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch
{
    /// <summary>
    /// Nested criterion
    /// </summary>
    public class NestedCriterion : ICriterion
    {
        /// <summary>
        /// Property name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Nested criteria
        /// </summary>
        public IEnumerable<ICriterion> Criteria { get; set; }

        /// <summary>
        /// Logic operator indicates how multiple criteria should be combined
        /// </summary>
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
