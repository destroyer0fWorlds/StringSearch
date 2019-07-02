using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch
{
    /// <summary>
    /// Represents a criterion
    /// </summary>
    public interface ICriterion
    {
        /// <summary>
        /// Property name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Operator type
        /// </summary>
        LogicOperatorType LogicOperator { get; set; }
    }
}
