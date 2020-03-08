
namespace StringSearch
{
    /// <summary>
    /// Represents a criterion
    /// </summary>
    public interface ICriterion
    {
        /// <summary>
        /// Raw input
        /// </summary>
        string Raw { get; set; }

        /// <summary>
        /// Property name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Logic operator indicates how multiple criteria should be combined
        /// </summary>
        LogicOperatorType LogicOperator { get; set; }
    }
}
