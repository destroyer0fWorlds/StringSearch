
namespace StringSearch.Filter.Tokens
{
    /// <summary>
    /// Logical and/or token
    /// </summary>
    class LogicToken : Token
    {
        /// <summary>
        /// Logic operator (and/or)
        /// </summary>
        public LogicOperatorType Operator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicToken"/> class
        /// </summary>
        public LogicToken()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicToken"/> class with the supplied operator
        /// </summary>
        /// <param name="operator"></param>
        public LogicToken(LogicOperatorType @operator)
        {
            Operator = @operator;
        }

        public override string ToString()
        {
            return this.Operator.ToString();
        }
    }
}
