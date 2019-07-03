using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch
{
    /// <summary>
    /// Base criterion
    /// </summary>
    public class Criterion : ICriterion
    {
        /// <summary>
        /// Property name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Operator type
        /// </summary>
        public ConditionOperatorType Operator { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Logic operator indicates how multiple criteria should be combined
        /// </summary>
        public LogicOperatorType LogicOperator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Criterion"/> class
        /// </summary>
        public Criterion()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Criterion"/> class with the specified name
        /// </summary>
        /// <param name="name"></param>
        public Criterion(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Criterion"/> class with the specified name, operator, and value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="operator"></param>
        /// <param name="value"></param>
        public Criterion(string name, ConditionOperatorType @operator, object value)
        {
            this.Name = name;
            this.Operator = @operator;
            this.Value = value;
        }
    }
}
