using System;
using System.Collections.Generic;
using System.Linq;

namespace StringSearch.Filter.Groups
{
    /// <summary>
    /// Criterion group
    /// </summary>
    class CriterionGroup : Group
    {
        /// <inheritdoc />
        public override bool HasNestedGroups
        {
            get
            {
                return this.NestedGroups != null && this.NestedGroups.Any();
            }
        }
    }
}
