using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch
{
    /// <summary>
    /// Range criterion
    /// </summary>
    public class RangeCriterion : ICriterion
    {
        /// <summary>
        /// Property name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Operator
        /// </summary>
        public ConditionOperatorType Operator { get; set; }

        /// <summary>
        /// Start value
        /// </summary>
        public object StartValue { get; set; }

        /// <summary>
        /// End value
        /// </summary>
        public object EndValue { get; set; }

        /// <summary>
        /// Logic operator indicates how multiple criteria should be combined
        /// </summary>
        public LogicOperatorType LogicOperator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeCriterion"/> class
        /// </summary>
        public RangeCriterion()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeCriterion"/> class with the specified name
        /// </summary>
        /// <param name="name"></param>
        public RangeCriterion(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeCriterion"/> class with the specified name, operator, and values
        /// </summary>
        /// <param name="name"></param>
        /// <param name="operator"></param>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        public RangeCriterion(string name, ConditionOperatorType @operator, object startValue, object endValue)
        {
            this.Name = name;
            this.Operator = @operator;
            this.StartValue = startValue;
            this.EndValue = endValue;
        }
    }
}
