using System.Collections.Generic;
using StringSearch.Tokens;

namespace StringSearch.Groups
{
    /// <summary>
    /// Represents a group of values that describe a criterion
    /// </summary>
    interface IGroup
    {
        /// <summary>
        /// Starting index where this group was found in the original string
        /// </summary>
        int Index { get; set; }

        /// <summary>
        /// The original string value that this group encapsulates
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// Nested groups
        /// </summary>
        List<IGroup> NestedGroups { get; set; }

        /// <summary>
        /// The original <see cref="Value"/> field parsed as meaningful tokens
        /// </summary>
        List<IToken> Tokens { get; set; }

        /// <summary>
        /// Indicates whether this group is made up of nested groups
        /// </summary>
        bool HasNestedGroups { get; }
    }
}
