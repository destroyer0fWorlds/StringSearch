using System;
using System.Collections.Generic;
using System.Text;
using StringSearch.Tokens;

namespace StringSearch.Groups
{
    abstract class Group : IGroup
    {
        public int Index { get; set; }

        public string Value { get; set; }

        public List<IGroup> NestedGroups { get; set; }

        public abstract bool HasNestedGroups { get; }

        public List<IToken> Tokens { get; set; }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
