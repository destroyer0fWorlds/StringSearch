using System.Collections.Generic;

namespace StringSearch.OrderBy
{
    /// <summary>
    /// Custom converter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOrderByConverter<T> where T : class
    {
        /// <summary>
        /// Convert the supplied criteria to a custom type
        /// </summary>
        /// <param name="orderedCriteria"></param>
        /// <returns></returns>
        T ConvertTo(IEnumerable<IOrderedCriterion> orderedCriteria);
    }
}
