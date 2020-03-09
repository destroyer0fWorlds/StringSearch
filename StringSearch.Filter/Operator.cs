
namespace StringSearch.Filter
{
    /// <summary>
    /// Base operator
    /// </summary>
    public class Operator : IOperator
    {
        /// <inheritdoc />
        public string Value { get; set; }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Value;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var otherOp = (Operator)obj;
            return this.Type == otherOp.Type;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hashCode = 38826312;
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            return hashCode;
        }
    }
}
