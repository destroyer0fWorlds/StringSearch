using System.Collections.Generic;
using StringSearch.Tokens;

namespace StringSearch.Groups
{
    /// <summary>
    /// Base group
    /// </summary>
    abstract class Group : IGroup
    {
        /// <inheritdoc />
        public int Index { get; set; }

        /// <inheritdoc />
        public string Value { get; set; }

        /// <inheritdoc />
        public List<IGroup> NestedGroups { get; set; }

        /// <inheritdoc />
        public abstract bool HasNestedGroups { get; }

        /// <inheritdoc />
        public List<IToken> Tokens { get; set; }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
