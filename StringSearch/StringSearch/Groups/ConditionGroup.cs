using System;
using System.Collections.Generic;
using System.Linq;

namespace StringSearch.Groups
{
    class ConditionGroup : Group
    {
        public override bool HasNestedGroups
        {
            get
            {
                return this.NestedGroups != null && this.NestedGroups.Any();
            }
        }
    }
}
