using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch.Filter.Groups
{
    /// <summary>
    /// And/Or group
    /// </summary>
    class LogicGroup : Group
    {
        /// <inheritdoc />
        public override bool HasNestedGroups => false;
    }
}
