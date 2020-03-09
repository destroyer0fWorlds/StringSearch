
namespace StringSearch.Filter.Tokens
{
    /// <summary>
    /// Operator token
    /// </summary>
    class OperatorToken : Token
    {
        /// <summary>
        /// Operator
        /// </summary>
        public ConditionOperatorType Operator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorToken"/>
        /// </summary>
        public OperatorToken()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorToken"/> class with the supplied operator
        /// </summary>
        /// <param name="operator"></param>
        public OperatorToken(ConditionOperatorType @operator)
        {
            Operator = @operator;
        }

        public override string ToString()
        {
            return this.Operator.ToString();
        }
    }
}
