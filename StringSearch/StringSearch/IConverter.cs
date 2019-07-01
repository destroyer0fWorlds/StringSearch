using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch
{
    public interface IConverter<T> where T : class
    {
        T ConvertTo(IEnumerable<ICriterion> criteria);
    }
}
