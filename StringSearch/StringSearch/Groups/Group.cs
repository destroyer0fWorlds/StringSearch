using System;
using System.Collections.Generic;
using System.Text;
using StringSearch.Tokens;

namespace StringSearch.Groups
{
    /// <summary>
    /// Base group
    /// </summary>
    abstract class Group : IGroup
    {
        /// <summary>
        /// Starting index where this group was found in the original string
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// The original string value that this group encapsulates
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Nested groups
        /// </summary>
        public List<IGroup> NestedGroups { get; set; }

        /// <summary>
        /// Indicates whether this group is made up of nested groups
        /// </summary>
        public abstract bool HasNestedGroups { get; }

        /// <summary>
        /// The original <see cref="Value"/> field parsed as meaningful tokens
        /// </summary>
        public List<IToken> Tokens { get; set; }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
