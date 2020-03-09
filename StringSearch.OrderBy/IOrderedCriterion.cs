
namespace StringSearch.OrderBy
{
    /// <summary>
    /// Represents an ordered criterion
    /// </summary>
    public interface IOrderedCriterion
    {
        /// <summary>
        /// Property name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Order direction
        /// </summary>
        OrderByDirection Direction { get; set; }
    }
}
