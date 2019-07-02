using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch
{
    /// <summary>
    /// Base operator
    /// </summary>
    public class Operator : IOperator
    {
        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public OperatorType Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Operator"/> class
        /// </summary>
        public Operator()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Operator"/> class with the supplied value and operation
        /// </summary>
        /// <param name="value"></param>
        /// <param name="operation"></param>
        public Operator(string value, OperatorType operation)
        {
            Value = value;
            Type = operation;
        }

        public override string ToString()
        {
            return this.Value;
        }

        public override bool Equals(object obj)
        {
            var otherOp = (Operator)obj;
            return this.Type == otherOp.Type;
        }

        public override int GetHashCode()
        {
            var hashCode = 38826312;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Value);
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            return hashCode;
        }
    }
}
