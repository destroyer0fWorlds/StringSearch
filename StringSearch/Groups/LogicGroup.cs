using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch.Groups
{
    /// <summary>
    /// And/Or group
    /// </summary>
    class LogicGroup : Group
    {
        public override bool HasNestedGroups => false;
    }
}
