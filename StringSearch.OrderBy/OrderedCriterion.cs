
namespace StringSearch.OrderBy
{
    /// <summary>
    /// Ordered criterion
    /// </summary>
    class OrderedCriterion : IOrderedCriterion
    {
        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public OrderByDirection Direction { get; set; }
    }
}
