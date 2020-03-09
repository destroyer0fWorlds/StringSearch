using System.Collections.Generic;

namespace StringSearch.Filter
{
    /// <summary>
    /// Custom converter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFilterConverter<T> where T : class
    {
        /// <summary>
        /// Convert the supplied criteria to a custom type
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        T ConvertTo(IEnumerable<ICriterion> criteria);
    }
}
