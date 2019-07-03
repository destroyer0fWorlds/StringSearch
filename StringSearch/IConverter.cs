using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch
{
    /// <summary>
    /// Custom converter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IConverter<T> where T : class
    {
        /// <summary>
        /// Convert the supplied criteria to a custom type
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        T ConvertTo(IEnumerable<ICriterion> criteria);
    }
}
