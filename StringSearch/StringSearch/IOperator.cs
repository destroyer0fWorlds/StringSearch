using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch
{
    /// <summary>
    /// Represents a configured operation
    /// </summary>
    public interface IOperator
    {
        /// <summary>
        /// Value
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        OperatorType Type { get; set; }
    }
}
